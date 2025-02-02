using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

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
        return FinancialAccount.Any(x => x.UserName == username && x.Password == password);
    }


    public FinancialAccountModel GetFinancialAccountByUsername(string username)
    {
        return FinancialAccount.FirstOrDefault(x => x.UserName.ToLower() == username.ToLower());
        
    }
}
