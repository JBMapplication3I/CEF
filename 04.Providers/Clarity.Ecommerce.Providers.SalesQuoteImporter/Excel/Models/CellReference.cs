// <copyright file="CellReference.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cell reference class</summary>
#pragma warning disable SA1600 // Elements should be documented
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter.Excel
{
    using System.Globalization;
    using System.Linq;

    internal class CellReference
    {
        internal CellReference(string cellReference)
        {
            var letter = string.Empty;
            var row = string.Empty;
            foreach (var character in cellReference.ToUpper(CultureInfo.InvariantCulture))
            {
                if (ExcelSalesQuoteImporterProvider.Alphabet.Contains(character))
                {
                    letter += character;
                }
                else
                {
                    row += character;
                }
            }
            Letter = letter;
            Row = uint.Parse(row);
        }

        internal CellReference(string letter, uint row)
        {
            Letter = letter;
            Row = row;
        }

        internal string Letter { get; }

        internal uint Row { get; }
    }
}
