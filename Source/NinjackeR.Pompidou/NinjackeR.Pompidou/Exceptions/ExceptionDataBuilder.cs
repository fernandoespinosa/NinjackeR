using NinjackeR.Pompidou.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NinjackeR.Pompidou.Exceptions
{
    public class ExceptionDataBuilder
    {
        public IDictionary<NamedValue, object> GetExceptionDataFromMethodInvocation(IMethodInvocation invocation)
        {
            return (GetParameterArguments(invocation).AsSummable() + GetPropertyValues(invocation) + GetFieldValues(invocation)).ToDictionary(t => t, t => t.Value);
        }

        public IEnumerable<NamedValue> GetParameterArguments(IMethodInvocation invocation)
        {
            return invocation.Method.GetParameters().Select((p, i) => NamedValue.FromParameter(p, invocation.Arguments[i]));
        }

        public IEnumerable<NamedValue> GetPropertyValues(IMethodInvocation invocation)
        {
            return invocation.InvocationTarget.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(p => p.GetIndexParameters().Length == 0)
                //.Where(p => p.PropertyType.IsValueType || p.PropertyType.IsPrimitive || p.PropertyType == typeof(string))
                .Select(p => NamedValue.FromProperty(p, invocation.InvocationTarget));
        }

        public IEnumerable<NamedValue> GetFieldValues(IMethodInvocation invocation)
        {
            return invocation.InvocationTarget.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                //.Where(p => p.FieldType.IsValueType || p.FieldType.IsPrimitive || p.FieldType == typeof(string))
                .Select(f => NamedValue.FromField(f, invocation.InvocationTarget));
            ;
        }
    }
}
