using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luxoft.API.Models
{
    public class SetPropertyModel
    {
        public int AssetId { get; set; } 
        public string Property { get; set; }
        public bool Value { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
