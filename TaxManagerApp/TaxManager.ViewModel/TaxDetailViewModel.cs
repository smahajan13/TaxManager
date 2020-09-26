using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TaxManager.ViewModel
{
    public class TaxDetailViewModel
    {
        [JsonRequired]
        public string MunicipalityName { get; set; }
        [JsonProperty("taxType")]
        [JsonRequired]
        public string TaxSchedule { get; set; }
        [JsonRequired]
        public double TaxRate { get; set; }
        [JsonRequired]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class UpdateTaxDetailViewModel
    {
        public int Id { get; set; }
        [JsonRequired]
        public string MunicipalityName { get; set; }
        [JsonProperty("taxType")]
        [JsonRequired]
        public string TaxSchedule { get; set; }
        [JsonRequired]
        public double TaxRate { get; set; }
        [JsonRequired]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
