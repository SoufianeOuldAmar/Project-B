namespace Testing;


[TestClass]
public class FlightPointTests
{
    [TestMethod]
    public void CalculateFlightPoint_ShouldReturnCorrectDiscount_WhenWithinLimits()
    {
        // Arrange
        int amountFlightPoints = 50;
        double totalPrice = 300.0;
        int allFlightPoints = 100;

        // Act
        double discount = FlightPointsLogic.CalculateFlightPoint(amountFlightPoints, totalPrice, allFlightPoints);

        // Assert
        Assert.AreEqual(50, discount, "Discount should be equal to the amount of points used as it's within all limits.");
    }

    [TestMethod]
    public void CalculateFlightPoint_ShouldCapDiscountAt20PercentOfTotalPrice()
    {
        // Arrange
        int amountFlightPoints = 100;
        double totalPrice = 200.0;
        int allFlightPoints = 100;

        // Act
        double discount = FlightPointsLogic.CalculateFlightPoint(amountFlightPoints, totalPrice, allFlightPoints);

        // Assert
        Assert.AreEqual(40, discount, "Discount should be capped at 20% of the total price.");
    }

    [TestMethod]
    public void CalculateFlightPoint_ShouldCapDiscountAtAvailablePoints()
    {
        // Arrange
        int amountFlightPoints = 50;
        double totalPrice = 300.0;
        int allFlightPoints = 30;

        // Act
        double discount = FlightPointsLogic.CalculateFlightPoint(amountFlightPoints, totalPrice, allFlightPoints);

        // Assert
        Assert.AreEqual(30, discount, "Discount should be capped at the available flight points.");
    }
}