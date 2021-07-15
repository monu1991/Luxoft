using Luxoft.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Luxoft.Data.Context
{
    public class AssetContext : DbContext
    {
        public AssetContext(DbContextOptions<AssetContext> options)
        : base(options)
        {
        }
        public DbSet<Asset> Assets { get; set; }
    }
}
