using RepositoryContracts;

namespace CLI.UI;

public class ViewPostsView
{
    private readonly IPostRepository postRepository;

    public ViewPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public Task ShowAsync()
    {
        var posts = postRepository
            .GetMany()
            .OrderBy(post => post.Id)
            .ToList();

        if (posts.Count == 0)
        {
            Console.WriteLine("There are no posts yet.");
            return Task.CompletedTask;
        }

        Console.WriteLine();
        Console.WriteLine("=== Posts ===");

        foreach (var post in posts)
        {
            Console.WriteLine($"[{post.Id}] {post.Title}");
        }

        return Task.CompletedTask;
    }
}
