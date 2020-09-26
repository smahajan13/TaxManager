using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaxManager.Service;
using TaxManager.Service.TaxService;
using TaxManager.ViewModel;

namespace TaxManagerApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxManagerController : ControllerBase
    {
        private readonly ILogger<TaxManagerController> _logger;
        private readonly ITaxService _taxService ;

        public TaxManagerController(ILogger<TaxManagerController> logger, ITaxService taxService )
        {
            _logger = logger;
            _taxService = taxService;
        }

        /// <summary>
        /// To retreive the taxdetails on the basis date
        /// </summary>
        /// <param name="taxViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTaxes")]
        public IActionResult GetTax([FromBody] TaxViewModel taxViewModel)
        {
            try
            {
                var result = _taxService.GetTax(taxViewModel);
                var response = new Response
                {
                    Data = result,
                    Message = "Data fetched successfully"
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                var response = new Response
                {
                    Message = ex.Message
                };
                return BadRequest(response);
            }           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taxDetailViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddTax")]
        public IActionResult AddTax([FromBody] TaxDetailViewModel taxDetailViewModel)
        {
            try
            {
                var result = _taxService.AddTax(taxDetailViewModel);
                if (result)
                {
                    var response = new Response
                    {
                        Data = result,
                        Message = "File imported successfully in database"
                    };
                    return Ok();
                }
                else
                {
                    var response = new Response
                    {
                        Data = result,
                        Message = "Some error in file, please verify and import again"
                    };
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                var response = new Response
                {
                    Message = ex.Message
                };
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Import taxes using CSV, please provide columns in the mentioned sequence municipalityName,TaxRate, TaxSchedule, StartDate,EndDate
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ImportTaxes")]
        public IActionResult ImportTaxDetails([FromBody] IFormFile file)
        {
            try
            {
                if (Path.GetExtension(file.FileName)==".CSV")
                {
                    var response= new Response
                    {
                        Message = "Only CSV files are supported.Please check the file format and import again."
                    };
                }
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);
                    var result = _taxService.ImportTax(newMemoryStream);
                    if (result)
                    {
                       var response =  new Response
                        {
                            Data = result,
                            Message = "File imported successfully in database"
                        };
                        return Ok(response);
                    }
                    else
                    {
                        var response = new Response
                        {
                            Data = result,
                            Message = "Some error in file, please verify and import again"
                        };
                        return BadRequest(response);
                    }
                }
            }
            catch(Exception ex)
            {
                var response = new Response
                {
                    Message = ex.Message
                };
                return BadRequest(response);
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="municipalityTaxRecord"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateMunicipality([FromBody] UpdateTaxDetailViewModel taxDetailViewModel)
        {
            try
            {
                var result = _taxService.UpdateTax(taxDetailViewModel);
                if (result)
                {
                    var response = new Response
                    {
                        Data = result,
                        Message = "File imported successfully in database"
                    };
                    return Ok();
                }
                else
                {
                    var response = new Response
                    {
                        Data = result,
                        Message = "Some error in file, please verify and import again"
                    };
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                var response = new Response
                {
                    Message = ex.Message
                };
                return BadRequest(response);
            }
        }
    }
}
