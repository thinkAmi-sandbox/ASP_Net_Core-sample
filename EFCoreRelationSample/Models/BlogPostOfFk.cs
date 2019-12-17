using System.Collections.Generic;

namespace EFCoreRelationSample.Models
{
    public class BlogOfFk
    {
        public int Id { get; set; }
        public string Url { get; set; }
        
        // ControllerでInclude()を使う際に、ナビゲーションプロパティを設定していないと
        // ビルドエラーになるため、明示的に定義しておく
        public List<PostOfFk> PostsOfFk { get; set; }
    }

    public class PostOfFk
    {
        public int Id { get; set; }
        public string Content { get; set; }

        // ControllerでInclude()を使う際に、ナビゲーションプロパティを設定していないと
        // ビルドエラーになるため、明示的に定義しておく
        // 外部キーは暗黙的な設定で問題なさそう
        public BlogOfFk BlogOfFk { get; set; }
    }
}