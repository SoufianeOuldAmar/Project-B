using System.Collections.Generic;

public static class AccountsLogic
{
    public enum CreateAccountStatus
    {
        IncorrectFullName,
        IncorrectEmail,
        IncorrectPassword,
        EmailExists,
        CorrectCredentials
    }

    public static List<UserAccountModel> _accounts = DataAccessClass.ReadList<UserAccountModel>("DataSources/accounts.json");
    static public UserAccountModel? CurrentAccount { get; private set; }

    public static void UpdateList(UserAccountModel acc)
    {
        int index = _accounts.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {
            // Update existing model
            _accounts[index] = acc;
        }
        else
        {
            // Add new model
            _accounts.Add(acc);
        }

        DataAccessClass.WriteList<UserAccountModel>("DataSources/accounts.json", _accounts);

    }

    public static List<CreateAccountStatus> CheckCreateAccount(string fullName, string email, string password)
    {
        List<CreateAccountStatus> statusList = new List<CreateAccountStatus>();

        bool hasNonLetters = fullName.Any(c => !char.IsLetter(c) && c != ' ');
        bool hasAtSymbol = email.Contains("@");
        bool hasMoreThanFiveChar = password.Length >= 5;
        bool EmailExists = false;

        foreach (var account in _accounts)
        {
            if (email == account.EmailAddress)
            {
                EmailExists = true;
            }
        }

        if (hasNonLetters)
        {
            statusList.Add(CreateAccountStatus.IncorrectFullName);
        }
        if (!hasAtSymbol)
        {
            statusList.Add(CreateAccountStatus.IncorrectEmail);
        }
        if (!hasMoreThanFiveChar)
        {
            statusList.Add(CreateAccountStatus.IncorrectPassword);
        }
        if (EmailExists)
        {
            statusList.Add(CreateAccountStatus.EmailExists);
        }


        return statusList;
    }

    public static UserAccountModel GetByEmail(string email)
    {
        return _accounts.Find(i => i.EmailAddress == email);
    }

    public static List<string> GetAllEmails()
    {
        List<string> allEmails = new List<string>();

        foreach (var item in _accounts)
        {
            allEmails.Add(item.EmailAddress);
        }

        return allEmails;
    }

    public static UserAccountModel CheckLogin(string email, string password)
    {
        if (email == null || password == null)
        {
            return null;
        }

        CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password);
        return CurrentAccount;
    }

    public static bool? TryLogInAgain(string input)
    {
        if (input.ToLower() == "yes") return true;
        else if (input.ToLower() == "no") return false;
        else return null;
    }
}
