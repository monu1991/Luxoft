using System;
using System.Collections.Generic;
using System.Text;

namespace Luxoft.Data.Model
{
    
    public class Asset
    {
        public int AssetId { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsFixIncome {get;set;}
        public bool IsConvertible { get; set; }
        public bool IsSwap { get; set; }
        public bool IsCash { get; set; }
        public bool IsFuture { get; set; }
    }
}
