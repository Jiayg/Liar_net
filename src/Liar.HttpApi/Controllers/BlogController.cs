using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos.Blog;
using Liar.Application.Contracts.IServices;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Liar.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : AbpController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        /// <summary>
        /// 添加博客
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> InsertPostAsync([FromBody] PostDto dto)
        {
            return await _blogService.InsertPostAsync(dto);
        }
        public async Task<bool> DeletePostAsync(int id)
        {
            await _blogService.DeletePostAsync(id);

            return true;
        }

        public async Task<bool> UpdatePostAsync(int id, PostDto dto)
        {
            //var post = await _blogService.GetPostAsync(id);

            //post.Title = dto.Title;
            //post.Author = dto.Author;
            //post.Url = dto.Url;
            //post.Html = dto.Html;
            //post.Markdown = dto.Markdown;
            //post.CategoryId = dto.CategoryId;
            //post.CreationTime = dto.CreationTime;

            //var post = new Post();

            //await _blogService.UpdatePostAsync(post);

            return true;
        }

        public async Task<PostDto> GetPostAsync(int id)
        {
            var post = await _blogService.GetPostAsync(id);

            return new PostDto
            {
                Title = post.Title,
                Author = post.Author,
                Url = post.Url,
                Html = post.Html,
                Markdown = post.Markdown,
                CategoryId = post.CategoryId,
                CreationTime = post.CreationTime
            };
        }
    }
}
