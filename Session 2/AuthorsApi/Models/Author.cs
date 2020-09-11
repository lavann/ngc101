using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AuthorsApi.Models
{
    public class Author
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("picture")]
        public string Picture { get; set; }

        [Newtonsoft.Json.JsonProperty("age")]
        public int Age { get; set; }

        [Newtonsoft.Json.JsonProperty("genre")]
        public string Genre { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("gender")]
        public string Gender { get; set; }

        [Newtonsoft.Json.JsonProperty("email")]
        public string Email { get; set; }

        [Newtonsoft.Json.JsonProperty("phone")]
        public string Phone { get; set; }

        [Newtonsoft.Json.JsonProperty("authorBio")]
        public string AuthorBio { get; set; }

        [Newtonsoft.Json.JsonProperty("publishDate")]
        public string PublishDate { get; set; }

        [Newtonsoft.Json.JsonProperty("tags")]
        public List<string> Tags { get; set; }

    }

  

}
