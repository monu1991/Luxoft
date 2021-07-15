using Luxoft.API.Models;
using Luxoft.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luxoft.API.Service
{
    public class AssetService : IAssetService
    {
        private readonly AssetContext _context;
        public AssetService(AssetContext context)
        {
            _context = context;
        }
        public async Task<bool> Update(SetPropertyModel model)
        {
            try
            {
                var asset = _context.Assets.Where(x => x.AssetId == model.AssetId).FirstOrDefault();

                if (model.TimeStamp > asset.TimeStamp)
                {
                    asset.TimeStamp = model.TimeStamp;
                    switch (model.Property.Replace(" ", "").ToLower())
                    {
                        case "isfixincome":
                            asset.IsFixIncome = model.Value;
                            break;
                        case "isconvertible":
                            asset.IsConvertible = model.Value;
                            break;
                        case "isswap":
                            asset.IsSwap = model.Value;
                            break;
                        case "iscash":
                            asset.IsCash = model.Value;
                            break;
                        case "isfuture":
                            asset.IsFuture = model.Value;
                            break;
                    }
                }
                _context.Assets.Update(asset);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
