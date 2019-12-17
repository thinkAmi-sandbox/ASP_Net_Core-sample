using System.Collections.Generic;
using EFCoreRelationSample.Models;

namespace EFCoreRelationSample.DTO
{
    public class PostOfFkDTO
    {
        // Postのフィールド
        public int Id { get; set; }
        public string Content { get; set; }
        
        // FKのフィールド
        public BlogOfFk BlogOfFk { get; set; }
    }

    public class BlogOfFkDTO
    {
        // Blogのフィールド
        public int Id { get; set; }
        public string Url { get; set; }

        // FK(Post)のフィールド
        public List<PostOfFk> PostsOfFk { get; set; }
    }
    
}