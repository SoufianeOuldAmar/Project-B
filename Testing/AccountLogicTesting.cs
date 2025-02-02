namespace Testing;

[TestClass]
public class TestAccountLogic
{
    [TestMethod]
    public void TestLogin()
    {
        var existingAccount = UserAccountLogic.CheckLogin("n@b.c", "xyz");
        var nonExistentAccount = UserAccountLogic.CheckLogin("iemand@outlook.com", "password");

        Assert.IsNotNull(existingAccount, "Account should not be null");
        Assert.IsNull(nonExistentAccount, "Account should be null");
    }

    [TestMethod]
    public void TestCreateAccount()
    {

        // Expected result for a successful account creation
        List<UserAccountLogic.CreateAccountStatus> statusListEmpty = new List<UserAccountLogic.CreateAccountStatus>();
        var successfulAccount = UserAccountLogic.CheckCreateAccount("Soufiane Ould Amar", "soufiane_ouldamar@outlook.com", "password");

        // Expected result for an unsuccessful account creation due to incorrect full name
        List<UserAccountLogic.CreateAccountStatus> statusListFullNameWrong = new List<UserAccountLogic.CreateAccountStatus>
        {
            UserAccountLogic.CreateAccountStatus.IncorrectFullName
        };
        var unsuccessfulFullNameAccount = UserAccountLogic.CheckCreateAccount("123", "iemand_anders@outlook.com", "password");

        // Expected result for an unsuccessful account creation due to incorrect email
        List<UserAccountLogic.CreateAccountStatus> statusListEmailWrong = new List<UserAccountLogic.CreateAccountStatus>
        {
            UserAccountLogic.CreateAccountStatus.IncorrectEmail
        };
        var unsuccessfulEmailAccount = UserAccountLogic.CheckCreateAccount("Iemand", "iemand", "password");

        // Expected result for an unsuccessful account creation due to incorrect password
        List<UserAccountLogic.CreateAccountStatus> statusListPasswordWrong = new List<UserAccountLogic.CreateAccountStatus>
        {
            UserAccountLogic.CreateAccountStatus.IncorrectPassword
        };
        var unsuccessfulPasswordAccount = UserAccountLogic.CheckCreateAccount("Iemand", "iemand@outlook.com", "pass");

        // Expected result for an unsuccessful account creation due to email already existing.
        List<UserAccountLogic.CreateAccountStatus> statusListEmailExists = new List<UserAccountLogic.CreateAccountStatus>
        {
            UserAccountLogic.CreateAccountStatus.EmailExists
        };
        var unsuccessfulEmailExistsAccount = UserAccountLogic.CheckCreateAccount("Iemand", "n@b.c", "password");

        // Use CollectionAssert to compare the contents of the lists
        CollectionAssert.AreEqual(statusListEmpty, successfulAccount, "statusListEmpty should be empty.");
        CollectionAssert.AreEqual(statusListFullNameWrong, unsuccessfulFullNameAccount, "statusListFullNameWrong should not be empty.");
        CollectionAssert.AreEqual(statusListEmailWrong, unsuccessfulEmailAccount, "statusListEmailWrong should not be empty.");
        CollectionAssert.AreEqual(statusListPasswordWrong, unsuccessfulPasswordAccount, "statusListPasswordWrong should not be empty.");
        CollectionAssert.AreEqual(statusListEmailExists, unsuccessfulEmailExistsAccount, "statusListEmailExists should not be empty.");

    }


}
