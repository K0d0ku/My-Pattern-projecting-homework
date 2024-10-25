/*output:
Alisa has connected.
Bob has connected.
Charli has connected.
Alisa sends: hello everyone!
Bob received: hello everyone!
Charli received: hello everyone!
Bob sends: hello Alisa!
Alisa received: hello Alisa!
Charli received: hello Alisa!
Charli sends: Hi all!
Alisa received: Hi all!
Bob received: Hi all!
Alisa sends a private msg: Hey Bob!
Bob received: [private from Alisa]: Hey Bob!
Bob sends a private msg: Hi Alisa!
Alisa received: [private from Bob]: Hi Alisa!
John has connected.
Dexter has connected.
Morgan has connected.
David has connected.
Alisa sends: hi @all in group 1!
Bob received: hi @all in group 1!
Charli received: hi @all in group 1!
John received: hi @all in group 1!
Dexter received: hi @all in group 1!
Morgan received: hi @all in group 1!
David received: hi @all in group 1!
John sends: hello David in group 2!
Alisa received: hello David in group 2!
Bob received: hello David in group 2!
Charli received: hello David in group 2!
Dexter received: hello David in group 2!
Morgan received: hello David in group 2!
David received: hello David in group 2!
Bob is now blocked.
Bob sends: cant sent msg cuz he is blocked!
Bob has disconnected from the chat.*/
using System;
using System.Collections.Generic;
/*i just rewrote this code from home work module 8 template*/
public class program
{
    public static void Main(string[] args)
    {
        ChatMediator chatMediator = new ChatMediator();

        User user1 = new User(chatMediator, "Alisa");
        User user2 = new User(chatMediator, "Bob");
        User user3 = new User(chatMediator, "Charli");

        chatMediator.RegisterUser(user1);
        chatMediator.RegisterUser(user2);
        chatMediator.RegisterUser(user3);

        user1.Send("hello everyone!");
        user2.Send("hello Alisa!");
        user3.Send("Hi all!");

        user1.SendPrivateMessage(user2, "Hey Bob!");
        user2.SendPrivateMessage(user1, "Hi Alisa!");

        ChatGroup group1 = new ChatGroup("group 1", chatMediator);
        ChatGroup group2 = new ChatGroup("group 2", chatMediator);

        User user4 = new User(group1.Mediator, "John");
        User user5 = new User(group1.Mediator, "Dexter");
        User user6 = new User(group2.Mediator, "Morgan");
        User user7 = new User(group2.Mediator, "David");

        group1.RegisterUser(user4);
        group1.RegisterUser(user5);
        group2.RegisterUser(user6);
        group2.RegisterUser(user7);

        user1.Send("hi @all in group 1!");
        user4.Send("hello David in group 2!");

        Admin admin = new Admin(chatMediator);
        admin.BlockUser(user2);
        user2.Send("cant sent msg cuz he is blocked!");

        user2.DeleteUser();
    }
}
public interface IMediator
{
    void SendMessage(string message, Colleague sender);
    void SendPrivateMessage(string message, Colleague sender, Colleague receiver);
    void DeleteUser(Colleague colleague);
    void BlockUser(Colleague colleague);
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
    public void RegisterUser(Colleague colleague)
    {
        _colleagues.Add(colleague);
        Console.WriteLine($"{((User)colleague).Name} has connected.");
    }
    public void SendMessage(string message, Colleague sender)
    {
        foreach (var colleague in _colleagues)
        {
            if (colleague != sender && !((User)sender).IsBlocked)
            {
                colleague.ReceiveMessage(message);
            }
        }
    }
    public void SendPrivateMessage(string message, Colleague sender, Colleague receiver)
    {
        if (!((User)sender).IsBlocked)
        {
            receiver.ReceiveMessage($"[private from {((User)sender).Name}]: {message}");
        }
    }
    public void DeleteUser(Colleague colleague)
    {
        _colleagues.Remove(colleague);
        Console.WriteLine($"{((User)colleague).Name} has disconnected from the chat.");
    }
    public void BlockUser(Colleague colleague)
    {
        ((User)colleague).IsBlocked = true;
        Console.WriteLine($"{((User)colleague).Name} is now blocked.");
    }
}
public class User : Colleague
{
    private string _name;
    public string Name => _name;
    public bool IsBlocked { get; set; } = false;
    public User(IMediator mediator, string name) : base(mediator)
    {
        _name = name;
    }
    public void Send(string message)
    {
        Console.WriteLine($"{_name} sends: {message}");
        _mediator.SendMessage(message, this);
    }
    public void SendPrivateMessage(User receiver, string message)
    {
        Console.WriteLine($"{_name} sends a private msg: {message}");
        _mediator.SendPrivateMessage(message, this, receiver);
    }
    public override void ReceiveMessage(string message)
    {
        if (!IsBlocked)
        {
            Console.WriteLine($"{_name} received: {message}");
        }
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
    public ChatGroup(string name, ChatMediator mediator)
    {
        _name = name;
        Mediator = mediator;
        _users = new List<User>();
    }
    public void RegisterUser(User user)
    {
        _users.Add(user);
        Mediator.RegisterUser(user);
    }
}
public class Admin
{
    private IMediator _mediator;
    public Admin(IMediator mediator)
    {
        _mediator = mediator;
    }
    public void BlockUser(Colleague colleague)
    {
        _mediator.BlockUser(colleague);
    }
}
