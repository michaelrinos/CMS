using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models {
    public class FakeContentRepoository : IContentRepository {
        public IQueryable<ContentObject> ContentObjects => new List<ContentObject> {
            new ContentObject() { }

        }.AsQueryable<ContentObject>();

        public void SaveContent(ContentObject item) {
            throw new NotImplementedException();
        }
    }
}
