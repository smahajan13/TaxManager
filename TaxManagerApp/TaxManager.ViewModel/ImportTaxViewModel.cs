using System;
using System.Collections.Generic;
using System.Text;
using TaxManager.Data.Model;

namespace TaxManager.ViewModel
{
    public class ImportTaxViewModel
    {
        public string MunicipalityName { get; set; }
        public TaxSchedule TaxSchedule { get; set; }
        public double TaxRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
