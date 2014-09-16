using System;
using System.Runtime.Serialization;

namespace NinjackeR.Pompidou.Exceptions
{
    [Serializable]
    public class FullStackTraceFormattedDataException : Exception
    {
        public FullStackTraceFormattedDataException(Exception innerException) : base(string.Empty, innerException) { }

        protected FullStackTraceFormattedDataException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return this.InnerException.FullStackTraceFormattedData();
        }
    }
}