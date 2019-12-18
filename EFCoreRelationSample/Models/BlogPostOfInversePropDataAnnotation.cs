using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreRelationSample.Models
{
    public class Post4
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int AuthorId { get; set; }
        public User4 Author { get; set; }

        public int ContributorId { get; set; }
        public User4 Contributor { get; set; }
    }

    public class User4
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [InverseProperty("Author")]
        public List<Post4> AuthoredPosts { get; set; }
        
        [InverseProperty("Contributor")]
        public List<Post4> ContributedToPosts { get; set; }
    }
}