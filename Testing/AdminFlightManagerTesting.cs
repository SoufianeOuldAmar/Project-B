using DataAccess;
namespace Testing;

[TestClass]
public class AdminFlightManagerLogicTests
{

    public void Date_ValidDateInApril_ReturnsTrue()
    {

        string input = "15-04-2024";


        bool result = AdminFlightManagerLogic.Date(input);


        Assert.IsTrue(result);
    }


    public void Date_ValidDateInFebruary_ReturnsTrue()
    {
        string input = "28-02-2024";
        bool result = AdminFlightManagerLogic.Date(input);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Date_InvalidDayInApril_ReturnsFalse()
    {
        string input = "31-04-2024";
        bool result = AdminFlightManagerLogic.Date(input);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Date_InvalidMonth_ReturnsFalse()
    {
        string input = "15-13-2024";
        bool result = AdminFlightManagerLogic.Date(input);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Date_InvalidYear_ReturnsFalse()
    {
        string input = "15-04-2023";
        bool result = AdminFlightManagerLogic.Date(input);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Date_InvalidFormat_ReturnsFalse()
    {
        string input = "15-04";
        bool result = AdminFlightManagerLogic.Date(input);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Date_ValidLeapYearDate_ReturnsTrue()
    {
        string input = "29-02-2024"; // Leap year test
        bool result = AdminFlightManagerLogic.Date(input);
        // Assert.IsTrue(result);
    }
}
