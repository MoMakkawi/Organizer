using Organizer.Client;
using Organizer.Client.Attributes;

namespace OrganizerExample;

file class Organizer : OrganizerServices
{
    [From("PathTo\\UnStructuredCode")]
    [To("PathTo\\Destination\\OrganizedCode")]
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
