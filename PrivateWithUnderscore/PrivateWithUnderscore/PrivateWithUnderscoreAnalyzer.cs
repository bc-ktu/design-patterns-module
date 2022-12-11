using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace PrivateWithUnderscore
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class PrivateWithUnderscoreAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "MY0001";

        private static readonly LocalizableString Title = "Private class members must start with and underscore _";
        private static readonly LocalizableString MessageFormat = "Private class members must start with and underscore _";
        private static readonly LocalizableString Description = "Private class members must start with and underscore _";
        private const string Category = "Naming";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Field);
        }

        private static void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            var field = (IFieldSymbol)context.Symbol;

            if (field.Name[0] != '_')
            {
                var diagnostic = Diagnostic.Create(Rule, field.Locations[0], field.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
