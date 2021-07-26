using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos;

namespace Liar.Application.Contracts.IServices
{
    public interface IBlogService
    {
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> InsertPostAsync(PostDto dto);

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeletePostAsync(int id);

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdatePostAsync(int id, PostDto dto);

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PostDto> GetPostAsync(int id);
    }
}
