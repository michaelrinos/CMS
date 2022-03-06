using Microsoft.Extensions.FileProviders;
using System.Linq;

namespace SportsStore.Models {
    public interface IViewRepository {
        IQueryable<RazerView> Views { get; }

        void SaveView(RazerView view);
    }
}