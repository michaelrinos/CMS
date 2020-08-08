using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models {
    public class DatabaseFileProvider : IFileProvider {
        #region Fields

        private string _connection;

        #endregion // Fields

        #region Methods

        #region Public
        public DatabaseFileProvider(string connection) {
            _connection = connection;
        }
        public IDirectoryContents GetDirectoryContents(string subpath) {
            Console.WriteLine();
            return new NotFoundDirectoryContents();
        }

        public IFileInfo GetFileInfo(string subpath) {
            if (IsVirtualPath(subpath)) {
                var li = subpath.LastIndexOf('/');
                var ns = subpath.Substring(li == -1 ? 0 : li + 1);
                
                var result = new DatabaseFileInfo(_connection, ns);
                return result.Exists ? result as IFileInfo : new NotFoundFileInfo(subpath);
            } else {
                return new NotFoundFileInfo(subpath);
            }
        }


        public IChangeToken Watch(string filter) {
            return new DatabaseChangeToken(_connection, filter);
        }

        #endregion // Public 

        #region Private

        /// <summary>
        /// Determines if a file is a virtual (db) file or a real file
        /// by looking at the path
        /// </summary>
        /// <param name="v"></param>
        /// <param name="subpath"></param>
        /// <returns></returns>
        private bool IsVirtualPath(string subpath) {
            return subpath.Contains("/Views/Dynamic");
        }

        #endregion // Private 

        #endregion //Methods

    }
}
