using ApiContracts;

namespace BlazorApp.Services;

public interface IPostService
{
    public Task<PostDto> AddPostAsync(CreatePostDto request);
    public Task<IEnumerable<PostDto>> GetPostsAsync();
    public Task<PostDto> GetPostAsync(int id);
}
