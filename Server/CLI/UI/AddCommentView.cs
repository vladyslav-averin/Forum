using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class AddCommentView
{
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;

    public AddCommentView(
        ICommentRepository commentRepository,
        IUserRepository userRepository,
        IPostRepository postRepository)
    {
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
        this.postRepository = postRepository;
    }

    public async Task ShowAsync()
    {
        Console.Write("Enter comment body: ");
        string? body = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(body))
        {
            Console.WriteLine("Comment body cannot be empty.");
            return;
        }

        Console.Write("Enter author ID: ");
        string? userIdInput = Console.ReadLine();

        if (!int.TryParse(userIdInput, out int userId))
        {
            Console.WriteLine("Author ID must be a number.");
            return;
        }

        bool userExists = userRepository
            .GetMany()
            .Any(user => user.Id == userId);

        if (!userExists)
        {
            Console.WriteLine($"User with ID {userId} does not exist.");
            return;
        }

        Console.Write("Enter post ID: ");
        string? postIdInput = Console.ReadLine();

        if (!int.TryParse(postIdInput, out int postId))
        {
            Console.WriteLine("Post ID must be a number.");
            return;
        }

        bool postExists = postRepository
            .GetMany()
            .Any(post => post.Id == postId);

        if (!postExists)
        {
            Console.WriteLine($"Post with ID {postId} does not exist.");
            return;
        }

        Comment comment = new Comment
        {
            Body = body.Trim(),
            UserId = userId,
            PostId = postId
        };

        Comment createdComment = await commentRepository.AddAsync(comment);

        Console.WriteLine(
            $"Comment created with ID {createdComment.Id}.");
    }
}
