using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedBlog.Model.Data
{
    [Table("UserProfile")]
    public class UserProfile
    {
        public UserProfile()
        {
            if (Files == null)
                Files = new List<FileDescription>();
        }

        //Base data
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string UserName { get; set; }
        public bool? Male { get; set; }
        public string Summary { get; set; }
        //public enum Specialization {get;set;}

        //Extended data
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }

        public virtual IList<FileDescription> Files { get; set; }
    }
}
