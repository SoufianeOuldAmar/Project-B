﻿using System.Collections.Generic;

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

    public static List<AccountModel> _accounts = DataAccessClass.ReadList<AccountModel>("DataSources/accounts.json");
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

        DataAccessClass.WriteList<AccountModel>("DataSources/accounts.json", _accounts);

    }

    public static string CreateAccount(string fullName, string email, string password)
    {
        List<CreateAccountStatus> statusList = CheckCreateAccount(fullName, email, password);

        if (statusList.Count == 0)
        {
            int id = _accounts.Count + 1; // Use the next id
            AccountModel account = new AccountModel(id, email, password, fullName);
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

                if (item == CreateAccountStatus.EmailExists)
                {
                    errorMessages += "Email already exists. Use another email.\n";
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

    public static AccountModel GetByEmail(string email)
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
