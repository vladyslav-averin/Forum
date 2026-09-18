using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    private readonly CreateUserView createUserView;
    private readonly CreatePostView createPostView;
    private readonly AddCommentView addCommentView;
    private readonly ViewPostsView viewPostsView;
    private readonly ViewPostView viewPostView;

    public CliApp(
        IUserRepository userRepository,
        IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
        createUserView = new CreateUserView(userRepository);
        createPostView = new CreatePostView(postRepository, userRepository);
        addCommentView = new AddCommentView(
            commentRepository,
            userRepository,
            postRepository);
        viewPostsView = new ViewPostsView(postRepository);
        viewPostView = new ViewPostView(postRepository, commentRepository);
    }

    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Forum CLI ===");
            Console.WriteLine("1. Create user");
            Console.WriteLine("2. Create post");
            Console.WriteLine("3. Add comment");
            Console.WriteLine("4. View posts");
            Console.WriteLine("5. View specific post");
            Console.WriteLine("0. Exit");
            Console.Write("Select an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "0":
                    Console.WriteLine("Goodbye!");
                    return;

                case "1":
                    await createUserView.ShowAsync();
                    break;

                case "2":
                    await createPostView.ShowAsync();
                    break;

                case "3":
                    await addCommentView.ShowAsync();
                    break;

                case "4":
                    await viewPostsView.ShowAsync();
                    break;

                case "5":
                    await viewPostView.ShowAsync();
                    break;

                default:
                    Console.WriteLine("Unknown option. Please try again.");
                    break;
            }
        }
    }
}
