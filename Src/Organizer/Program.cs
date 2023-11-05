using static Organizer.Handler;
using Organizer.Controller;

do
{
    InputOrganizerPath()
        .GetFileContent()
        .GetSyntaxTree()
        .GetClasses()
        .FindOrganizerClass()!
        .FindOrganizerConstructor()!
        .GetBlockSyntaxes()
        .BuildFileStructureTree()
        .ImplementOrganizerServices();

} while (true);

