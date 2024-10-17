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
            Console.WriteLine("Login as Admin successful. Welcome!");

        }
        else
        {
            Console.WriteLine("Invalid email or password. Please try again.");

        }


    }

}