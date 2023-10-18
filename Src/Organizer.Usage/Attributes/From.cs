namespace Organizer.Client.Attributes;

[AttributeUsage(AttributeTargets.Constructor, AllowMultiple = true)]
public class From : Attribute
{
public From(string path) { }
}