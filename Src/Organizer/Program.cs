using Microsoft.CodeAnalysis.CSharp;

Console.WriteLine("Enter Organizer Path : ");

var path = Console.ReadLine()!;
Console.WriteLine($"Ok..., Organizer Path : {path}");

var code = File.ReadAllText(path)!;
Console.WriteLine($"Ok..., Organizer Code Readed");

var tree = CSharpSyntaxTree.ParseText(code);
