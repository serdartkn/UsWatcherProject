using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class File : IEntity
    {
        public int Id { get; set; }
        public string ChangeType { get; set; }
        public string FileName { get; set; }
        public string Sha512 { get; set; }
    }
}
