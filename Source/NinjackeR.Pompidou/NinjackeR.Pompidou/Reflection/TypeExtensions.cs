using System;
using System.Reflection;

namespace NinjackeR.Pompidou.Reflection
{
    public static class TypeExtensions
    {
        public static Delegate CreateDelegate(this Type type, object firstArgument, MethodInfo method)
        {
            return Delegate.CreateDelegate(type, firstArgument, method);
        }
    }
}