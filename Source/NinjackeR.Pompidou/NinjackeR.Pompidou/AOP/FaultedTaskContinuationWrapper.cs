using System;
using System.Threading.Tasks;

namespace NinjackeR.Pompidou.AOP
{
    public class FaultedTaskContinuationWrapper
    {
        private readonly Action<Exception> _action;

        public FaultedTaskContinuationWrapper(Action<Exception> action)
        {
            _action = action;
        }

        public virtual T ContinuationFunction<T>(Task<T> task)
        {
            if (task.IsFaulted)
                _action(task.Exception);
            return task.Result;
        }
    }
}