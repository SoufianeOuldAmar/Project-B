using System.Collections.Concurrent;

static class AdminAccountPresentation
{
    static AdminAccountLogic logic = new AdminAccountLogic();
    public static void Start()
    {
        AdminAccountModel email = SearchByEmail();
        if (email != null)
        {
            Console.WriteLine("Welcome!");
        }
        else
        {

            Console.WriteLine("Wrong Email");
        }
    }

    // public static void ShowFlightnformation(AccountModel account)
    // {
    //     Console.WriteLine($"ID: {account.Id}");
    //     Console.WriteLine($"ID: {account.EmailAddress}");
    //     Console.WriteLine($"ID: {account.Password}");
    //     Console.WriteLine($"ID: {account.FullName}");

    // }
    public static AdminAccountModel SearchByEmail()
    {
        Console.WriteLine("Enter Your Email: ");
        string email = Console.ReadLine();
        AdminAccountModel account = logic.GetByEmail(email);
        if (email == null)
        {
            Console.WriteLine("Wrong Email");
        }
        return account;


    }
}