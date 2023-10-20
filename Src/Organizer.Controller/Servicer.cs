using Organizer.Generator.Services.FolderServices;
using Organizer.Generator.Services.TypeServices;
using Organizer.Tree.Core;
using Organizer.Tree.Helpers;

namespace Organizer.Controller;

public static class Servicer
{
    public static void ImplementOrganizerServices(this List<Node>? nodes)
    {
        var orgCtor = nodes?
            .GetRoot()?
            .FindOrganizerConstructor();

        var toDirPath = orgCtor?
            .GetTargetDirectoryPath()
            .RefactoreSlashes();

        if (toDirPath is null) 
            throw new ArgumentNullException("Destination path from \"To attribute\" is missing");

        nodes?
            .Where(node => node.IsLeaf)
            .CreateForFolders(toDirPath);

        orgCtor?.GetCustomerTypeDeclarationSyntaxes()
           .IgnoreForTypes(orgCtor)
           .UpdateForTypes(orgCtor)
           .ContainForTypes(nodes, toDirPath);
    }
}