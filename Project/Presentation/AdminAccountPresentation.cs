using System.Collections.Concurrent;
using System.Data.Common;
using System.Threading;
static class AdminAccountPresentation
{
    static AdminAccountLogic logic = new AdminAccountLogic();
    public static void Login()
    {
        int i = 0;
        while (true)
        {

            Console.WriteLine("Enter Your Username: ");
            string username = Console.ReadLine();

            Console.WriteLine("Enter Your password: ");
            string password = Console.ReadLine();

            bool isValid = logic.ValidateLogin(username, password);

            if (isValid)
            {
                Console.WriteLine("Login as Admin successful. Welcome!");
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("Enter q to logout");
                string input = Console.ReadLine().ToLower();
                if (input == "q")
                {
                    Console.WriteLine("You logged out");
                    break;
                }

            }
            else
            {
                i++;
                Console.WriteLine("Invalid email or password. Please try again.");
                if (i >= 3)
                {
                    Console.WriteLine("You will be locked out for 1 minute due to multiple failed attempts.");
                    Thread.Sleep(60000);
                    i = 0;
                }

            }
        }

    }

}