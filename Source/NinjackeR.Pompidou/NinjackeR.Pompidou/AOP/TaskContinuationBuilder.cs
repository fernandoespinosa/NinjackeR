using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NinjackeR.Pompidou.Reflection;

namespace NinjackeR.Pompidou.AOP
{
    public class TaskContinuationBuilder
    {
        public object GetTaskContinuationFromMethodInvocation(object taskObject, Action<Exception> exceptionHandler)
        {
            object taskContinuation = null;

            var taskType = taskObject.GetType();
            /*
             * act on this method only if it is generic and of type Task<TResult> for some TResult
             */
            if (taskType.IsGenericType && taskType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                /*
                 * the faultedTaskContinuationWrapper will PROVIDE AND EXPOSE the method that will be used as a continuation,
                 * in which will invoke the wrapped exceptionHandler.
                 */
                var faultedTaskContinuationWrapper = new FaultedTaskContinuationWrapper(exceptionHandler);

                //-- the TResult
                var resultType = taskType.GetGenericArguments().Single();

                //-- this gets all the methods named "ContinueWith" that return a generic type (presumably Task<T>) and take a single argument
                var continueWithMethods = taskType.GetMethods().Where(m => m.Name == "ContinueWith" && m.ReturnType.IsGenericType && m.GetParameters().Count() == 1);

                //-- this gets the one that takes a Func<Task<T>, R> argument
                var continueWithMethod = continueWithMethods.Single(m => m.GetParameters().Single().ParameterType.GetGenericArguments().First() == taskType);

                // construct the actual (closed generic) method based on the above generic definition that will be invoked
                var closedGenericContinueWithMethod = continueWithMethod.MakeGenericMethod(resultType);

                var continuationMethod = typeof(Func<,>).MakeGenericType(taskType, resultType).CreateDelegate(
                    faultedTaskContinuationWrapper,
                    faultedTaskContinuationWrapper.GetType().GetMethod("ContinuationMethod").MakeGenericMethod(resultType));

                // invoke the "ContinueWith" with method on the original task
                taskContinuation = closedGenericContinueWithMethod.Invoke(taskObject, continuationMethod);
            }
            else
            {
                var task = (Task) taskObject;
                return task.ContinueWith(t => {
                    if (t.IsFaulted)
                        exceptionHandler(t.Exception);
                });
            }

            return taskContinuation;
        }
    }
}