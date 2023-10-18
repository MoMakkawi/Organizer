namespace Organizer.Tree;

public sealed class Value
{
    public required BlockSyntax Block { get; init; }

    public IEnumerable<InvocationExpressionSyntax> Header = Enumerable.Empty<InvocationExpressionSyntax>();
}