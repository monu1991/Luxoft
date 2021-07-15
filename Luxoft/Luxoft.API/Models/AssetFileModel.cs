using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luxoft.API.Models
{
    public class AssetFileModel
    {
        [Name("AssetId")]
        public string AssetId { get; set; }
        [Name("Properties")]
        public string Property { get; set; }
        [Name("Value")]
        public bool Value { get; set; }
        [Name("Timestamp")]
        public DateTime TimeStamp { get; set; } = DateTime.MinValue;
    }
}
