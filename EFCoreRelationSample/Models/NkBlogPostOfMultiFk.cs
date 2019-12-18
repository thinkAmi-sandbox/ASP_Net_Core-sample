using System.Collections.Generic;
using System.Security.Policy;

namespace EFCoreRelationSample.Models
{
    public class NkBlog2
    {
        public string Url { get; set; }
        public string Author { get; set; }

        public string Description { get; set; }
        
        public List<NkPost2> Posts { get; set; }

    }
    
    public class NkPost2
    {
        public string Url { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }
        
        public NkBlog2 Blog { get; set; }
    }
}