
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using TaxManager.Data;
using TaxManager.Data.Model;
using TaxManager.ViewModel;

namespace TaxManager.Service.TaxService
{
    public class TaxService : ITaxService
    {
        private readonly IMapper _mapper;
        private readonly TaxManagerContext _context;
        public TaxService(TaxManagerContext context,IMapper mapper)
        {
            _context = context;
           // _mapper = mapper;
        }
        public bool AddTax(TaxDetailViewModel taxViewModel)
        {
            TaxSchedule taxType = (TaxSchedule)Enum.Parse(typeof(TaxSchedule), taxViewModel.TaxSchedule);
            if (!string.IsNullOrWhiteSpace(taxViewModel.MunicipalityName))
            {
                var taxDetails = new TaxDetails
                {
                    MunicipalityName = taxViewModel.MunicipalityName,
                    TaxSchedule = taxType,
                    TaxRate = taxViewModel.TaxRate,
                    StartDate = taxViewModel.StartDate,
                    EndDate = taxViewModel.EndDate,
                };
                _context.TaxDetails.Add(taxDetails);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public UpdateTaxDetailViewModel GetTax(TaxViewModel taxViewModel)
        {
            var taxDetails = _context.TaxDetails.Where(x => x.MunicipalityName == taxViewModel.MunicipalityName && x.StartDate == taxViewModel.Date).FirstOrDefault();
            var taxType = Enum.GetName(typeof(TaxSchedule), taxDetails.TaxSchedule);
            return new UpdateTaxDetailViewModel
            {
                Id =taxDetails.Id,
                MunicipalityName = taxDetails.MunicipalityName,
                TaxRate= taxDetails.TaxRate,
                TaxSchedule = taxType,
                StartDate = taxDetails.StartDate,
                EndDate= taxDetails.EndDate
            };
        }
        public bool ImportTax(MemoryStream stream)
        {
            try
            {
                List<ImportTaxViewModel> taxDetails = new List<ImportTaxViewModel>();
                using (StreamReader reader = new StreamReader(stream))
                {
                    stream.Position = 0;
                    while (!reader.EndOfStream)
                    {
                        string dataLine = reader.ReadLine();
                        if (!string.IsNullOrWhiteSpace(dataLine))
                        {
                            var details = ExcelConverter.GetTaxDetails(dataLine);
                            taxDetails.Add(details);
                        }
                    }
                }
                List<TaxDetails> tax = _mapper.Map<List<ImportTaxViewModel>, List<TaxDetails>>(taxDetails);
                _context.TaxDetails.AddRange(tax);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateTax(UpdateTaxDetailViewModel taxViewModel)
        {
            var taxDetails = _context.TaxDetails.FirstOrDefault(x => x.Id == taxViewModel.Id);
            if (taxDetails != null && taxDetails.Id > 0)
            {
                TaxSchedule taxType = (TaxSchedule)Enum.Parse(typeof(TaxSchedule), taxViewModel.TaxSchedule);
                taxDetails.MunicipalityName = taxViewModel.MunicipalityName;
                taxDetails.TaxSchedule = taxType;
                taxDetails.TaxRate = taxViewModel.TaxRate;
                taxDetails.StartDate = taxViewModel.StartDate;
                taxDetails.EndDate = taxViewModel.EndDate;
                _context.TaxDetails.Update(taxDetails);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }   
}



