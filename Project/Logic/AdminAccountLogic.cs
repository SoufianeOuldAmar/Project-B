using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
public class AdminAccountLogic
{
    private List<AdminAccountModel> _accounts = new List<AdminAccountModel>();
    public AdminAccountLogic()
    {
        _accounts = AdminAccountAccess.LoadAll();
    }

    public void GetList(AdminAccountModel acc)
    {

        int index = _accounts.FindIndex(s => s.UserName == acc.UserName);

        if (index != -1)
        {

            _accounts[index] = acc;
        }
    }
    public bool ValidateLogin(string username, string password)
    {
        AdminAccountModel account = _accounts.Find(i => i.UserName.ToLower() == username.ToLower());


        if (account != null && account.Password == password)
        {
            return true;
        }

        return false;
    }

}