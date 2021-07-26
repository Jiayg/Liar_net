using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos.Blog;
using Liar.Application.Contracts.IServices;
using Liar.Domain.Entities;
using Liar.Domain.Repositories;

namespace Liar.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IPostRepository _postRepository;

        public BlogService(IPostRepository postRepository)
        {
            this._postRepository = postRepository;
        }

        public Task<bool> DeletePostAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PostDto> GetPostAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> InsertPostAsync(PostDto dto)
        {
            var entity = new Post
            {
                Title = dto.Title,
                Author = dto.Author,
                Url = dto.Url,
                Html = dto.Html,
                Markdown = dto.Markdown,
                CategoryId = dto.CategoryId,
                CreationTime = dto.CreationTime
            };

            var post = await _postRepository.InsertAsync(entity);
            return post != null;
        }

        public Task<bool> UpdatePostAsync(int id, PostDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}
