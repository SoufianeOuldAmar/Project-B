using System.Collections.Concurrent;
using System.Data.Common;

static class AdminAccountPresentation
{
    static AdminAccountLogic logic = new AdminAccountLogic();
    // public static void Start()
    // {
    //     Login();

    // }
    public static void Login()
    {
        Console.WriteLine("Enter Your Username: ");
        string username = Console.ReadLine();

        Console.WriteLine("Enter Your password: ");
        string password = Console.ReadLine();

        bool isValid = logic.ValidateLogin(username, password);

        if (isValid)
        {
            Console.WriteLine("Login as Admin successfil. Welcome!");

        }
        else
        {
            Console.WriteLine("Invalid email or password. Please try again.");

        }


    }

}
//     AdminAccountModel email = SearchByEmail();
//     AdminAccountModel password = SearchByEmail();
//     if (email != null && password != null)
//     {
//         Console.WriteLine("Welcome!");
//     }
//     else
//     {

//         Console.WriteLine("Wrong Email");
//     }
// }

// public static void ShowFlightnformation(AccountModel account)
// {
//     Console.WriteLine($"ID: {account.Id}");
//     Console.WriteLine($"ID: {account.EmailAddress}");
//     Console.WriteLine($"ID: {account.Password}");
//     Console.WriteLine($"ID: {account.FullName}");

// }
// public static AdminAccountModel SearchByEmail()
// {
//     Console.WriteLine("Enter Your Email: ");
//     string email = Console.ReadLine();
//     AdminAccountModel account = logic.GetByEmail(email);
//     if (email == null)
//     {
//         Console.WriteLine("Wrong Email");
//     }
//     else
//     {
//         Console.WriteLine("Enter Your password: ");
//         string password = Console.ReadLine();
//         if (password == null)
//         {
//             Console.WriteLine("Wrong Password");
//         }
//     }

//     return account;