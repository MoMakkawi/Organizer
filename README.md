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
![Organizer Architecture](https://github.com/MoMakkawi/Organizer/assets/94985793/f07c6cf7-d8e4-44bc-b4b9-c5953d907b0e)

## 3 Points that will be clarified for each library :
- How can the user benefit from it in this project?
- Some explanatory notes.
- How can the user reuse this library?

### First Library : Organizer.Usage Library :
#### How can the user benefit from Organizer.Usage Library in this project?
Steps to benefit from this library in our project:
1. Download the library (recommend [NuGet](https://www.nuget.org/profiles/MoMakkawi))
2. Create a C# file (suffixed by .cs)
3. Create a class with any name you prefer (I recommend the name “Organizer”)
4. Make the constructor class inherit from the ["OrganizerServices" class](https://github.com/MoMakkawi/Organizer/blob/master/Src/Organizer.Usage/OrganizerServices.cs) where it is located in the library under the "Organizer.Client" namespace
5. Create a constructor from your class (Step 3).
6. Use the "From" constructor Attribute(s) to specify the path (more than one path is allowed) in which the codes you want to organize will be located, and use the "To" constructor Attribute to specify the path (only one path is allowed) in which the resulting organized codes will be located.
7. Finally, use the organizational services* provided by the library.

Example :
```csharp
using Organizer.Client;
using Organizer.Client.Attributes;

namespace OrganizerExample;

file class Organizer : OrganizerServices
{
    [From("PathTo\\UnStructuredCode\\file.cs")]
    [From("PathTo\\UnStructuredCode\\Folder")]
    [To("PathTo\\DestinationFolder\\OrganizedCodeFolder")]
    public Organizer()
    {
        CreateFolder("Requests");
        {
            ContainTypes("Requests");
        }
        CreateFolder("Responses");
        {
            UpdateTypes("Response", "Res");
            ContainTypes("Res");
        }
        CreateFolder("Models");
        {
            ContainTypes("Model");
        }
    }
}
```

#### Some explanatory notes for Organizer.Usage Library.
#### How can the user reuse this Organizer.Usage library?


# Note :
There is a version of this project that works at Compilation Time called **The Organizer Source Code Generator** and is well documented. \
You can access the code of The Organizer Source Code Generator At Compile Time via [GetHub Reopsitory](https://github.com/MoMakkawi/Organizer-SG),\
You can download The Organizer Source Code Generator At Compile Time RELEASE via [NuGet](https://www.nuget.org/packages/MoMakkawi.Organizer.Generator),\
and The Organizer Source Code Generator At Compile Time Documentation [PDF Link](https://github.com/MoMakkawi/Organizer-SG/blob/master/Organizer%20Official%20Document.pdf).
