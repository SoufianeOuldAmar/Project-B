static class Menu
{
    private static AirplaneLayout airplaneLayout = new AirplaneLayout();
    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        Console.WriteLine("Enter 1 to login");
        Console.WriteLine("Enter 2 to view airplane layout");
        Console.WriteLine("Enter 3 to select a seat");
        Console.WriteLine("Enter 4 to exit");

        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                UserLogin.Start();
                break;
            case "2":
                airplaneLayout.DisplayLayout();
                Start(); // Show menu again after displaying the layout
                break;
            case "3":
                SelectSeat();
                Start(); // Show menu again after selecting a seat
                break;
            case "4":
                Console.WriteLine("Exiting...");
                return;
            default:
                Console.WriteLine("Invalid input");
                Start();
                break;
        }
    }

    private static void SelectSeat()
    {
        Console.WriteLine("\nSelect a seat by entering row number and seat letter (e.g., 12C):");
        string input = Console.ReadLine();
        if (int.TryParse(input.Substring(0, input.Length - 1), out int row) &&
            char.TryParse(input.Substring(input.Length - 1), out char seatLetter))
        {
            airplaneLayout.SelectSeat(row, seatLetter);
        }
        else
        {
            Console.WriteLine("Invalid input format.");
        }
    }
}