namespace Organizer.Controller;

internal static class Attribute
{
    internal static IEnumerable<AttributeSyntax> GetAttributes
        (this ConstructorDeclarationSyntax organizerCtor, Type attribute)
        => organizerCtor
        .AttributeLists
        .SelectMany(a => a.Attributes)
        .Where(a => a.Name.ToString() == attribute.Name);
}