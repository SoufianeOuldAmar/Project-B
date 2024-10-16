namespace Testing;

[TestClass]
public class TestAccountLogic
{
    [TestMethod]
    public void TestLogin()
    {   
        AccountsLogic accountsLogic = new AccountsLogic();
        var existingAccount = accountsLogic.CheckLogin("n@b.c", "xyz");
        var nonExistentAccount = accountsLogic.CheckLogin("iemand@outlook.com", "password");

        Assert.IsNotNull(existingAccount, "Account should not be null");
        Assert.IsNull(nonExistentAccount, "Account should be null");
    }

    [TestMethod]
    public void TestCreateAccount()
    {   
        AccountsLogic accountsLogic = new AccountsLogic();

        // Expected result for a successful account creation
        List<AccountsLogic.CreateAccountStatus> statusListEmpty = new List<AccountsLogic.CreateAccountStatus>();
        var successfulAccount = accountsLogic.CheckCreateAccount("Soufiane Ould Amar", "soufiane_ouldamar@outlook.com", "password");

        // Expected result for an unsuccessful account creation due to incorrect full name
        List<AccountsLogic.CreateAccountStatus> statusListFullNameWrong = new List<AccountsLogic.CreateAccountStatus>
        {
            AccountsLogic.CreateAccountStatus.IncorrectFullName
        };
        var unsuccessfulFullNameAccount = accountsLogic.CheckCreateAccount("123", "iemand_anders@outlook.com", "password");

        // Expected result for an unsuccessful account creation due to incorrect email
        List<AccountsLogic.CreateAccountStatus> statusListEmailWrong = new List<AccountsLogic.CreateAccountStatus>
        {
            AccountsLogic.CreateAccountStatus.IncorrectEmail
        };
        var unsuccessfulEmailAccount = accountsLogic.CheckCreateAccount("Iemand", "iemand", "password");

        // Expected result for an unsuccessful account creation due to incorrect password
        List<AccountsLogic.CreateAccountStatus> statusListPasswordWrong = new List<AccountsLogic.CreateAccountStatus>
        {
            AccountsLogic.CreateAccountStatus.IncorrectPassword
        };
        var unsuccessfulPasswordAccount = accountsLogic.CheckCreateAccount("Iemand", "iemand@outlook.com", "pass");

        // Use CollectionAssert to compare the contents of the lists
        CollectionAssert.AreEqual(statusListEmpty, successfulAccount, "statusListEmpty should be empty.");
        CollectionAssert.AreEqual(statusListFullNameWrong, unsuccessfulFullNameAccount, "statusListFullNameWrong should not be empty.");
        CollectionAssert.AreEqual(statusListEmailWrong, unsuccessfulEmailAccount, "statusListEmailWrong should not be empty.");
        CollectionAssert.AreEqual(statusListPasswordWrong, unsuccessfulPasswordAccount, "statusListPasswordWrong should not be empty.");
    }
}