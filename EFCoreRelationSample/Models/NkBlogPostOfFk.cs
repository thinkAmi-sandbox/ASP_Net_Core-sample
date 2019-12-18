using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreRelationSample.Models
{
    public class NkBlog1
    {
        [Key]
        public string Url { get; set; }

        public List<NkPost1> Posts { get; set; }
    }

    public class NkPost1
    {
        [Key]
        public string Title { get; set; }
        public string Content { get; set; }

        [ForeignKey("Blog")]
        public string UrlFk { get; set; }
        public NkBlog1 Blog { get; set; }
    }
}