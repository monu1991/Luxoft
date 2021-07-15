using Luxoft.API.Models;
using Luxoft.API.Service;
using Luxoft.Data.Context;
using Luxoft.Data.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luxoft.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssetController : Controller
    {
        private readonly AssetContext _context;
        private readonly IAssetService _service;
        private readonly IFileProcessing _fileProcessing;

        public AssetController(AssetContext context, IAssetService service, IFileProcessing fileProcessing)
        {
            _context = context;
            _service = service;
            _fileProcessing = fileProcessing;
        }

        [HttpGet]
        public async Task<ActionResult> Get(string property, bool value)
        {
            try
            {
                var propertyname = property.Replace(" ", "");
                var res = new List<Luxoft.Data.Model.Asset>();
                switch (property.ToLower())
                {
                    case "isfixincome":

                            res = _context.Assets
                                .Where(b => b.IsFixIncome == value)
                                .ToList();
                        break;
                    case "isconvertible":

                            res = _context.Assets
                                .Where(b => b.IsConvertible == value)
                                .ToList();
                        
                        break;
                    case "isswap":

                      
                            res = _context.Assets
                                .Where(b => b.IsSwap == value)
                                .ToList();
                        break;
                    case "iscash":

                            res = _context.Assets
                                .Where(b => b.IsCash == value)
                                .ToList();
                        break;
                    case "isfuture":

                        
                            res = _context.Assets
                                .Where(b => b.IsFuture == value)
                                .ToList();
                        break;
                    default: return BadRequest(500);
                        break;
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(500);
            }
        }

        [HttpGet]
        [Route("loadFile")]
        public bool LoadFileData()
        {
            _fileProcessing.LoadFileData();
            return true;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<bool> SetProperty([FromBody]SetPropertyModel model)
        {
            return await _service.Update(model);            
        }
    }
}
