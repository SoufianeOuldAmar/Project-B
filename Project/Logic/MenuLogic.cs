public static class MenuLogic
{
    public static void PushMenu(Action menu)
    {
        MenuPresentation.menuStack.Push(menu);
    }

    public static void PopMenu()
    {
        if (MenuPresentation.menuStack.Count > 0)
            MenuPresentation.menuStack.Pop();
    }
}
