using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using TaxManager.Data.Model;
using TaxManager.ViewModel;

namespace TaxManager.Service
{
    public static class ExcelConverter
    {
        public static ImportTaxViewModel GetTaxDetails(string values)
        {
            string[] taxValues = values.Split(',');
            if (ValidateExcelValues(taxValues))
            {
                var taxDetails = new ImportTaxViewModel
                {
                    MunicipalityName = taxValues[0],
                    TaxRate = Convert.ToDouble(taxValues[1]),
                    TaxSchedule = GetTaxSchedule(taxValues[2])
                   
                };
                if (string.IsNullOrEmpty(taxValues[3]) && taxDetails.TaxSchedule == TaxSchedule.Daily)
                {
                    var validDate = DateTime.TryParse(taxValues[3], out DateTime endDate);
                    taxDetails.StartDate = taxDetails.StartDate;
                    taxDetails.EndDate = taxDetails.StartDate;
                }
                else
                {
                    var validDate = DateTime.TryParse(taxValues[4], out DateTime endDate);
                    if(validDate)
                    taxDetails.EndDate = endDate;
                }
                return taxDetails;
            }
            return new ImportTaxViewModel();
        }
       
        private static TaxSchedule GetTaxSchedule(string taxSchedule)
        {
            TaxSchedule tax = (TaxSchedule)Enum.Parse(typeof(TaxSchedule), taxSchedule);
            
            return tax;
        }
        private static bool ValidateExcelValues(string[] data)
        {
            if (data.Length < 4)
            {
                return false;
            }
            var validRate = double.TryParse(data[1], out double taxRate);
            if (!validRate)
            {
                return false;
            }
            var valid = DateTime.TryParse(data[3], out DateTime startDate);
            if (!valid)
            {
                return false;
            }
            return true;
        }
       
    }
}
