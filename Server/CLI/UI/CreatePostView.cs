using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    public CreatePostView(
        IPostRepository postRepository,
        IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.Write("Enter post title: ");
        string? title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title cannot be empty.");
            return;
        }

        Console.Write("Enter post body: ");
        string? body = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(body))
        {
            Console.WriteLine("Body cannot be empty.");
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

        Post post = new Post
        {
            Title = title.Trim(),
            Body = body.Trim(),
            UserId = userId
        };

        Post createdPost = await postRepository.AddAsync(post);

        Console.WriteLine(
            $"Post '{createdPost.Title}' created with ID {createdPost.Id}.");
    }
}
