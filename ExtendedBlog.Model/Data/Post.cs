using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ExtendedBlog.Model.Infrastructure;

namespace ExtendedBlog.Model.Data
{
    public class Post
    {
        public Post()
        {
            Tags = new List<Tag>();
        }

        [Required(ErrorMessage = "PostId: Field is required")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }

        /* Заголовок статьи */
        [Required(ErrorMessage = "Header: Field is required")]
        public string Header { get; set; }

        [Required(ErrorMessage = "Body: Field is required")]
        public string Body { get; set; }

        [Required(ErrorMessage = "BodyShort: Field is required")]
        public string BodyShort { get; set; }

        //[Required(ErrorMessage = "Title: Field is required")]
        /// <summary>
        /// <title> не видно пользователю, используется поисковым роботом 
        /// </summary>
        public string Title { get; set; }

        [Required(ErrorMessage = "Url: Field is required")]
        public string Url { get; set; }

        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public bool Published { get; set; }

        [Required(ErrorMessage = "PostedOn: Field is required")]

        [JsonProperty]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime PostedOn { get; set; }

        [JsonProperty]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? Modified { get; set; }

        public virtual Category Category { get; set; }
        public virtual IList<Tag> Tags { get; set; }
    }
}
