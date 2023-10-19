using System.Text.RegularExpressions;

using Organizer.Tree.Core;
using Organizer.Tree.Helpers;

namespace Organizer.Generator.Services.TypeServices
{
    public static class ContainTypeServcie
    {
        public static void ContainForTypes
             (this IEnumerable<BaseTypeDeclarationSyntax> types,
             List<Node> nodes,
             string targetPath)
        {
            if (nodes is null) return;

            foreach ((Node node, string fullTargetPath)
                in from node in nodes
                   let fullTargetPath = GetFullTargetPath(targetPath, node)
                   select (node, fullTargetPath))
            {
                node.Value
                    .GetPrimaryBlockInvocations()
                    .ContainForTypesByName(types, fullTargetPath)
                    .ContainForTypesByPattern(types, fullTargetPath);
            }
        }

        public static IEnumerable<InvocationExpressionSyntax> ContainForTypesByName
           (this IEnumerable<InvocationExpressionSyntax> invocations,
           IEnumerable<BaseTypeDeclarationSyntax> types,
           string fullTargetPath)
        {
            invocations
                .GetSingleParamsOf(nameof(OrganizerServices.ContainType))
                .GetTypesToCreateByNames(types)
                .CreateRequeredTypes(fullTargetPath);

            return invocations;
        }

        public static void ContainForTypesByPattern
           (this IEnumerable<InvocationExpressionSyntax> invocations,
           IEnumerable<BaseTypeDeclarationSyntax> types,
           string fullTargetPath)
        {
            var typesInfoToCreate = invocations
                .GetMultParamsOf(nameof(OrganizerServices.ContainTypes));

            var acceptedPatterns = typesInfoToCreate
                .Select(info => info.First());

            var ignoredPatterns = typesInfoToCreate
                .Where(info => info.Count() > 1)
                .Select(info => info.ElementAt(1));

            types
                .GetTypesToCreateByPatterns(ignoredPatterns, acceptedPatterns)
                .CreateRequeredTypes(fullTargetPath);
        }

        private static void CreateRequeredTypes
            (this IEnumerable<BaseTypeDeclarationSyntax> typesToCreate,
            string fullTargetPath)
        {
            Action<BaseTypeDeclarationSyntax, string> CreateFile = (type, typePath) =>
                 {
                     File.WriteAllText(typePath, type.SyntaxTree.ToString());
                 };

            Func<BaseTypeDeclarationSyntax, string> GetTypePath = (type)
                 => Path
                    .Combine(fullTargetPath, type.Identifier.Text + ".g.cs")
                    .RefactoreSlashes();

            typesToCreate
                .Select(type => new { content = type, path = GetTypePath(type) })
                .ToList()
                .ForEach(_ => CreateFile(_.content, _.path));
        }

        private static string GetFullTargetPath(string targetPath, Node node)
        {
            var folderPath = node.Value?.Header?
                .GetSingleParamsOf(nameof(OrganizerServices.CreateFolder))
                .LastOrDefault();

            return folderPath is null ? targetPath :
                Path.Combine(targetPath, folderPath).RefactoreSlashes();
        }

        private static IEnumerable<InvocationExpressionSyntax> GetPrimaryBlockInvocations
            (this Value value)
        {
            var invocations = value
                .Block
                .Statements
                .ToString()
                .Split('}')
                .SelectMany(s => s.Split('{').FirstOrDefault())
                .ToArray();

            return CSharpSyntaxTree
                .ParseText(new string(invocations))
                .GetRoot()
                .DescendantNodes()
                .OfType<InvocationExpressionSyntax>();
        }

        private static IEnumerable<BaseTypeDeclarationSyntax> GetTypesToCreateByNames
            (this IEnumerable<string> typesNameToCreate, IEnumerable<BaseTypeDeclarationSyntax> types)
            => from type in types
               join typeName in typesNameToCreate
               on type.Identifier.Text.ToString() equals typeName
               select type;

        private static IEnumerable<BaseTypeDeclarationSyntax> GetTypesToCreateByPatterns
            (this IEnumerable<BaseTypeDeclarationSyntax> types,
            IEnumerable<string> ignoredPatterns,
            IEnumerable<string> acceptedPatterns)
            => types
            .Where(type =>
                acceptedPatterns.Any(acceptPattern
                    => Regex.IsMatch(type.Identifier.Text.ToString(), acceptPattern)))
            .Where(type =>
                !ignoredPatterns.Any(ignorePattern
                    => Regex.IsMatch(type.Identifier.Text.ToString(), ignorePattern)));
    }
}