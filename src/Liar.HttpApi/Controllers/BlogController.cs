using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Liar.Application.Contracts.Dtos;
using Liar.Application.Contracts.IServices;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// 查询博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<PostDto> GetPostAsync(int id)
        {
            return await _blogService.GetPostAsync(id);
        }
    }
}
