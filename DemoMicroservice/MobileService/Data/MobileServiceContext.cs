using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileService.Models;

namespace MobileService.Data
{
    public class MobileServiceContext : DbContext
    {
        public MobileServiceContext (DbContextOptions<MobileServiceContext> options)
            : base(options)
        {
        }

        public DbSet<MobileService.Models.Mobile> Mobile { get; set; } = default!;
    }
}
