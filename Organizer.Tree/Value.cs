using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Organizer.Tree
{
    public sealed class Value
    {
        public BlockSyntax Block { get; set; }
        public IEnumerable<InvocationExpressionSyntax> Header { get; set; } = Enumerable.Empty<InvocationExpressionSyntax>();
    }
}