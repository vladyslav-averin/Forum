using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class ViewPostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public ViewPostView(
        IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        Console.Write("Enter post ID: ");
        string? postIdInput = Console.ReadLine();

        if (!int.TryParse(postIdInput, out int postId))
        {
            Console.WriteLine("Post ID must be a number.");
            return;
        }

        Post post;

        try
        {
            post = await postRepository.GetSingleAsync(postId);
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine($"Post with ID {postId} does not exist.");
            return;
        }

        List<Comment> comments = commentRepository
            .GetMany()
            .Where(comment => comment.PostId == postId)
            .OrderBy(comment => comment.Id)
            .ToList();

        Console.WriteLine();
        Console.WriteLine($"=== {post.Title} ===");
        Console.WriteLine($"Post ID: {post.Id}");
        Console.WriteLine($"Author ID: {post.UserId}");
        Console.WriteLine(post.Body);
        Console.WriteLine();
        Console.WriteLine("Comments:");

        if (comments.Count == 0)
        {
            Console.WriteLine("There are no comments yet.");
            return;
        }

        foreach (Comment comment in comments)
        {
            Console.WriteLine(
                $"[{comment.Id}] User {comment.UserId}: {comment.Body}");
        }
    }
}
