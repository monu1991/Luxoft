using CsvHelper;
using Luxoft.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Luxoft.API.Service
{
    public class FileProcessing : IFileProcessing
    {
        // toDo: confusion regarding to save the data or not if asset ID has not been found in the table
        // currently saving the data into db if AssetID is not there
        public void LoadFileData()
        {
            string filepath = @"C:\Users\nirzar.ambade\source\repos\Luxoft\Luxoft\input.csv";
            List<AssetFileModel> assets = new List<AssetFileModel>();
            using (var reader = new StreamReader(filepath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                assets = csv.GetRecords<AssetFileModel>().ToList();
            }

            // group by id and selecting the row with max TimeStamp
            assets = GroupByIdAndSelectingRowWithMaxDate(assets).ToList();

            var batches = SplitList(assets, 10);
            FileProcessing p = new FileProcessing();
            Parallel.ForEach(batches, batch => {
                p.InsertToDb(ListToDataTable(batch));
            });
        }
        private static List<List<AssetFileModel>> SplitList(List<AssetFileModel> assets, int nSize = 10)
        {
            var list = new List<List<AssetFileModel>>();
            int numberOfRecords = assets.Count > nSize ? assets.Count / nSize : assets.Count;
            for (int i = 0; i < assets.Count; i += numberOfRecords)
            {
                list.Add(assets.GetRange(i, Math.Min(numberOfRecords, assets.Count - i)));
            }

            return list;
        }

        private static IEnumerable<AssetFileModel> GroupByIdAndSelectingRowWithMaxDate(List<AssetFileModel> assets)
        {
            return assets.GroupBy(x => x.AssetId)
                    .Select(x => x
                       .OrderByDescending(y => y.TimeStamp)
                       .First());
        }

        private static DataTable ListToDataTable(List<AssetFileModel> assets)
        {
            DataTable dt = new DataTable();

            dt.Clear();
            dt.Columns.Add("AssetId");
            dt.Columns.Add("IsFixIncome").DefaultValue = false;
            dt.Columns.Add("IsConvertible").DefaultValue = false;
            dt.Columns.Add("IsSwap").DefaultValue = false;
            dt.Columns.Add("IsCash").DefaultValue = false;
            dt.Columns.Add("IsFuture").DefaultValue = false;
            dt.Columns.Add("TimeStamp").DefaultValue = false;

            foreach (var asset in assets)
            {
                DataRow dr = dt.NewRow();
                dr["AssetId"] = asset.AssetId;
                dr["TimeStamp"] = asset.TimeStamp;

                switch (asset.Property.Replace(" ", "").ToLower())
                {
                    case "isfixincome":
                        dr["IsFixIncome"] = asset.Value;
                        break;
                    case "isconvertible":
                        dr["IsConvertible"] = asset.Value;
                        break;
                    case "isswap":
                        dr["IsSwap"] = asset.Value;
                        break;
                    case "iscash":
                        dr["IsCash"] = asset.Value;
                        break;
                    case "isfuture":
                        dr["IsFuture"] = asset.Value;
                        break;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void InsertToDb(DataTable dt)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection("data source=localhost;initial catalog=Luxoft;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
                using (SqlCommand cmd = new SqlCommand("[dbo].[InsertUpdateAssetsFromFile]", sqlConnection))
                {
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapt.SelectCommand.Parameters.Add(new SqlParameter("@AssetInputs", SqlDbType.Structured));
                    adapt.SelectCommand.Parameters["@AssetInputs"].Value = dt;

                    // fill the data table - no need to explicitly call `conn.Open()` - 
                    // the SqlDataAdapter automatically does this (and closes the connection, too)
                    DataTable dt1 = new DataTable();
                    adapt.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
