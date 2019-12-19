using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreRelationSample.Models
{
    public class MyContext : DbContext
    {
        // コンストラクタの型もこれでないとContextにならない
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            this.MyDeleteBehavior = DeleteBehavior.NoAction;
        }

        public DeleteBehavior MyDeleteBehavior { get; }
        public bool IsRequired { get; }
        public MyContext(DbContextOptions<MyContext> options, DeleteBehavior myDeleteBehavior, bool isRequired) : base(options)
        {
            this.MyDeleteBehavior = myDeleteBehavior;
            this.IsRequired = isRequired;
        }
        
        // モデルクラスへのアクセス (DbSet<TModel>) 型のpublicプロパティ
        public DbSet<BlogOfFk> BlogOfFks { get; set; }
        public DbSet<PostOfFk> PostOfFks { get; set; }
        
        // サロゲートキー
        // // 単一ナビゲーションプロパティによる外部キー制約を確認するための設定
        public DbSet<BlogOfSingleNavProp> BlogOfSingleNavProps { get; set; }
        public DbSet<PostOfSingleNavProp> PostOfSingleNavProps { get; set; }
        
        // [ForeignKey] Data annotationによる外部キー制約を確認するための設定
        // 依存エンティティ(子クラス)の外部キーに設定
        public DbSet<Blog1> Blog1List { get; set; }
        public DbSet<Post1> Post1List { get; set; }
        // 依存エンティティの参照ナビゲーションプロパティに設定
        public DbSet<Blog2> Blog2List { get; set; }
        public DbSet<Post2> Post2List { get; set; }
        // プリンシパルエンティティのコレクションナビゲーションプロパティに設定
        public DbSet<Blog3> Blog3List { get; set; }
        public DbSet<Post3> Post3List { get; set; }
        
        // [InverseProperty] Data annotationによる外部キー制約を確認するための設定
        public DbSet<Post4> Post4List { get; set; }
        public DbSet<User4> User4List { get; set; }

        // 単一プリンシパルキーを設定
        public DbSet<Blog5> Blog5List { get; set; }
        public DbSet<Post5> Post5List { get; set; }

        // 複合プリンシパルキーを設定
        public DbSet<Blog6> Blog6List { get; set; }
        public DbSet<Post6> Post6List { get; set; }
        
        // null許容参照型機能の確認(NOT NULL制約に影響)
        public DbSet<RqBlog1> RqBlog1List { get; set; }
        public DbSet<RqBlog2> RqBlog2List { get; set; }
        public DbSet<RqBlog3> RqBlog3List { get; set; }
        public DbSet<RqPost3> RqPost3List { get; set; }
        public DbSet<RqBlog4> RqBlog4List { get; set; }
        public DbSet<RqBlog5> RqBlog5List { get; set; }

        // ナチュラルキー
        
        // [ForeignKey] Data annotation & PKがナチュラルキーの時の、外部キー制約を確認するための設定
        public DbSet<NkBlog1> NkBlog1List { get; set; }
        public DbSet<NkPost1> NkPost1List { get; set; }
        
        // 複合主キー・複合外部キーを持つモデル
        public DbSet<NkBlog2> NkBlog2List { get; set; }
        public DbSet<NkPost2> NkPost2List { get; set; }
        
        // SQLをログ出力するためのLoggerFactoryを用意
        static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information)
                .AddConsole();
        });
        
        // UseLoggerFactory()とEnableSensitiveDataLogging()を設定
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(loggerFactory);
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // データのシードを定義
            // 親データ
            modelBuilder.Entity<BlogOfFk>().HasData(
                new {Id = 1, Url = "https://example.com/foo"},
                new {Id = 2, Url = "https://example.com/bar"}
            );
            
            // 子データ
            modelBuilder.Entity<PostOfFk>().HasData(
                new {Id = 1, Content = "ふー", BlogOfFkId = 1},
                new {Id = 2, Content = "ばー", BlogOfFkId = 1}
            );
            
            
            // Data Annotationでは定義できない制約
            // 単一プリンシパルキーを定義
            modelBuilder.Entity<Post5>()
                .HasOne(m => m.Blog)
                .WithMany(m => m.Posts)
                .HasForeignKey(m => m.BlogUrl)
                .HasPrincipalKey(m => m.Url);
            
            // 複合プリンシパルキーを定義
            modelBuilder.Entity<Post6>()
                .HasOne(m => m.Blog)
                .WithMany(m => m.Posts)
                .HasForeignKey(m => new {m.BlogUrl, m.BlogAuthor})
                .HasPrincipalKey(m => new {m.Url, m.Author});
            
            
            // PKやFKがナチュラルキーの場合の定義
            // Blogの複合主キー
            modelBuilder.Entity<NkBlog2>()
                .HasKey(m => new {m.Url, m.Author});
            
            // Postの複合外部キー
            modelBuilder.Entity<NkPost2>()
                .HasOne(p => p.Blog)
                .WithMany(m => m.Posts)
                .HasForeignKey(m => new {m.Url, m.Author});

            // Postの複合主キー
            modelBuilder.Entity<NkPost2>()
                .HasKey(m => new {m.Url, m.Author, m.Title});
        }
    }
}