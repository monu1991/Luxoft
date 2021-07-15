using Luxoft.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luxoft.API.Service
{
    public interface IAssetService
    {
        Task<bool> Update(SetPropertyModel model);
    }
}
