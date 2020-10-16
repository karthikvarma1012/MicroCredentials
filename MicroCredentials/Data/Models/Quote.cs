using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroCredentials.Data.Models
{
    public class Quote
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "customer_id")]
        public int CustomerId { get; set; }

        [JsonProperty(PropertyName = "start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "end_date")]
        public DateTime EndDate { get; set; }

        [JsonProperty(PropertyName = "contribution_amount")]
        public decimal ContributionAmount { get; set; }

        [JsonProperty(PropertyName = "maturity_amount")]
        public decimal MaturityAmount { get; set; }
    }
}
