using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models {
    public class RazerView : IFileInfo {
        public string Location { get; set; }
        public byte[] Content { get; set; }
        public DateTimeOffset LastModified { get; set; }
        [NotMapped]
        public DateTime LastRequested { get; set; }

        public bool Exists => Content == null ? false : true;
        public bool IsDirectory => false;

        public long Length {
            get {
                using (var stream = new MemoryStream(this.Content)) {
                    return stream.Length;
                }
            }
        }
        public string PhysicalPath => null;

        public string Name => Path.GetFileName(Location);

        public Stream CreateReadStream() {
            return new MemoryStream(Content);
        }
    }
}
