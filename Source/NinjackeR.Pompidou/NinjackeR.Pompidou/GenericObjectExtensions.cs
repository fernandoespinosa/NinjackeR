using System;

namespace NinjackeR.Pompidou
{
    public static class GenericObjectExtensions
    {
        public static TResult Try<T, TResult>(this T @object, Func<T, TResult> func)
        {
            return Eval.TryInvoke(() => func(@object));
        }
    }
}