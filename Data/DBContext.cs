using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ManyEvents.Models;

namespace ManyEvents.Data
{
    public class DBContext : DbContext
    {
        public DBContext (DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<ManyEvents.Models.MEvent> MEvent { get; set; } = default!;

        public DbSet<ManyEvents.Models.MFeeType> MFeeType { get; set; } = default!;

        public DbSet<ManyEvents.Models.MPerson> MPerson { get; set; } = default!;

        public DbSet<ManyEvents.Models.MCompany> MCompany { get; set; } = default!;
    }
}
