using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TaxManager.ViewModel;

namespace TaxManager.Service.TaxService
{
    public interface ITaxService
    {
       UpdateTaxDetailViewModel GetTax(TaxViewModel taxViewModel);
       bool AddTax(TaxDetailViewModel taxViewModel);
       bool ImportTax(MemoryStream stream);
       bool UpdateTax(UpdateTaxDetailViewModel taxViewModel);
    }
}
