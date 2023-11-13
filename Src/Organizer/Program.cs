using static Organizer.Handler;
using Organizer.Controller;

try
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
}
catch
{
    throw;
}

