public static class Welcome
{
    public static void Message()
    {
        Console.Clear();
        string message = "Welcome to BOSST Airlines";
        string bigText = Figgle.FiggleFonts.Standard.Render(message);
        Random rand = new Random();

        foreach (char c in bigText)
        {
            Console.ForegroundColor = (ConsoleColor)rand.Next(1, 14);
            Console.Write(c);
            Thread.Sleep(2);
        }
        for (int i = 0; i < 7; i++)
        {
            Console.Clear();
            foreach (char c in bigText)
            {
                Console.ForegroundColor = (ConsoleColor)rand.Next(1, 14);
                Console.Write(c);
            }
            Thread.Sleep(300);
            // Thread.Sleep(200);
            // Console.ForegroundColor = (ConsoleColor)rand.Next(1, 14);
            // Console.WriteLine(bigText);
            // Thread.Sleep(200);
        }
        Console.ResetColor();
    }
}