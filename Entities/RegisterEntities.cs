

using System.Text.Json.Serialization;
using contactForm.Enum;

namespace contactForm.Entities
{
    public class RegisterEntities
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        [JsonIgnore]
        public QueryType? QueryType { get; set; }

        [JsonIgnore]
        public string Message { get; set; }
    }
}