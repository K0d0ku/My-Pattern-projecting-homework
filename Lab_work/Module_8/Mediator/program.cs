/*output:
alisa otpravlyaet privatnoe soobshenie: Privet bob!

Log: 10/23/2024 9:17:43 PM: Log:[ private msg from alisa]: Privet bob!

bob poluchil soobhsenie: Log:[private msg from alisa]: Privet bob!

bob otpravlyaet privatnoe soobshenie: Privet alisa!

Log: 10/23/2024 9:17:43 PM: Log:[ private msg from bob]: Privet alisa!

alisa poluchil soobhsenie: Log:[private msg from bob]: Privet alisa!

alisa otprovlyaet soobshenie: hi @all Group 1!

Log: 10/23/2024 9:17:43 PM: Log: User sent: hi @all Group 1!

bob otprovlyaet soobshenie: hi dexter in Group 1!

Log: 10/23/2024 9:17:43 PM: Log: User sent: hi dexter in Group 1!

charli otprovlyaet soobshenie: hi @all Group 2!

Log: 10/23/2024 9:17:43 PM: Log: User sent: hi @all Group 2!

john otprovlyaet soobshenie: hi david in Group 2!

Log: 10/23/2024 9:17:43 PM: Log: User sent: hi david in Group 2!

dexter poluchil soobhsenie: hi david in Group 2!

bob has disconnected from the chat.
bob otprovlyaet soobshenie: this user now cant send msg.
Log: 10/23/2024 9:17:43 PM: Log: User sent: this user now cant send msg.*/

/*BTW the logging system only shows in output the latest logs , and when checking the log file that i uploaded there might be more than 1 log*/

using System;
public class progarm
{
    public static void Main(string[] args)
    {
        // Создаем посредника
        ChatMediator chatMediator = new ChatMediator();

        // Создаем участников
        User user1 = new User(chatMediator, "alisa");
        User user2 = new User(chatMediator, "bob");
        User user3 = new User(chatMediator, "charli");

        /*// Регистрируем участников в чате
        chatMediator.RegisterColleague(user1);
        chatMediator.RegisterColleague(user2);
        chatMediator.RegisterColleague(user3);

        // Участники обмениваются сообщениями
        user1.Send("privet vsem!");
        user2.Send("privet alisa!");
        user3.Send("vsem privet!");*/

        /*my code*/
        user1.SendPrivateMessage(user2, "Privet bob!\n");
        user2.SendPrivateMessage(user1, "Privet alisa!\n");

        ChatGroup group1 = new ChatGroup("Group 1");
        ChatGroup group2 = new ChatGroup("Group 2");

        User user4 = new User(group1.Mediator, "john");
        User user5 = new User(group1.Mediator, "dexter");
        User user6 = new User(group2.Mediator, "morgan");
        User user7 = new User(group2.Mediator, "david");

        group1.RegisterUser(user4);
        group1.RegisterUser(user5);
        group2.RegisterUser(user6);
        group2.RegisterUser(user7);

        user1.Send("hi @all Group 1!\n");
        user2.Send("hi dexter in Group 1!\n");

        user3.Send("hi @all Group 2!\n");
        user4.Send("hi david in Group 2!\n");

        user2.DeleteUser();
        user2.Send("this user now cant send msg.");
    }
}
public interface IMediator
{
    void SendMessage(string message, Colleague colleague);
    void SendPrivateMessage(string message, Colleague sender, Colleague receiver);
    void DeleteUser(Colleague colleague);
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
    private string _logFilePath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\lab\8\logs\chat_log.txt";
    public ChatMediator()
    {
        _colleagues = new List<Colleague>();
    }
    public void RegisterColleague(Colleague colleague)
    {
        _colleagues.Add(colleague);
    }
    public void SendMessage(string message, Colleague sender)
    {
        LogAction($"Log: {sender} sent: {message}"); /*logs public msg*/
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
        LogAction($"Log:[ private msg from {((User)sender).Name}]: {message}"); /*logs private msg*/
        receiver.ReceiveMessage($"Log:[private msg from {((User)sender).Name}]: {message}");
    }
    private void LogAction(string message)
    {
        string logMessage = $"Log: {DateTime.Now}: {message}";
        Console.WriteLine(logMessage); /* prints log to console,
                                        i added this myself cuz i did not wanted to open a log file*/
        File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
    }
    public void DeleteUser(Colleague colleague)
    {
        _colleagues.Remove(colleague);
        Console.WriteLine($"{((User)colleague).Name} has disconnected from the chat.");
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
        Console.WriteLine($"{_name} otprovlyaet soobshenie: {message}");
        _mediator.SendMessage(message, this);
    }
    /*my code*/
    public void SendPrivateMessage(User receiver, string message)
    {
        Console.WriteLine($"{_name} otpravlyaet privatnoe soobshenie: {message}");
        _mediator.SendPrivateMessage(message, this, receiver);
    }

    public override void ReceiveMessage(string message)
    {
        Console.WriteLine($"{_name} poluchil soobhsenie: {message}");
    }
    /*my code*/
    public void DeleteUser()
    {
        _mediator.DeleteUser(this);
    }
}
/*my code*/
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
