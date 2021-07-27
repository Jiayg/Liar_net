using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos;
using Liar.Application.Contracts.IServices;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Liar.HttpApi.Host.Controllers
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

        [HttpGet]
        public async Task<string> GetString()
        {
            return await Task.FromResult("hello world");
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
