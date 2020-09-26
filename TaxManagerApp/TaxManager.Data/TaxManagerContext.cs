using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaxManager.Data.Model;

namespace TaxManager.Data
{
    public class TaxManagerContext:DbContext
    {
        public TaxManagerContext(DbContextOptions<TaxManagerContext> options)
            : base(options)
        { }

        public DbSet<TaxDetails> TaxDetails { get; set; }
    }
}
