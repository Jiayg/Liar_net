using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos;
using Liar.Application.Contracts.IServices;
using Liar.Domain.Shared.BaseModels;
using Microsoft.AspNetCore.Mvc;

namespace Liar.HttpApi.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        /// <summary>
        /// 测试 输出hello world
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<string>> GetString()
        {
            return Success<string>(await Task.FromResult("hello world"));
        }

        /// <summary>
        /// 查询博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ResultModel<PostDto>> GetPostAsync(int id)
        {
            return Success<PostDto>(await _blogService.GetPostAsync(id));
        }
    }
}
