namespace Testing;

[TestClass]
public class TestAccountLogic
{
    [TestMethod]
    public void TestLogin()
    {   
        var existingAccount = AccountsLogic.CheckLogin("n@b.c", "xyz");
        var nonExistentAccount = AccountsLogic.CheckLogin("iemand@outlook.com", "password");

        Assert.IsNotNull(existingAccount, "Account should not be null");
        Assert.IsNull(nonExistentAccount, "Account should be null");
    }

    [TestMethod]
    public void TestCreateAccount()
    {   

        // Expected result for a successful account creation
        List<AccountsLogic.CreateAccountStatus> statusListEmpty = new List<AccountsLogic.CreateAccountStatus>();
        var successfulAccount = AccountsLogic.CheckCreateAccount("Soufiane Ould Amar", "soufiane_ouldamar@outlook.com", "password");

        // Expected result for an unsuccessful account creation due to incorrect full name
        List<AccountsLogic.CreateAccountStatus> statusListFullNameWrong = new List<AccountsLogic.CreateAccountStatus>
        {
            AccountsLogic.CreateAccountStatus.IncorrectFullName
        };
        var unsuccessfulFullNameAccount = AccountsLogic.CheckCreateAccount("123", "iemand_anders@outlook.com", "password");

        // Expected result for an unsuccessful account creation due to incorrect email
        List<AccountsLogic.CreateAccountStatus> statusListEmailWrong = new List<AccountsLogic.CreateAccountStatus>
        {
            AccountsLogic.CreateAccountStatus.IncorrectEmail
        };
        var unsuccessfulEmailAccount = AccountsLogic.CheckCreateAccount("Iemand", "iemand", "password");

        // Expected result for an unsuccessful account creation due to incorrect password
        List<AccountsLogic.CreateAccountStatus> statusListPasswordWrong = new List<AccountsLogic.CreateAccountStatus>
        {
            AccountsLogic.CreateAccountStatus.IncorrectPassword
        };
        var unsuccessfulPasswordAccount = AccountsLogic.CheckCreateAccount("Iemand", "iemand@outlook.com", "pass");

        // Expected result for an unsuccessful account creation due to email already existing.
        List<AccountsLogic.CreateAccountStatus> statusListEmailExists = new List<AccountsLogic.CreateAccountStatus>
        {
            AccountsLogic.CreateAccountStatus.EmailExists
        };
        var unsuccessfulEmailExistsAccount = AccountsLogic.CheckCreateAccount("Iemand", "n@b.c", "password");

        // Use CollectionAssert to compare the contents of the lists
        CollectionAssert.AreEqual(statusListEmpty, successfulAccount, "statusListEmpty should be empty.");
        CollectionAssert.AreEqual(statusListFullNameWrong, unsuccessfulFullNameAccount, "statusListFullNameWrong should not be empty.");
        CollectionAssert.AreEqual(statusListEmailWrong, unsuccessfulEmailAccount, "statusListEmailWrong should not be empty.");
        CollectionAssert.AreEqual(statusListPasswordWrong, unsuccessfulPasswordAccount, "statusListPasswordWrong should not be empty.");
        CollectionAssert.AreEqual(statusListEmailExists, unsuccessfulEmailExistsAccount, "statusListEmailExists should not be empty.");

    }

    
}