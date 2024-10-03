public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}
public interface IUserRepos
{
    void SaveToDatabase();
}
public class DatabaseRepos : IUserRepos
{
    public void SaveToDatabase()
    {
        // Код для сохранения пользователя в базу данных
    }
}
public interface IEmailService
{
    void SendEmail();
}
public class EmailService : IEmailService
{
    public void SendEmail()
    {
        // Код для отправки электронного письма пользователю
    }
}
public interface IPrintAddressLabels
{
    void PrintAddressLabel();
}
public class PrintAdress : IPrintAddressLabels
{
    public void PrintAddressLabel()
    {
        // Код для печати адресного ярлыка
    }
}
