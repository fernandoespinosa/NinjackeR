using System;
using System.Reflection;

namespace NinjackeR.Pompidou.Reflection
{
    public interface IMethodInvocation
    {
        object[] Arguments { get; }
        object InvocationTarget { get; }
        MethodInfo Method { get; }
        object ReturnValue { get; set; }
    }
}
