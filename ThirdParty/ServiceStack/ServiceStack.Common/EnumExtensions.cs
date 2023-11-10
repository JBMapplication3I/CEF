namespace ServiceStack
{
    using System;
    using System.Collections.Generic;

    public static class EnumExtensions
    {
        /// <summary>Gets the textual description of the enum if it has one. e.g.
        /// <code>
        /// enum UserColors
        /// {
        ///     [Description("Bright Red")]
        ///     BrightRed
        /// }
        /// UserColors.BrightRed.ToDescription();
        /// </code></summary>
        /// <param name="enum">The @enum to act on.</param>
        /// <returns>@enum as a string.</returns>
        public static string ToDescription(this Enum @enum)
        {
            var type = @enum.GetType();
            var memInfo = type.GetMember(@enum.ToString());
            if (memInfo.Length > 0)
            {
                var description = memInfo[0].GetDescription();
                if (description != null)
                {
                    return description;
                }
            }
            return @enum.ToString();
        }

        public static List<string> ToList(this Enum @enum)
        {
            return new(Enum.GetNames(@enum.GetType()));
        }

        public static TypeCode GetTypeCode(this Enum @enum)
        {
            return Enum.GetUnderlyingType(@enum.GetType()).GetTypeCode();
        }

        public static bool Has<T>(this Enum @enum, T value)
        {
            var typeCode = @enum.GetTypeCode();
            return typeCode switch
            {
                TypeCode.Byte => ((byte)(object)@enum & (byte)(object)value) == (byte)(object)value,
                TypeCode.Int16 => ((short)(object)@enum & (short)(object)value) == (short)(object)value,
                TypeCode.Int32 => ((int)(object)@enum & (int)(object)value) == (int)(object)value,
                TypeCode.Int64 => ((long)(object)@enum & (long)(object)value) == (long)(object)value,
                _ => throw new NotSupportedException($"Enums of type {@enum.GetType().Name}"),
            };
        }

        public static bool Is<T>(this Enum @enum, T value)
        {
            var typeCode = @enum.GetTypeCode();
            return typeCode switch
            {
                TypeCode.Byte => (byte)(object)@enum == (byte)(object)value,
                TypeCode.Int16 => (short)(object)@enum == (short)(object)value,
                TypeCode.Int32 => (int)(object)@enum == (int)(object)value,
                TypeCode.Int64 => (long)(object)@enum == (long)(object)value,
                _ => throw new NotSupportedException($"Enums of type {@enum.GetType().Name}"),
            };
        }

        public static T Add<T>(this Enum @enum, T value)
        {
            var typeCode = @enum.GetTypeCode();
            return typeCode switch
            {
                TypeCode.Byte => (T)(object)((byte)(object)@enum | (byte)(object)value),
                TypeCode.Int16 => (T)(object)((short)(object)@enum | (short)(object)value),
                TypeCode.Int32 => (T)(object)((int)(object)@enum | (int)(object)value),
                TypeCode.Int64 => (T)(object)((long)(object)@enum | (long)(object)value),
                _ => throw new NotSupportedException($"Enums of type {@enum.GetType().Name}"),
            };
        }

        public static T Remove<T>(this Enum @enum, T value)
        {
            var typeCode = @enum.GetTypeCode();
            return typeCode switch
            {
                TypeCode.Byte => (T)(object)((byte)(object)@enum & ~(byte)(object)value),
                TypeCode.Int16 => (T)(object)((short)(object)@enum & ~(short)(object)value),
                TypeCode.Int32 => (T)(object)((int)(object)@enum & ~(int)(object)value),
                TypeCode.Int64 => (T)(object)((long)(object)@enum & ~(long)(object)value),
                _ => throw new NotSupportedException($"Enums of type {@enum.GetType().Name}"),
            };
        }
    }
}
