using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models {
    public class EFViewsRepository : IViewRepository {
        private ApplicationDbContext context;

        public EFViewsRepository(ApplicationDbContext ctx) {
            context = ctx;
        }

        public IQueryable<RazerView> Views => context.Views;


    }
}
