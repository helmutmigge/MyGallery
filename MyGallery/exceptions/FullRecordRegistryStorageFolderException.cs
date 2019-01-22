using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGallery.exceptions
{
    public class FullRecordRegistryStorageFolderException : Exception
    {
        public FullRecordRegistryStorageFolderException(string message) : base(message) { }

    }
}
