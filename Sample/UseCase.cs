
namespace Sample
{
    using System.Linq;
    using System.Threading.Tasks;
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using CoWorker.Component.Blogs.Models.Blog.Query;
    using CoWorker.Component.Blogs.Models;
    using CoWorker.Component.Blog.Models;

    [Controller, AllowAnonymous]
    [Route("blog")]
    public class BlogController
    {
        [ControllerContext]
        public ControllerContext ControllerContext { get; set; }

        private readonly IBlogFactory _factory;
        private readonly IActionContextAccessor _accessor;

        public BlogController(
            IActionContextAccessor accessor,
            IBlogFactory factory)
        {
            _factory = factory;
            _accessor = accessor;
        }

        [HttpPost]
        async public Task<IActionResult> Post([FromBody] Post post)
        {
            var related = new PostRelated() { Current = post }.WithParent();
            await Task.Run(() => _factory.Post(related));
            return new CreatedResult($"{ControllerContext.HttpContext.Request.Path.ToString()}/{post.Id.ToString()}", post);
        }

        [HttpGet("{id:guid}")]
        async public Task<IActionResult> Get(Guid id)
        {
            var result = await Task.Run(() => _factory.GetBlogs(new GetBlogsById(id)));
            return new OkObjectResult(result.FirstOrDefault());
        }

        [HttpPost("{parentId:guid}")]
        async public Task<IActionResult> Post([FromBody] Post post, [FromRoute] Guid parentId)
        {
            var related = new PostRelated() { Current = post }.WithParent(parentId);
            await Task.Run(() => _factory.Post(related));
            return new CreatedResult($"{ControllerContext.HttpContext.Request.Path.ToString()}/{post.Id.ToString()}", post);
        }

        [HttpGet("News")]
        async public Task<IActionResult> News()
        {
            var result = await Task.Run(() => _factory.GetBlogs(new News()));
            return new OkObjectResult(result.FirstOrDefault());
        }
    }
}

