using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models {
        /*
    public class EFViewsRepository : IViewRepository, IFileProvider {
        private ApplicationDbContext context;

        public IQueryable<RazerView> Views => context.Views;

        public IDirectoryContents GetDirectoryContents(string subpath) {
            throw new NotImplementedException();
        }

        public IFileInfo GetFileInfo(string subpath) {
            return Views.Where(x => x.Location == subpath).FirstOrDefault();
        }

        public IChangeToken Watch(string filter) {
            throw new NotImplementedException();
        }

    }
    // */
}
