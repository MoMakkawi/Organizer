# Summary:
Organizer a tool called that automates the process of organizing system architecture files and folders based on the developer’s preference. 
So, that could be used by the client to organize the structure of their source code. 
This will result in more usable source code as it will be easier to surf, access and read.

## Important note:
There are two versions of this project, (This Version Here)  the first is a Docker Container, which contains a [Console Project](https://github.com/MoMakkawi/Organizer), while the second is [Soucre Generator](https://github.com/MoMakkawi/Organizer-SG) Run at Compite-Time.

# General idea:
This project is in the field of **Re-architecting** , It generates source code files and folders.
this project presents a Console Project and many libraries created with Roslyn for .NET developers. the project presented is called Organizer. 
It meant to organize any unorganized C# code as requested by the client, by restructuring the base types (classes, interfaces, records, enumerations, structs) 
in different folders and files according to the needs of the client using a set of services. 
The meaning of unorganized code in the scope of The Organizer is C# code files full of base types.

### The services provided by the Organizer are:
 * A service to create folder(s) to contain generated files.
 * A service to include base type(s) in a specific generated folder depending on a type name or a pattern.
 * A service to change the name(s) of specific type(s) depending on a base type name or pattern of multiple base types.
 * A service to ignore type(s) depending on a base type name or a pattern of multiple types.
 * Ability to exclude specific types from the creation or update patterns depending on a type name.

# About techniques:
 - Console Project
 - .NET Compiler Roslyn.
 - xUnit.
 - .NET 7.0.

# Organizer Architecture :
![Organizer Architecture](https://github.com/MoMakkawi/Organizer/assets/94985793/099076cb-f74f-48aa-be8b-3da65f787439)

## 3 Points that will be clarified for each library :
- How can the user benefit from it in this project?
- How can the user reuse this library?
- Some explanatory notes.




# Note :
There is a version of this project that works at Compilation Time called **The Organizer Source Code Generator** and is well documented. \
You can access the code of The Organizer Source Code Generator At Compile Time via [GetHub Reopsitory](https://github.com/MoMakkawi/Organizer-SG),\
You can download The Organizer Source Code Generator At Compile Time RELEASE via [NuGet](https://www.nuget.org/packages/MoMakkawi.Organizer.Generator),\
and The Organizer Source Code Generator At Compile Time Documentation [PDF Link](https://github.com/MoMakkawi/Organizer-SG/blob/master/Organizer%20Official%20Document.pdf).
