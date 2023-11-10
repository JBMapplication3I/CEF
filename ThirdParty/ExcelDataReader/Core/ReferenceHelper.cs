// Decompiled with JetBrains decompiler
// Type: Excel.Core.ReferenceHelper
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core
{
    using System.Text.RegularExpressions;

    /// <summary>A reference helper.</summary>
    public static class ReferenceHelper
    {
        /// <summary>Reference to column and row.</summary>
        /// <param name="reference">The reference.</param>
        /// <returns>An int[].</returns>
        public static int[] ReferenceToColumnAndRow(string reference)
        {
            var regex = new Regex("([a-zA-Z]*)([0-9]*)");
            var upper = regex.Match(reference).Groups[1].Value.ToUpper();
            var s = regex.Match(reference).Groups[2].Value;
            var num1 = 0;
            var num2 = 1;
            for (var index = upper.Length - 1; index >= 0; --index)
            {
                var num3 = upper[index] - 65 + 1;
                num1 += num2 * num3;
                num2 *= 26;
            }
            return new int[2] { int.Parse(s), num1 };
        }
    }
}
