using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models {
    public class RazerView : IFileInfo {

        public int RazerViewId { get; set; }
        public string Location { get; set; }
        public byte[] Content { get; set; }
        public DateTimeOffset LastModified { get; set; }

        public DateTime LastRequested { get; set; }

        [NotMapped]
        public bool Exists => Content == null ? false : true;
        [NotMapped]
        public bool IsDirectory => false;

        [NotMapped]
        public long Length {
            get {
                using (var stream = new MemoryStream(this.Content)) {
                    return stream.Length;
                }
            }
        }
        [NotMapped]
        public string PhysicalPath => null;

        [NotMapped]
        public string Name => Path.GetFileName(Location);

        public Stream CreateReadStream() {
            return new MemoryStream(Content);
        }
    }
}
