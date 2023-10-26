using Microsoft.CodeAnalysis.CSharp;

using Organizer.Controller;

Console.Write("Enter Organizer Path : ");

var path = Console.ReadLine();
if (path is null) throw new Exception($"File in path not Found : {path} ");
else Console.WriteLine($"Ok..., Organizer Path : {path}");

var isFileExist = File.Exists(path);
if (!isFileExist) throw new ($"File in path not Found : {path} ");
else Console.WriteLine($"Ok ..., File exist by name : {Path.GetFileName(path)}");

var code = File.ReadAllText(path);
if (code is null) throw new Exception($"File in path not Found : {path} ");
else Console.WriteLine($"Ok..., Organizer Code Readed");

CSharpSyntaxTree
    .ParseText(code)
    .GetClasses()
    ?.FindOrganizerClass() 
    ?.FindOrganizerConstructor()
    ?.GetBlockSyntaxes()
    ?.BuildFileStructureTree()
    .ImplementOrganizerServices();
    
