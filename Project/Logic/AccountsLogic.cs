using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{
    public enum CreateAccountStatus
    {
        IncorrectFullName,
        IncorrectEmail,
        IncorrectPassword,
        CorrectCredentials
    }

    private List<AccountModel> _accounts;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public AccountModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        _accounts = AccountsAccess.LoadAll();
    }


    public void UpdateList(AccountModel acc)
    {
        //Find if there is already an model with the same id
        int index = _accounts.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {
            //update existing model
            _accounts[index] = acc;
        }
        else
        {
            //add new model
            _accounts.Add(acc);
        }
        AccountsAccess.WriteAll(_accounts);

    }

    public string CreateAccount(string fullName, string email, string password)
    {
        List<CreateAccountStatus> statusList = CheckCreateAccount(fullName, email, password);

        if (statusList.Count == 0)
        {
            int id = _accounts.Count + 1;
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
            }

            return errorMessages;
        }
    }


    public List<CreateAccountStatus> CheckCreateAccount(string fullName, string email, string password)
    {
        List<CreateAccountStatus> statusList = new List<CreateAccountStatus>();

        bool hasNonLetters = fullName.Any(c => !char.IsLetter(c) && c != ' ');
        bool hasAtSymbol = email.Contains("@");
        bool hasMoreThanFiveChar = password.Length >= 5;


        if (hasNonLetters)
        {
            statusList.Add(CreateAccountStatus.IncorrectFullName); // Return status for invalid full name
        }
        if (!hasAtSymbol)
        {
            statusList.Add(CreateAccountStatus.IncorrectEmail); // Return status for invalid email
        }
        if (!hasMoreThanFiveChar)
        {
            statusList.Add(CreateAccountStatus.IncorrectPassword); // Return status for invalid password
        }

        return statusList; // Return status for successful account creation
    }


    public AccountModel GetById(int id)
    {
        return _accounts.Find(i => i.Id == id);
    }

    public AccountModel CheckLogin(string email, string password)
    {
        if (email == null || password == null)
        {
            return null;
        }
        CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password);
        return CurrentAccount;
    }

    public bool? TryLogInAgain(string input)
    {
        if (input.ToLower() == "yes") return true;
        else if (input.ToLower() == "no") return false;
        else return null;
    }
}




