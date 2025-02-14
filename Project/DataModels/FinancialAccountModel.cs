public class FinancialAccountModel : AccountModel, IDataModel
{
    public string UserName { get; }

    public FinancialAccountModel(int id, string username, string password) : base(id, password)
    {
        UserName = username;
    }
}