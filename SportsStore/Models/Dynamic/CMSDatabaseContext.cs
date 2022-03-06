using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Dynamic {
    public class CMSDatabaseContext : DbContext {
        public CMSDatabaseContext(string connectionString) : base() { }
    }
}
