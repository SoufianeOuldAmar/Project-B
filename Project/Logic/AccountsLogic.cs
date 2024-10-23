public static class AccountsLogic
{
    public enum CreateAccountStatus
    {
        IncorrectFullName,
        IncorrectEmail,
        IncorrectPassword,
        CorrectCredentials
    }

    private static List<AccountModel> _accounts = AccountsAccess.LoadAll();
    static public AccountModel? CurrentAccount { get; private set; }

    public static void UpdateList(AccountModel acc)
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

        AccountsAccess.WriteAll(_accounts);
    }

    public static string CreateAccount(string fullName, string email, string password)
    {
        List<CreateAccountStatus> statusList = CheckCreateAccount(fullName, email, password);

        if (statusList.Count == 0)
        {
            int id = _accounts.Count + 1; // Use the next id
            AccountModel account = new AccountModel(id, email, password, fullName, false);
            UpdateList(account);
            return "\nAccount created successfully!";
        }
        else
        {
            string errorMessages = "\nError messages!:\n";
            foreach (var item in statusList)
            {
                if (item == CreateAccountStatus.IncorrectFullName)
                {
                    errorMessages += "Full name is incorrect. Please enter a valid name.\n";
                }

                if (item == CreateAccountStatus.IncorrectEmail)
                {
                    errorMessages += "Email is incorrect. Please enter a valid email.\n";
                }

                if (item == CreateAccountStatus.IncorrectPassword)
                {
                    errorMessages += "Password is too short. It must be at least 5 characters.\n";
                }
            }

            return errorMessages;
        }
    }

    public static List<CreateAccountStatus> CheckCreateAccount(string fullName, string email, string password)
    {
        List<CreateAccountStatus> statusList = new List<CreateAccountStatus>();

        bool hasNonLetters = fullName.Any(c => !char.IsLetter(c) && c != ' ');
        bool hasAtSymbol = email.Contains("@");
        bool hasMoreThanFiveChar = password.Length >= 5;

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

        return statusList;
    }

    public static AccountModel GetById(int id)
    {
        return _accounts.Find(i => i.Id == id);
    }

    public static AccountModel CheckLogin(string email, string password)
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
