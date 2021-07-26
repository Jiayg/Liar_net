using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos.Blog;

namespace Liar.Application.Contracts.IServices
{
    public interface IBlogService
    {
        Task<bool> InsertPostAsync(PostDto dto);

        Task<bool> DeletePostAsync(int id);

        Task<bool> UpdatePostAsync(int id, PostDto dto);

        Task<PostDto> GetPostAsync(int id);
    }
}
