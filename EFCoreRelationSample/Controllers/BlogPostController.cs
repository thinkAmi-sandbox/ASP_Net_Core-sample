using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreRelationSample.DTO;
using EFCoreRelationSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostController : ControllerBase
    {
        private readonly MyContext _context;

        private readonly IMapper _mapper;

        public BlogPostController(MyContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }


        // 1 -> N の順でたぐる場合
        [HttpGet("p2c", Name = "Parent2Child")]
        public async Task<IActionResult> P2C()
        {
            // 両参照を使ってる場合、エラーが出る
            // An unhandled exception occurred while processing the request.
            // JsonException: A possible object cycle was detected which is not supported. This can either be due to a cycle or if the object depth is larger than the maximum allowed depth of 32.
            var blog = await _context.BlogOfFks
                .Include(p => p.PostsOfFk)
                .ToListAsync();
            return Ok(blog);
            
            // ASP.NET Core2時代は、SerializerSettings.ReferenceLoopHandling を設定すれば動作した模様
            // https://docs.microsoft.com/ja-jp/ef/core/querying/related-data#related-data-and-serialization
            // https://qiita.com/Nossa/items/e6bc1bc542a9fad94ebf
            // 
            // しかし、.NET Core3からJSONライブラリが変更になった影響で、上記の設定ができなくなった
            // https://stackoverflow.com/questions/55666826/where-did-imvcbuilder-addjsonoptions-go-in-net-core-3-0
            // AddNewtonsoftJson()を使えば良さそうだが、追加でインストールする必要がある
        }
        
        [HttpGet("p2c-auto", Name = "Parent2ChildWithAutoMapper")]
        public async Task<IActionResult> P2CWithAutoMapper()
        {
            // AutoMapperを使う場合
            // [{"id":1,"url":"https://example.com/foo","postsOfFk":[{"id":1,"content":"ふー","blogOfFk":null},{"id":2,"content":"ばー","blogOfFk":null}]},{"id":2,"url":"https://example.com/bar","postsOfFk":[]}]
            var blogs = await _context.BlogOfFks
                .Include(p => p.PostsOfFk)
                .ProjectTo<BlogOfFkDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return Ok(blogs);
        }
        
        // N -> 1でたぐる場合も、デフォルトのままだと同じエラー
        [HttpGet("c2p", Name = "Child2Parent")]
        public async Task<IActionResult> C2P()
        {
            // JsonException: A possible object cycle was detected which is not supported. This can either be due to a cycle or if the object depth is larger than the maximum allowed depth of 32.
            var posts = await _context.PostOfFks
                .Include(p => p.BlogOfFk)
                .ToListAsync();
            return Ok(posts);
        }
        
        // N -> 1をたぐる場合も、AutoMapperで対応可能
        [HttpGet("c2p-auto", Name = "Child2ParentWithAutoMapper")]
        public async Task<IActionResult> C2PWithAutoMapper()
        {
            var posts = await _context.PostOfFks
                .Include(p => p.BlogOfFk)
                .ProjectTo<PostOfFkDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return Ok(posts);
        }
    }
}