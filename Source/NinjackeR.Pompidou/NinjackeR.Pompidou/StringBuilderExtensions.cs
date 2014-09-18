using System;
using System.Collections.Generic;
using System.Text;

namespace NinjackeR.Pompidou {

    public static class StringBuilderExtensions {
        public static StringBuilder AppendFormattedLine(this StringBuilder stringBuilder, string format, params object[] args) {
            stringBuilder.AppendFormat(format, args);
            return stringBuilder.AppendLine();
        }

        public static StringBuilder AppendFormattedLines<T>(this StringBuilder stringBuilder, IEnumerable<T> elements, string lineFormat, Func<T, object[]> paramsDelegate) {
            elements.ForEach(e => stringBuilder.AppendFormattedLine(lineFormat, paramsDelegate(e)));
            return stringBuilder;
        }
    }
}
