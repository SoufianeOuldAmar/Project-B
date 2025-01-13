public static class FlightPointsLogic
{
    public static double CalculateFlightPoint(int amountFlightPoints, double totalPrice, int allFlightPoints)
    {
        double maxDiscount = totalPrice * 0.2;
        double discountToApply = Math.Min(amountFlightPoints, Math.Min(maxDiscount, allFlightPoints));

        return discountToApply;
    }
}