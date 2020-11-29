using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStoreRestApiTest.Modules
{
    public class Pet
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photoUrls")]
        public List<string> PhotoUrls { get; set; }

        [JsonProperty("tags")]
        public List<Category> Tags { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}; Id: {Id}; Status: {Status}";
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Pet p = (Pet)obj;
                return (Id == p.Id) && (Name == p.Name);
            }
        }

        public override int GetHashCode()
        {
            return (Id << 2) ^ Name.GetHashCode();
        }
    }

    
}
