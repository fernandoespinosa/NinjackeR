using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace NinjackeR.Pompidou.Exceptions
{
    public class ExceptionFingerprint<THashAlgorithm> where THashAlgorithm : HashAlgorithm
    {
        private readonly THashAlgorithm _hashAlgorithm;

        public ExceptionFingerprint(THashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
        }

        public virtual byte[] ComputeFullStackTraceHash(Exception exception)
        {
            var stringBuilder = new StringBuilder();

            for (var ex = exception; ex != null; ex = ex.InnerException)
            {
                stringBuilder.AppendLine(ex.GetType().ToString());
                stringBuilder.AppendLine(new StackTrace(ex, true).ToString());
            }

            return _hashAlgorithm.ComputeHash(Encoding.Unicode.GetBytes(stringBuilder.ToString()));
        }
    }
}