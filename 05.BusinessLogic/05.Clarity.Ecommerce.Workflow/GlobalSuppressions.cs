// <copyright file="GlobalSuppressions.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the global suppressions class</summary>
// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[module: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Readability",
    "RCS1123:Add parentheses according to operator precedence.",
    Justification = "Redundant parenthesis should not be used.",
    Scope = "namespaceanddescendants",
    Target = "~N:Clarity.Ecommerce.Workflow")]
