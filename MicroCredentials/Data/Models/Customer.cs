using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroCredentials.Data.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "user_name")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "email_address")]
        public string EmailAddress { get; set; }

        [JsonProperty(PropertyName = "pan")]
        public string PAN { get; set; }

        [JsonProperty(PropertyName = "contact_number")]
        public string ContactNumber { get; set; }

        [JsonProperty(PropertyName = "date_of_birth")]
        public DateTime DOB { get; set; }

        [JsonProperty(PropertyName = "account_type")]
        public string AccountType { get; set; }
    }
}
