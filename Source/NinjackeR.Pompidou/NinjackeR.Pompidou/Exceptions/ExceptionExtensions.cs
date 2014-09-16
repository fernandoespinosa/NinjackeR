using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NinjackeR.Pompidou.Exceptions
{
    public static class ExceptionExtensions
    {
        private static readonly MD5CryptoServiceProvider Md5 = new MD5CryptoServiceProvider();

        public static string FullStackTraceFormattedData(this Exception exception)
        {
            var builder = new StringBuilder();
            var exceptionTrace = exception.InduceSequence(e => e.InnerException).ToArray();
            exceptionTrace.ForEach((e, i) => AppendStackTraceFormattedData(builder, e, exceptionTrace.Length - i));
            return builder.ToString();
        }

        private static void AppendStackTraceFormattedData(StringBuilder buffer, Exception exception, int position)
        {
            buffer.AppendLine("--- Exception (Level #{0}) {1}: {2}", position, exception.GetType(), exception.Message);
            if (!string.IsNullOrEmpty(exception.StackTrace))
                buffer.AppendLine(exception.StackTrace);
            if (exception.Data.Count != 0)
            {
                buffer.AppendLine("Data:");
                foreach (var key in exception.Data.Keys)
                    buffer.AppendLine("   {0}: {1}", key, exception.Data[key]);
            }
            buffer.AppendLine();
        }

        public static string FullMessage(this Exception exception)
        {
            return exception.GetType() + ": " + exception.Message + (exception.InnerException != null ? " ---> " + FullMessage(exception.InnerException) : string.Empty);
        }

        public static string FullStackTraceChecksum(this Exception exception)
        {
            var stringBuilder = new StringBuilder();

            for (var ex = exception; ex != null; ex = ex.InnerException)
            {
                stringBuilder.AppendLine("{0}", ex.GetType());
                stringBuilder.AppendLine(new StackTrace(ex, true).ToString());
            }

            var checksum = Md5.ComputeHash(Encoding.Unicode.GetBytes(stringBuilder.ToString()));

            return BitConverter.ToString(checksum).Replace("-", "");
        }
    }
}