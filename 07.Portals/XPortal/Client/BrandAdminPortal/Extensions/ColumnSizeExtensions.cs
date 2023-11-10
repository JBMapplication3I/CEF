// <copyright file="ColumnSizeExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the column size extensions class</summary>
// ReSharper disable InconsistentNaming
namespace Clarity.Ecommerce.UI.XPortal.SharedLibrary
{
    using Blazorise;
    using JetBrains.Annotations;

    /// <summary>A column size extensions.</summary>
    [PublicAPI]
    public static class CS
    {
        /// <summary>Gets the start of a fluent column size.</summary>
        /// <value>The start.</value>
        public static IFluentColumnWithSize Start => new FluentColumn();

        /// <summary>Declares a column size set all at once.</summary>
        /// <param name="xs">The xs column size.</param>
        /// <param name="sm">The sm column size.</param>
        /// <param name="md">The md column size.</param>
        /// <param name="lg">The lg column size.</param>
        /// <param name="xl">The xl column size.</param>
        /// <returns>An IFluentColumnWithSize.</returns>
        public static IFluentColumnWithSize Declare(CW xs, CW sm, CW md, CW lg, CW xl)
            => new FluentColumn().XS(xs).SM(sm).MD(md).LG(lg).XL(xl);
    }

    /// <summary>A column size extensions.</summary>
    [PublicAPI]
    public static class ColumnSizeExtensions
    {
        /// <summary>Valid on all devices. (extra small)</summary>
        /// <param name="col">The col to act on.</param>
        /// <returns>An IFluentColumnWithSize.</returns>
        public static IFluentColumnWithSize OnXS(this IFluentColumnOnBreakpointWithOffsetAndSize col)
            => ((FluentColumn)col).WithBreakpoint(Breakpoint.Mobile);

        /// <summary>Breakpoint on tablets (small).</summary>
        /// <param name="col">The col to act on.</param>
        /// <returns>An IFluentColumnWithSize.</returns>
        public static IFluentColumnWithSize OnSM(this IFluentColumnOnBreakpointWithOffsetAndSize col)
            => ((FluentColumn)col).WithBreakpoint(Breakpoint.Tablet);

        /// <summary>Breakpoint on desktop (medium).</summary>
        /// <param name="col">The col to act on.</param>
        /// <returns>An IFluentColumnWithSize.</returns>
        public static IFluentColumnWithSize OnMD(this IFluentColumnOnBreakpointWithOffsetAndSize col)
            => ((FluentColumn)col).WithBreakpoint(Breakpoint.Desktop);

        /// <summary>Breakpoint on wide-screen (large).</summary>
        /// <param name="col">The col to act on.</param>
        /// <returns>An IFluentColumnWithSize.</returns>
        public static IFluentColumnWithSize OnLG(this IFluentColumnOnBreakpointWithOffsetAndSize col)
            => ((FluentColumn)col).WithBreakpoint(Breakpoint.Widescreen);

        /// <summary>Breakpoint on large desktops (extra large).</summary>
        /// <param name="col">The col to act on.</param>
        /// <returns>An IFluentColumnWithSize.</returns>
        public static IFluentColumnWithSize OnXL(this IFluentColumnOnBreakpointWithOffsetAndSize col)
            => ((FluentColumn)col).WithBreakpoint(Breakpoint.FullHD);

        /// <summary>Valid on all devices. (extra small)</summary>
        /// <param name="col"> The col to act on.</param>
        /// <param name="size">The size.</param>
        /// <returns>An IFluentColumnWithSize.</returns>
        public static IFluentColumnWithSize XS(this IFluentColumnWithSize col, CW size)
            => ((FluentColumn)((FluentColumn)col).WithColumnSize((ColumnWidth)size)).WithBreakpoint(Breakpoint.Mobile);

        /// <summary>Breakpoint on tablets (small).</summary>
        /// <param name="col"> The col to act on.</param>
        /// <param name="size">The size.</param>
        /// <returns>An IFluentColumnWithSize.</returns>
        public static IFluentColumnWithSize SM(this IFluentColumnWithSize col, CW size)
            => ((FluentColumn)((FluentColumn)col).WithColumnSize((ColumnWidth)size)).WithBreakpoint(Breakpoint.Tablet);

        /// <summary>Breakpoint on desktop (medium).</summary>
        /// <param name="col"> The col to act on.</param>
        /// <param name="size">The size.</param>
        /// <returns>An IFluentColumnWithSize.</returns>
        public static IFluentColumnWithSize MD(this IFluentColumnWithSize col, CW size)
            => ((FluentColumn)((FluentColumn)col).WithColumnSize((ColumnWidth)size)).WithBreakpoint(Breakpoint.Desktop);

        /// <summary>Breakpoint on wide-screen (large).</summary>
        /// <param name="col"> The col to act on.</param>
        /// <param name="size">The size.</param>
        /// <returns>An IFluentColumnWithSize.</returns>
        public static IFluentColumnWithSize LG(this IFluentColumnWithSize col, CW size)
            => ((FluentColumn)((FluentColumn)col).WithColumnSize((ColumnWidth)size)).WithBreakpoint(Breakpoint.Widescreen);

        /// <summary>Breakpoint on large desktops (extra large).</summary>
        /// <param name="col"> The col to act on.</param>
        /// <param name="size">The size.</param>
        /// <returns>An IFluentColumnWithSize.</returns>
        public static IFluentColumnWithSize XL(this IFluentColumnWithSize col, CW size)
            => ((FluentColumn)((FluentColumn)col).WithColumnSize((ColumnWidth)size)).WithBreakpoint(Breakpoint.FullHD);
    }

    /// <summary>Values that represent column widths.</summary>
    /// <remarks>This is just a redirect of Blazorise.ColumnWidth enum to shorthand the name.</remarks>
    [PublicAPI]
    public enum CW
    {
        /// <summary>No sizing.</summary>
        None = ColumnWidth.None,

        /// <summary>One column width.</summary>
        Is1 = ColumnWidth.Is1,

        /// <summary>Two columns width.</summary>
        Is2 = ColumnWidth.Is2,

        /// <summary>Three columns width.</summary>
        Is3 = ColumnWidth.Is3,

        /// <summary>Four columns width.</summary>
        Is4 = ColumnWidth.Is4,

        /// <summary>Five columns width.</summary>
        Is5 = ColumnWidth.Is5,

        /// <summary>Six columns width.</summary>
        Is6 = ColumnWidth.Is6,

        /// <summary>Seven columns width.</summary>
        Is7 = ColumnWidth.Is7,

        /// <summary>Eight columns width.</summary>
        Is8 = ColumnWidth.Is8,

        /// <summary>Nine columns width.</summary>
        Is9 = ColumnWidth.Is9,

        /// <summary>Ten columns width.</summary>
        Is10 = ColumnWidth.Is10,

        /// <summary>Eleven columns width.</summary>
        Is11 = ColumnWidth.Is11,

        /// <summary>Twelve columns width.</summary>
        Is12 = ColumnWidth.Is12,

        /// <summary>Twelve columns width.</summary>
        Full = ColumnWidth.Full,

        /// <summary>Six columns width.</summary>
        Half = ColumnWidth.Half,

        /// <summary>Four columns width.</summary>
        Third = ColumnWidth.Third,

        /// <summary>Three columns width.</summary>
        Quarter = ColumnWidth.Quarter,

        /// <summary>Fill all available space.</summary>
        Auto = ColumnWidth.Auto,
    }
}
