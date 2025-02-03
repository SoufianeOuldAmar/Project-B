using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
public class AdminAccountLogic
{
    private List<AdminAccountModel> _accounts1 = new List<AdminAccountModel>();
    public AdminAccountLogic()
    {
        _accounts1 = DataAccessClass.ReadList<AdminAccountModel>("DataSources/adminaccount.json");
    }

    public void GetList(AdminAccountModel acc)
    {

        int index = _accounts1.FindIndex(s => s.UserName == acc.UserName);

        if (index != -1)
        {

            _accounts1[index] = acc;
        }
    }
    public bool ValidateLogin(string username, string password)
    {
        AdminAccountModel account = _accounts1.Find(i => i.UserName.ToLower() == username.ToLower());

        if (account != null)
        {
            return true;
        }

        return false;
    }
}
