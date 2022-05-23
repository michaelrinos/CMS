using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models {
    public interface IViewModelBuilder<T> {
        T Build();
    }
    public interface IViewModelBuilder<T, U> {
        T Build(U state);
    }
    public interface IViewModelBuilderAsync<T, U> {
        void Init(U state);
        Task<T> Build();

    }
}
