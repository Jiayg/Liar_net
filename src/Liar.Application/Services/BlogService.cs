using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos;
using Liar.Application.Contracts.IServices;
using Liar.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Liar.Application.Services
{
    public class BlogService : LiarAppService, IBlogService
    {
        private readonly IRepository<Post> _postRepo;

        public BlogService(IRepository<Post> postRepo)
        {
            this._postRepo = postRepo;
        }

        public Task<bool> DeletePostAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PostDto> GetPostAsync(int id)
        {
            var entities = await _postRepo.GetAsync(x => x.Id.Equals(id));

            return ObjectMapper.Map<Post, PostDto>(entities);
        }

        public Task<bool> InsertPostAsync(PostDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdatePostAsync(int id, PostDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}
