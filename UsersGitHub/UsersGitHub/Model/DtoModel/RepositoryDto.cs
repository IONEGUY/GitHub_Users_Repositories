using Newtonsoft.Json;

namespace UsersGitHub.Model.DtoModel
{
    public class RepositoryDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
