using System;
using System.Linq;
using System.Text;

namespace NinjackeR.Pompidou.Exceptions
{
    public static class ExceptionExtensions
    {
        public static string FormatFullStackTraceData(this Exception exception)
        {
            var builder = new StringBuilder();
            var exceptionTrace = exception.InduceSequence(e => e.InnerException).ToArray();
            exceptionTrace.ForEach((e, i) => FormatStackTraceData(builder, e, exceptionTrace.Length - i));
            return builder.ToString();
        }

        private static void FormatStackTraceData(StringBuilder buffer, Exception exception, int position)
        {
            buffer.AppendFormattedLine("--- Exception (Level #{0}) {1}: {2}", position, exception.GetType(), exception.Message);
            if (!string.IsNullOrEmpty(exception.StackTrace))
                buffer.AppendLine(exception.StackTrace);
            if (exception.Data.Count != 0)
            {
                buffer.AppendLine("Data:");
                foreach (var key in exception.Data.Keys)
                    buffer.AppendFormattedLine("   {0}: {1}", key, exception.Data[key]);
            }
            buffer.AppendLine();
        }

        public static string FullMessage(this Exception exception)
        {
            return exception.GetType() + ": " + exception.Message + (exception.InnerException != null ? " ---> " + FullMessage(exception.InnerException) : string.Empty);
        }
    }
}