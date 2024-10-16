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

    public void UpdateList(AdminAccountModel acc)
    {

        int index = _accounts.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {

            _accounts[index] = acc;
        }
        else
        {
            //add new model
            _accounts.Add(acc);
        }
        AdminAccountAccess.WriteAll(_accounts);

    }
    public AdminAccountModel GetByEmail(string email)
    {
        return _accounts.Find(i => i.EmailAddress.ToLower() == email.ToLower());
    }

}