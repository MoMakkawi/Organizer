namespace Organizer.Client.Attributes;

[AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false)]
public class To : Attribute
{
    public To(string path) { }
}
