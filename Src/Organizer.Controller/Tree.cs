using Organizer.Tree;
using Organizer.Tree.Core;

namespace Organizer.Controller;

public static class Tree
{
    public static List<Node> BuildFileStructureTree(this IEnumerable<BlockSyntax> blocks)
        => Builder.TreeBuilder(blocks);

    public static Node? GetRoot(this List<Node> nodes)
        => nodes.FirstOrDefault(n => n.Parent is null);
}