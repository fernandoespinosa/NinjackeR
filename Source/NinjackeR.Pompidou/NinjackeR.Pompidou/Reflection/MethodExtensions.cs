using System.Reflection;

namespace NinjackeR.Pompidou.Reflection
{
    public static class MethodExtensions
    {
        public static object Invoke(this MethodBase method, object obj, params object[] parameters)
        {
            return method.Invoke(obj, parameters);
        }
    }
}