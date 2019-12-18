using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreRelationSample.Models
{
    // [ForeignKey]が外部キーにあるパターン
    public class Blog1
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public List<Post1> Posts { get; set; }
    }

    public class Post1
    {
        public int Id { get; set; }
        public string Content { get; set; }

        // 外部キー
        [ForeignKey("Blog")]
        public int BlogFk { get; set; }
        
        // 参照ナビゲーションプロパティ
        public Blog1 Blog { get; set; }
    }
    
    
    // [ForeignKey]が依存エンティティの参照ナビゲーションプロパティにあるパターン
    public class Blog2
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public List<Post2> Posts { get; set; }
    }

    public class Post2
    {
        public int Id { get; set; }
        public string Content { get; set; }

        // 外部キー
        public int BlogFk { get; set; }

        // 参照ナビゲーションプロパティ
        [ForeignKey("BlogFk")]
        public Blog2 Blog { get; set; }
    }
    
    
    // [ForeignKey]がプリンシパルエンティティのコレクションナビゲーションプロパティにあるパターン
    public class Blog3
    {
        public int Id { get; set; }
        public string Url { get; set; }
        
        // コレクションナビゲーションプロパティ
        [ForeignKey("BlogFk")]
        public List<Post3> Posts { get; set; }
    }

    public class Post3
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int BlogFk { get; set; }
        public Blog3 Blog { get; set; }
    }
}