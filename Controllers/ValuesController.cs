using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using n11api.Services;
using n11api.Models;

namespace n11api.Controllers
{
    [Route("api/[action]")]
    public class ValuesController : Controller
    {
        HtmlParserService _htmlParserService;
        public ValuesController(HtmlParserService htmlParserService)
        {
            _htmlParserService = htmlParserService;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts(string url, int page)
        {
            return _htmlParserService.GetProducts(url, page);
        }

        [HttpGet]
        public ProductDetail GetProductDetail(string idOrUrl)
        {
            return _htmlParserService.GetProductDetail(idOrUrl);
        }

    }
}
