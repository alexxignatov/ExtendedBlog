using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedBlog.Model.Data
{
    public class FileDescription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FileDescriptionId { get; set; }

        public string LocalPath { get; set; }
        public int Type { get; set; }
        public virtual UserProfile Profile { get; set; }
    }
}
