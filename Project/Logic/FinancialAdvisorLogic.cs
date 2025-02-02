using System;
using System.Collections.Generic;
using System.Dynamic;

public class FinancialAdvisorLogic
{
    private List<FinancialAccountModel> FinancialAccount {get; set;}


    public FinancialAdvisorLogic()
    {

        FinancialAccount = new List<FinancialAccountModel>
        {
            new FinancialAccountModel(1, "Advisor", "xyz")
        };
    }
    public bool ValidateLogin(string username, string password)
    {   
        Console.WriteLine(username + " " + FinancialAccount[0].UserName);
        Console.WriteLine(password + " " + FinancialAccount[0].Password);

        return FinancialAccount.Any(x => x.UserName == username && x.Password == password);
    }

    public FinancialAccountModel GetFinancialAccountByUsername(string username)
    {
        return FinancialAccount.FirstOrDefault(x => x.UserName.ToLower() == username.ToLower());
    }
}
