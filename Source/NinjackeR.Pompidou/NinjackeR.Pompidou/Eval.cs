using System;

namespace NinjackeR.Pompidou
{
    public static class Eval
    {
        public static TResult Invoke<TResult>(Func<TResult> f)
        {
            return f();
        }

        public static TResult Apply<T, TResult>(this T value, Func<T, TResult> f)
        {
            return f(value);
        }

        public static Result<T> TryInvoke<T>(Func<T> f)
        {
            return TryInvoke(f, default(T));
        }

        public static Result<T> TryInvoke<T>(Func<T> f, T @default)
        {
            try
            {
                return new Result<T> { Value = f(), Success = true };
            }
            catch (Exception ex)
            {
                return new Result<T> { Value = @default, Success = false, Exception = ex };
            }
        }

        public static Result<T> TryInvokeOrHandleException<T>(Func<T> f, Func<Exception, T> handler)
        {
            try
            {
                return new Result<T> { Value = f(), Success = true };
            }
            catch (Exception ex)
            {
                return new Result<T> { Value = handler(ex), Success = false, Exception = ex };
            }
        }

        public static Result TryInvoke(Action op)
        {
            try
            {
                op();
                return new Result { Success = true };
            }
            catch (Exception ex)
            {
                return new Result { Success = false, Exception = ex };
            }
        }

        public struct Result
        {
            public bool Success { get; set; }
            public Exception Exception { get; internal set; }
        }

        public struct Result<T>
        {
            public bool Success { get; set; }
            public Exception Exception { get; internal set; }
            public T Value { get; internal set; }

            public static implicit operator T(Result<T> @this)
            {
                return @this.Value;
            }

            public override string ToString()
            {
                return Convert.ToString(Value);
            }
        }
    }
}
