using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Organizer;

internal static class Handler
{
    internal static string InputOrganizerPath()
    {
        Console.Write("Enter Organizer Path : ");
        var path = Console.ReadLine();

        if (path is null) throw new($"File in path not Found : {path} ");
        else Console.WriteLine($"Ok..., Organizer Path : {path}");

        return path;
    }

    private static void CheckFileExistence(this string path)
    {
        var isFileExist = File.Exists(path);

        if (!isFileExist) throw new($"File in path not Found : {path} ");
        else Console.WriteLine($"Ok ..., File exist by name : {Path.GetFileName(path)}");
    }

    internal static string GetFileContent(this string path)
    {
        CheckFileExistence(path);

        var code = File.ReadAllText(path);
        if (code is null) throw new($"File in path not Found : {path} ");
        else Console.WriteLine($"Ok..., Organizer Code Readed");

        return code;
    }

    internal static SyntaxTree GetSyntaxTree(this string code) => CSharpSyntaxTree.ParseText(code);

}
