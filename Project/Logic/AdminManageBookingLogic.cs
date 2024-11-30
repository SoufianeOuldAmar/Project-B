public static class AdminManageBookingLogic
{
    public static List<BookedFlightsModel> AdminSearchBooking(string email)
    {
        var BookdeFlight = BookedFlightsAccess.LoadAll();
        if (BookdeFlight.ContainsKey(email))
        {
            return BookdeFlight[email];
        }
        return null;
    }

}