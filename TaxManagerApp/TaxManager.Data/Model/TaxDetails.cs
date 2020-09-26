using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace TaxManager.Data.Model
{
    public class TaxDetails
    {
        [Key]
        [Required]
        public int Id { get; set; }        
        [StringLength(50)]
        public string MunicipalityName { get; set; }          
        public TaxSchedule TaxSchedule { get; set; }
        public double TaxRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
