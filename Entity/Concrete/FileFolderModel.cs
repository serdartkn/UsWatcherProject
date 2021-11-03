using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class FileFolderModel : IEntity
    {
        public string FileContent { get; set; }
        public string FileName { get; set; }
    }
}