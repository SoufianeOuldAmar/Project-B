namespace Testing;

[TestClass]
public class TestAdminAccountLogic
{
    [TestMethod]
    [DataRow(null, "password")]
    [DataRow("BOOST", null)]
    [DataRow(null, null)]
    public void CheckAdminLogin_NullValues_ReturnNull(string username, string password)
    {
        AdminAccountLogic ac = new AdminAccountLogic();
        bool actual = ac.ValidateLogin(username, password);
        Assert.IsFalse(actual);
    }
    [TestMethod]
    [DataRow("BOOST", "airline")]
    [DataRow("sadsa", "dasdasda")]
    [DataRow(null, null)]
    public void CheckAdminLogin_NullValues_ReturnExpected(string username, string password)
    {
        AdminAccountLogic ac = new AdminAccountLogic();
        bool actual = ac.ValidateLogin(username, password);
        if (username == "BOOST" && password == "airline")
        {
            Assert.IsTrue(actual);
        }
        else
        {
            Assert.IsFalse(actual);
        }
    }

}