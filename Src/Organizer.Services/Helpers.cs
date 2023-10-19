using Organizer.Tree.Helpers;

namespace Organizer.Services;

internal static class Helpers
{
    internal static IEnumerable<InvocationExpressionSyntax> GetInvocations(this ConstructorDeclarationSyntax constructor)
    {
        return constructor?
            .DescendantNodes()
            .OfType<InvocationExpressionSyntax>();
    }

    internal static string GetTypeName(this ArgumentSyntax arg)
        => arg.GetParameterValue();

    internal static BaseTypeDeclarationSyntax ConvertToBaseTypeDeclarationSyntax(string type)
        => CSharpSyntaxTree.ParseText(type)
            .GetRoot()
            .DescendantNodes()
            .OfType<BaseTypeDeclarationSyntax>()
            .ElementAtOrDefault(0);

    internal static IEnumerable<InvocationExpressionSyntax> GetInvocationsByName
        (this IEnumerable<InvocationExpressionSyntax> invocations, string organizerServiceName)
        => invocations.Where(invoc => invoc.IsName(organizerServiceName));

    internal static IEnumerable<string> GetSingleParamsOf
        (this IEnumerable<InvocationExpressionSyntax> invocations,
        string organizerServiceName)
    {
        return invocations
            .GetInvocationsByName(organizerServiceName)
            .SelectMany(invoc => invoc.ArgumentList.Arguments)
            .Select(arg => arg.GetParameterValue());
    }

    internal static IEnumerable<IEnumerable<string>> GetMultParamsOf
        (this IEnumerable<InvocationExpressionSyntax> invocations,
        string organizerServiceName)
    {
        return invocations
            .GetInvocationsByName(organizerServiceName)
            .Select(invoc => invoc.ArgumentList.Arguments)
            .Select(args => args.Select(arg => arg.GetParameterValue()));
    }

    /// <summary>
    /// convert BaseTypeDeclarationSyntax to string
    /// then add to this string
    /// the namespace declaration and the using directives
    /// </summary>
    /// <param name="type">ClassDeclarationSyntax or StructDeclarationSyntax or EnumDeclarationSyntax</param>
    /// <returns>string contain : using directives then the namespace declaration have the input type declaration</returns>
    internal static string RefactoreToString(this BaseTypeDeclarationSyntax type)
    {
        var nodes = type
            .Parent
            .SyntaxTree
            .GetRoot()
            .DescendantNodes();

        var namespaceName = string.Join(".", nodes
            .OfType<NamespaceDeclarationSyntax>()
            .Select(@namespace => @namespace.Name.ToString()));
            

        var usings = string.Concat(nodes
            .OfType<UsingDirectiveSyntax>()
            .Select(@using => @using.ToString()));

        var refactoredType = namespaceName is null
            ? string.Concat(usings , type)
            : string.Concat(usings , "namespace ", namespaceName, "{", type, "}");

        return CSharpSyntaxTree
            .ParseText(refactoredType)
            .GetRoot()
            .NormalizeWhitespace()
            .ToString();
    }

}