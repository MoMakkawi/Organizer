using Organizer.Generator.Services.FolderServices;
using Organizer.Generator.Services.TypeServices;
using Organizer.Tree.Core;

namespace Organizer.Controller;

public static class Servicer
{
    public static void ImplementOrganizerServices(this List<Node> nodes)
    {
        var orgCtor = nodes?
            .GetRoot()?
            .FindOrganizerConstructor();

        var toDirPath = orgCtor?
            .GetTargetDirectoryPath();

        nodes
            .Where(node => node.IsLeaf)
            .CreateForFolders(toDirPath);

        orgCtor?.GetCustomerTypeDeclarationSyntaxes()
           .IgnoreForTypes(orgCtor)
           .UpdateForTypes(orgCtor)
           .ContainForTypes(nodes, toDirPath);
    }
}