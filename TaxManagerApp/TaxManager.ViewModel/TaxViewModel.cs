using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxManager.ViewModel
{
    public class TaxViewModel
    {
        [JsonRequired]
        public string MunicipalityName { get; set; }
        [JsonRequired]
        public DateTime Date { get; set; }
    }
}
