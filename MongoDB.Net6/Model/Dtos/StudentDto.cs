using Newtonsoft.Json;

namespace MongoDB.Net6.Model.Dtos
{
    public class StudentDto
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("birthDate")]
        public DateTime? BirthDate { get; set; }
    }
}
