using System;
using System.Threading.Tasks;

namespace NinjackeR.Pompidou.AOP
{
    public class FaultedTaskContinuationWrapper
    {
        private readonly Action<Exception> _exceptionHandler;

        public FaultedTaskContinuationWrapper(Action<Exception> exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }

        public virtual T ContinuationMethod<T>(Task<T> task)
        {
            if (task.IsFaulted)
                _exceptionHandler(task.Exception);
            return task.Result;
        }
    }
}