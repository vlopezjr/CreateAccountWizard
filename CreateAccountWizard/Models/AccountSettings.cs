using CreateCustomer.API.Entities;

namespace CreateAccountWizard.Models
{
    public class AccountSettings
    {
        public int CreditLimitUsed { get; set; }
        public string PricePackSlip { get; set; }
        public string UserFld3 { get; set; } //invoice copies - 1 or 99 used mainly for diff types of accounts
        public CustClass CustClass { get; set; }

        public AccountSettings()
        {
            CustClass = new CustClass();
        }
    }
}
