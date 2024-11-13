using BankingApp;

namespace Bankingapp
{
    public class User
    {
        private string UserName { get; set; }
        private string Password { get; set; }
        private List<Account> accounts = new List<Account>();

        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        
        
        public string GetUserName()
        {
            return UserName;
        }
        
        public void SetPassword(string newPassword)
        {
            Password = newPassword;
        }
        public string GetPassword()
        {
            return Password;
        }

        public List<Account> GetAccounts() {
            return accounts;
        }
        public void AddAccount(Account account) { 
            accounts.Add(account);
        }
    }


}
