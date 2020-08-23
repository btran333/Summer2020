using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace RESTService.Models
{
    public class User
    {
        [JsonProperty("ID")]
        public Guid Id { get; set; }

        [JsonProperty("Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [JsonProperty("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [JsonProperty("DateAdded")]

        public DateTime DateAdded { get; set; }
    }

}