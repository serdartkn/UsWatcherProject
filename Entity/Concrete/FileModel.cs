using Core.Entity.Abstract;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    [Table("File")]
    public class FileModel : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string ChangeType { get; set; }
        public byte[] FileContentHash { get; set; }
        public byte[] FileContentSalt { get; set; }
        public string FileName { get; set; }
        public DateTime Date { get; set; }
    }
}