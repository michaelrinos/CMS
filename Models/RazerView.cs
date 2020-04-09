using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Models {
    public class RazerView : IFileInfo {

        #region Fields 

        private byte[] _ContentBytes;
        private string _Content;

        #endregion // Fields

        public int RazerViewId { get; set; }
        public string Location { get; set; }

        [NotMapped]
        public byte[] ContentBytes { get => _ContentBytes; private set => _ContentBytes = value; }

        public string Content { get => _Content; 
            set {
                _Content = value;
                _ContentBytes = Encoding.UTF8.GetBytes(value);
            } 
        }
        public DateTimeOffset LastModified { get; set; }

        public DateTime? LastRequested { get; set; }

        [NotMapped]
        public bool Exists => Content == null ? false : true;
        [NotMapped]
        public bool IsDirectory => false;

        [NotMapped]
        public long Length {
            get {
                using (var stream = new MemoryStream(this.ContentBytes)) {
                    return stream.Length;
                }
            }
        }
        [NotMapped]
        public string PhysicalPath => null;

        [NotMapped]
        public string Name => Path.GetFileName(Location);

        public Stream CreateReadStream() {
            return new MemoryStream(ContentBytes);
        }
    }
}
