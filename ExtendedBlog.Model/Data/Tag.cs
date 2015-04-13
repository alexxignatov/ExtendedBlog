using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedBlog.Model.Data
{
    /// <summary>
    /// Класс "меток"
    /// </summary>
    public class Tag
    {
        public Tag() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }

        [Required(ErrorMessage = "Name: Field is required")]
        [StringLength(500, ErrorMessage = "Name: Length should not exceed 500 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Url: Field is required")]
        [StringLength(500, ErrorMessage = "Url: Length should not exceed 500 characters")]
        public string Url { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual IList<Post> Posts { get; set; }
    }
}
