using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreRelationSample.Models
{
    public class RqBlog1
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }

    public class RqBlog2
    {
        public int Id { get; set; }
        
        [Required]
        public string Url { get; set; }
    }
    
    public class RqBlog3
    {
        [Key]
        public string Url { get; set; }
        
        // コレクションナビゲーションプロパティ
        public List<RqPost3> Posts { get; set; }
    }

    public class RqPost3
    {
        public int Id { get; set; }
        public string Content { get; set; }

        // 外部キー
        [ForeignKey("RqBlog3Url")]
        public string RqBlogFk { get; set; }
        
        // 参照ナビゲーションプロパティ
        public RqBlog3 Blog { get; set; }
    }

#nullable enable
    public class RqBlog4
    {
        public int Id { get; set; }
        public string? Url { get; set; }
    }
    
    public class RqBlog5
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
#nullable restore
    
}