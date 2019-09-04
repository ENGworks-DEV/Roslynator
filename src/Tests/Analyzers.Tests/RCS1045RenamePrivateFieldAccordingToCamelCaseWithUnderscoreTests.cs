﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Roslynator.CSharp.CodeFixes;
using Xunit;

namespace Roslynator.CSharp.Analysis.Tests
{
    public class RCS1045RenamePrivateFieldAccordingToCamelCaseWithUnderscoreTests : AbstractCSharpFixVerifier
    {
        public override DiagnosticDescriptor Descriptor { get; } = DiagnosticDescriptors.RenamePrivateFieldAccordingToCamelCaseWithUnderscore;

        public override DiagnosticAnalyzer Analyzer { get; } = new RenamePrivateFieldAnalyzer();

        public override CodeFixProvider FixProvider { get; } = new RenameFieldAccordingToCamelCaseWithUnderscoreCodeFixProvider();

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.RenamePrivateFieldAccordingToCamelCaseWithUnderscore)]
        public async Task Test_Lowercase()
        {
            await VerifyDiagnosticAndFixAsync(@"
class C
{
    private string _f;
    private string [|f|];
}
", @"
class C
{
    private string _f;
    private string _f2;
}
");
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.RenamePrivateFieldAccordingToCamelCaseWithUnderscore)]
        public async Task Test_Uppercase()
        {
            await VerifyDiagnosticAndFixAsync(@"
class C
{
    private string [|F|];
}
", @"
class C
{
    private string _f;
}
");
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.RenamePrivateFieldAccordingToCamelCaseWithUnderscore)]
        public async Task Test_UnderscoreUppercase()
        {
            await VerifyDiagnosticAndFixAsync(@"
class C
{
    private string [|_F|];
}
", @"
class C
{
    private string _f;
}
");
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.RenamePrivateFieldAccordingToCamelCaseWithUnderscore)]
        public async Task Test_Underscore()
        {
            await VerifyDiagnosticAndFixAsync(@"
class C
{
    private string [|__|];
}
", @"
class C
{
    private string _;
}
");
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.RenamePrivateFieldAccordingToCamelCaseWithUnderscore)]
        public async Task TestNoDiagnostic_StaticPrefix()
        {
            await VerifyNoDiagnosticAsync(@"
class C
{
    private string s_value;
}
");
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.RenamePrivateFieldAccordingToCamelCaseWithUnderscore)]
        public async Task TestNoDiagnostic_ThreadPrefix()
        {
            await VerifyNoDiagnosticAsync(@"
class C
{
    private string t_value;
}
");
        }
    }
}