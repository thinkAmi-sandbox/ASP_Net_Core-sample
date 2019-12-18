using System.Collections.Generic;
using System.Security.Permissions;

namespace EFCoreRelationSample.Models
{
    // 単一プリンシパルキーをFluentAPIで設定
    public class Blog5
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }

        public List<Post5> Posts { get; set; }
    }

    public class Post5
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string BlogUrl { get; set; }
        public Blog5 Blog { get; set; }
    }
    
    
    // 複合プリンシパルキーをFluentAPIで設定
    public class Blog6
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        public List<Post6> Posts { get; set; }
        
    }

    public class Post6
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string BlogUrl { get; set; }
        public string BlogAuthor { get; set; }
        public Blog6 Blog { get; set; }
    }
}