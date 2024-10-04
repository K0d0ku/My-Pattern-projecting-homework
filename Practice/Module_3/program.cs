using System;
public class program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("workin");
        UserManager userManager = new UserManager();
        userManager.AddUser(new User { Name = "Kodoku", Email = "Kodoku@gmail.com", Role = "Fullstack GameDev" });
        userManager.AddUser(new User { Name = "Notch", Email = "Notch@gmail.com", Role = "GameDev" });
        userManager.DisplayUsers();
        userManager.UpdateUser("Notch@gmail.com", new User { Name = "Notch", Email = "Notch@gmail.com", Role = "Team lead" });
        Console.WriteLine("after update:");
        userManager.DisplayUsers();
        userManager.RemoveUser("Notch@gmail.com");
        Console.WriteLine("after removing user");
        userManager.DisplayUsers();
    }
}
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}
public class UserManager
{
    List<User> users = new List<User>();
    public void AddUser(User user)
    {
        users.Add(user);
        Console.WriteLine($"user {user.Name} {user.Email} have been added");
    }
    public void RemoveUser(string email)
    {
        User userToRemove = users.Find(u => u.Email == email);
        users.Remove(userToRemove);
    }
    public void UpdateUser(string email, User updateUser)
    {
        User userToUpdate = users.Find(u => u.Email == email);
        userToUpdate.Name = updateUser.Name;
        userToUpdate.Email = updateUser.Email;
        userToUpdate.Role = updateUser.Role;
        Console.WriteLine($"User {email} have been updated");
    }
    public void DisplayUsers()
    {
        foreach (var user in users)
        {
            Console.WriteLine($"Name: {user.Name}, Email: {user.Email}, Role: {user.Role}");
        }
    }
}
