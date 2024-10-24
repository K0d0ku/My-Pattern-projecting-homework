/*output:
Alisa joined the chat.

Bob joined the chat.

Charli joined the chat.
Alisa sends: hello everyone!
Bob received: hello everyone!
Charli received: hello everyone!
Bob sends: hello Alisa!
Alisa received: hello Alisa!
Charli received: hello Alisa!
Charli sends: hi everyone!
Alisa received: hi everyone!
Bob received: hi everyone!
Alisa sends a private msg: hi Bob!
Bob received: [private msg from Alisa]: hi Bob!
Bob sends a private msg: hi Alisa!
Alisa received: [private msg from Bob]: hi Alisa!

John joined the chat.

Dexter joined the chat.

Morgan joined the chat.

David joined the chat.
Alisa sends: hi @all in Group 1!
Bob received: hi @all in Group 1!
Charli received: hi @all in Group 1!
Bob sends: hi Dexter in Group 1!
Alisa received: hi Dexter in Group 1!
Charli received: hi Dexter in Group 1!
Charli sends: hi @all in Group 2!
Alisa received: hi @all in Group 2!
Bob received: hi @all in Group 2!
John sends: hi David in Group 2!
Dexter received: hi David in Group 2!

Bob has disconnected from the chat.

Bob is removed from chat.*/

using System;
using System.Collections.Generic;
public class program
{
    public static void Main(string[] args)
    {
        ChatMediator chatMediator = new ChatMediator();

        User user1 = new User(chatMediator, "Alisa");
        User user2 = new User(chatMediator, "Bob");
        User user3 = new User(chatMediator, "Charli");

        chatMediator.RegisterColleague(user1);
        chatMediator.RegisterColleague(user2);
        chatMediator.RegisterColleague(user3);

        user1.Send("hello everyone!");
        user2.Send("hello Alisa!");
        user3.Send("hi everyone!");

        user1.SendPrivateMessage(user2, "hi Bob!");
        user2.SendPrivateMessage(user1, "hi Alisa!");

        ChatGroup group1 = new ChatGroup("group 1");
        ChatGroup group2 = new ChatGroup("group 2");

        User user4 = new User(group1.Mediator, "John");
        User user5 = new User(group1.Mediator, "Dexter");
        User user6 = new User(group2.Mediator, "Morgan");
        User user7 = new User(group2.Mediator, "David");

        group1.RegisterUser(user4);
        group1.RegisterUser(user5);
        group2.RegisterUser(user6);
        group2.RegisterUser(user7);

        user1.Send("hi @all in Group 1!");
        user2.Send("hi Dexter in Group 1!");
        user3.Send("hi @all in Group 2!");
        user4.Send("hi David in Group 2!");

        user2.DeleteUser();
        user2.Send("this user cannot send messages now.");
    }
}
public interface IMediator
{
    void SendMessage(string message, Colleague colleague);
    void SendPrivateMessage(string message, Colleague sender, Colleague receiver);
    void DeleteUser(Colleague colleague);
    bool IsUserRegistered(Colleague colleague);
}
public abstract class Colleague
{
    protected IMediator _mediator;
    public Colleague(IMediator mediator)
    {
        _mediator = mediator;
    }
    public abstract void ReceiveMessage(string message);
}
public class ChatMediator : IMediator
{
    private List<Colleague> _colleagues;
    public ChatMediator()
    {
        _colleagues = new List<Colleague>();
    }
    public void RegisterColleague(Colleague colleague)
    {
        _colleagues.Add(colleague);
        Console.WriteLine($"\n{((User)colleague).Name} joined the chat.");
    }
    public void SendMessage(string message, Colleague sender)
    {
        foreach (var colleague in _colleagues)
        {
            if (colleague != sender)
            {
                colleague.ReceiveMessage(message);
            }
        }
    }
    public void SendPrivateMessage(string message, Colleague sender, Colleague receiver)
    {
        receiver.ReceiveMessage($"[private msg from {((User)sender).Name}]: {message}");
    }
    public void DeleteUser(Colleague colleague)
    {
        _colleagues.Remove(colleague);
        Console.WriteLine($"\n{((User)colleague).Name} has disconnected from the chat.");
    }
    public bool IsUserRegistered(Colleague colleague)
    {
        return _colleagues.Contains(colleague);
    }
}
public class User : Colleague
{
    private string _name;
    public string Name => _name;
    public User(IMediator mediator, string name) : base(mediator)
    {
        _name = name;
    }
    public void Send(string message)
    {
        if (!_mediator.IsUserRegistered(this))
        {
            Console.WriteLine($"\n{_name} is removed from chat.");
            return;
        }
        Console.WriteLine($"{_name} sends: {message}");
        _mediator.SendMessage(message, this);
    }
    public void SendPrivateMessage(User receiver, string message)
    {
        if (!_mediator.IsUserRegistered(this))
        {
            Console.WriteLine($"\n{_name} is removed from chat.");
            return;
        }
        Console.WriteLine($"{_name} sends a private msg: {message}");
        _mediator.SendPrivateMessage(message, this, receiver);
    }
    public override void ReceiveMessage(string message)
    {
        Console.WriteLine($"{_name} received: {message}");
    }
    public void DeleteUser()
    {
        _mediator.DeleteUser(this);
    }
}
public class ChatGroup
{
    public ChatMediator Mediator { get; }
    private List<User> _users;
    private string _name;
    public ChatGroup(string name)
    {
        _name = name;
        Mediator = new ChatMediator();
        _users = new List<User>();
    }
    public void RegisterUser(User user)
    {
        _users.Add(user);
        Mediator.RegisterColleague(user);
    }
}
