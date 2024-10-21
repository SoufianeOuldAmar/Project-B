static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        Console.WriteLine("Enter 1 to login");
        Console.WriteLine("Enter 2 to do something else in the future");

        string input = Console.ReadLine();
        if (input == "1")
        {
            Console.WriteLine("Are you Admin or User? A/U");
            string input1 = Console.ReadLine().ToLower();
            if (input1 == "u")
            {
                MenuPresentation.Start();
            }
            else if (input1 == "a")
            {
                AdminAccountPresentation.Login();
            }
            else
            {
                Console.WriteLine("Invalid input");
                Start();
            }
        }
        else if (input == "2")
        {
            Console.WriteLine("This feature is not yet implemented");

        }
        else
        {
            Console.WriteLine("Invalid input");
            Start();
        }

    }
}