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

<a name="ServicesExplain"></a>
### The services provided by the Organizer:
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

## There 2 Points that will be clarified for each library 
- How can the user benefit from it in this project?
- Some explanatory notes.

### First Library : Organizer.Usage Library :
#### How can the user benefit from Organizer.Usage Library in this project?
Steps to benefit from this library in our project:
1. Download the library (recommend [NuGet](https://www.nuget.org/profiles/MoMakkawi))
2. Create a C# file (suffixed by .cs)
3. Create a class with any name you prefer (I recommend the name “Organizer”)
4. Make the constructor class inherit from the ["OrganizerServices" class](https://github.com/MoMakkawi/Organizer/blob/master/Src/Organizer.Usage/OrganizerServices.cs) where it is located in the library under the "Organizer.Client" namespace
5. Create a constructor from your class (Step 3).
6. Use the "From" constructor Attribute(s) to specify the path (more than one path is allowed) in which the codes you want to organize will be located, and use the "To" constructor Attribute to specify the path (only one path is allowed) in which the resulting organized codes will be located.
7.  Use an [organizer services](#UsageNotes). Curly brackets must be used as shown in the example.

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
<a name="UsageNotes"></a>
#### Some explanatory notes for Organizer.Usage Library.
* [C# BaseTypes](https://learn.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typedeclarationsyntax?view=roslyn-dotnet-4.6.0) That is, we focus on: Classes + Interfaces + Structures + Records
* The [services](#ServicesExplain) provided by the organizer will be used in the form of invocations :
```csharp
//Folder Organizer Service:

CreateFolder("directoryName");

//BaseTypes Organizer Services:

ContainType("typeName");
ContainTypes("pattern");
ContainTypes("pattern", "except");

IgnoreType("typeName");
IgnoreTypes("pattern");

UpdateType("oldTypeName", "newTypeName");
UpdateTypes("pattern", "updateName");
UpdateTypes("pattern", "updateName", "except");
```

### Second Library : Organizer.Tree Library :
#### How can the user benefit from Organizer.Tree Library in this project?
The main function of this library is to build a tree, which is done by calling the ```TreeBuilder``` function located in the [Builder.cs](https://github.com/MoMakkawi/Organizer/blob/master/Src/Organizer.Tree/Builder.cs) file. \
***The tree*** that we will build is like any tree that contains nodes and edges. \
We start with the nodes. **The node** contains two basic things: the value and additional information called the node description. The following figure shows more details for Node Structure.
![Node structure ](https://github.com/MoMakkawi/Organizer/assets/94985793/19d1777b-fe15-412f-be80-4b4c09351072) \
To build **edges**, we take advantage of the additional information that we called the node description,The next paragraph will explain the mechanism of building the tree in the necessary detail.

<a name="TreeNotes"></a>
#### Some explanatory notes for Organizer.Tree Library.
* To build the tree, we will go through three basic steps that will change the values of the node object, as the following diagram shows, which displays snapshots showing how the values change as the object passes through the stages.At each stage, we focus on the variable(s) whose value and color will change to yellow, as in the first step we assigned a value to the block.
1. Build Nodes Descending.
2. Build Edges.
3. Refactor Nodes Informations. \
![SnapShots Diagram](https://github.com/MoMakkawi/Organizer/assets/94985793/69682eea-642d-4d86-ba1c-6a130b7428c2)

* To understand these steps, you need to be focused. \
The first step is to build the tree, but from the bottom up, This will be done by reverse For statement.
![Reversed Nodes](https://github.com/MoMakkawi/Organizer/assets/94985793/44994369-2fd0-4ac5-b5aa-86c3a92cf95c)
Of course, you are wondering why we built the tree in reverse in the first step, and the answer is here in the second step, where we will build the edges. We need to know which node is the father and which node is the son in order to connect them.

The data for each node is two numbers, the beginning and end of the block
The parent’s block certainly contains the block of his children, as well as the grandchildren

This can be achieved through one of **two solutions** (we chose the second): \
_The first:_ is to start with the father and try to search for his direct children and not the grandchildren. This is exhausting and will take a lot of effort, and if you think about it carefully, in our case it is not really effective. \
_The second:_ To build a tree in reverse and then start with the first node, let us call it node 1. We search until we find the first node, which we call node X, so that two conditions are met: the first is the beginning of node 1 is greater than the beginning of node 1 is smaller than the beginning of node x.
The fulfillment of these two conditions makes node 1 a child of node x. \
Why do we consider the first node that fulfills the condition to be the father? Do not think that the input is garbage and the output is garbage as well. The nodes will be in order. This is because the .Net compiler, i.e. Roslin, will read the blocks in a specific order, and I benefited from this feature. \
Of course, after completing the edges, we will return the tree to the order we are accustomed to . \
![NodesWithEdges](https://github.com/MoMakkawi/Organizer/assets/94985793/4e2cbf1a-8e42-4044-a552-a2bde75c1f2d)

The third and final step is maintenance of the tree information, where we give a value to the header of the node. \
![tree](https://github.com/MoMakkawi/Organizer/assets/94985793/05088b59-f85b-4457-893d-8127726626f9) \
**There are two types of headers :** \
_The first_ is the one that is between a parent node and a first child node: here the header of the son is the code written between '{', which specifies the beginning of the father block, and '{', which specifies the beginning of the son block. \
_The second_ is between two nodes. Let us determine the header of the second node, i.e. the code between '}', which expresses the end of the first node's block, and '{', which specifies the beginning of the second node's block. 
```csharp
#region Parent Node
//Parent Node Header
{
    //The First Case , Parent-First Child Header...

    #region First Child Node
    //First Child Node Header
    {

    }
    #endregion First Child Node

    //The Second Case , Child-Child Header...

    #region Another Child Node 
    //Another Child Node Header
    {

    }
    #endregion Another Child Node 
}
#endregion Parent Node
```
Before assigning the header value to the node, we perform maintenance only and only for the content of one type of service, which is ```CreateFolder("folderName");``` where the maintenance is in its parameters. The following example shows what was maintained. 

**Note** that what you will see will change inside the organizer. You can call it an internal change, meaning that the code that the user wrote will not actually change and will remain as he wrote it.

Before maintenance: Note that if you want to know the path of file 3, you have to go up to find out the father path, and thus we notice that there may be a lot of go back, which will cost a lot.
```csharp
CreateFolder("folder1");
{
    CreateFolder("folder2");
    {
        CreateFolder("folder3");
        { ... }
    }
}
```
After maintenance, it will be easy to know file 3 path, and the reason for this is that we will not have to go back.
```csharp
CreateFolder("folder1");
{
    CreateFolder("folder1//folder2");
    {
        CreateFolder("folder1//folder2//folder3");
        { ... }
    }
}
```

### Third Library : Organizer.Services Library :
#### How can the user benefit from Organizer.Services Library in this project?
#### Some explanatory notes for Organizer.Services Library.

# Note :
There is a version of this project that works at Compilation Time called **The Organizer Source Code Generator** and is well documented. \
You can access the code of The Organizer Source Code Generator At Compile Time via [GetHub Reopsitory](https://github.com/MoMakkawi/Organizer-SG),\
You can download The Organizer Source Code Generator At Compile Time RELEASE via [NuGet](https://www.nuget.org/packages/MoMakkawi.Organizer.Generator),\
and The Organizer Source Code Generator At Compile Time Documentation [PDF Link](https://github.com/MoMakkawi/Organizer-SG/blob/master/Organizer%20Official%20Document.pdf).
