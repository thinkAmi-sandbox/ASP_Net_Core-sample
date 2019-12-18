using System.Collections.Generic;

namespace EFCoreRelationSample.Models
{
    public class BlogOfSingleNavProp
    {
        public int Id { get; set; }
        public string Url { get; set; }
        
        // 単一ナビゲーションプロパティ(Single Navigation Property)のみ定義
        public List<PostOfSingleNavProp> PostsOfFk { get; set; }
    }

    public class PostOfSingleNavProp
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}