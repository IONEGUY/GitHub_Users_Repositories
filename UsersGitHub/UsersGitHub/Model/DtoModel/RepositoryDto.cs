using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace UsersGitHub.Model.DtoModel
{
    public class RepositoryDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
