using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.Write("Enter username: ");
        string? userName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(userName))
        {
            Console.WriteLine("Username cannot be empty.");
            return;
        }

        userName = userName.Trim();

        bool userExists = userRepository
            .GetMany()
            .Any(user => user.UserName.Equals(
                userName,
                StringComparison.OrdinalIgnoreCase));

        if (userExists)
        {
            Console.WriteLine("This username is already taken.");
            return;
        }

        Console.Write("Enter password: ");
        string? password = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Password cannot be empty.");
            return;
        }

        User user = new User
        {
            UserName = userName,
            Password = password
        };

        User createdUser = await userRepository.AddAsync(user);

        Console.WriteLine(
            $"User '{createdUser.UserName}' created with ID {createdUser.Id}.");
    }
}