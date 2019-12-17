using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreRelationSample.Models
{
    public class MyContext : DbContext
    {
        // コンストラクタの型もこれでないとContextにならない
        public MyContext(DbContextOptions<MyContext> options) : base(options) {}
        
        // モデルクラスへのアクセス (DbSet<TModel>) 型のpublicプロパティ
        public DbSet<BlogOfFk> BlogOfFks { get; set; }
        public DbSet<PostOfFk> PostOfFks { get; set; }
        
        
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
        }
    }
}