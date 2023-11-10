// <copyright file="ColumnDesc.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the column description class</summary>
namespace Clarity.Ecommerce.DataModel
{
    public class ColumnDesc
    {
        public ColumnDesc(string name, bool nullable, int maxLength, byte scale)
        {
            MaxLength = maxLength;
            Scale = scale;
            Nullable = nullable;
            Name = name;
        }

        public int MaxLength { get; set; }

        public byte Scale { get; set; }

        public bool? Nullable { get; set; }

        public string Name { get; set; }
    }
}
