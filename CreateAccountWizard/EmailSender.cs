using CPUserControls.CPMail;
using CreateCustomer.API.Entities;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CreateAccountWizard
{
    internal static class EmailSender
    {
        internal static void EmailNonAuthorizedUser(Customer currentCustomer)
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string userId = userName.Substring(userName.IndexOf(@"\") + 1);

            string summary = $"{userId} has created a new account: {currentCustomer.Name} - Customer ID: {currentCustomer.Id}";

            if (Debugger.IsAttached) { MessageBox.Show(summary); return; }

            ServiceSoapClient client = new ServiceSoapClient();
            client.Email("op@caseparts.com", "pams@caseparts.com, valeriem@caseparts.com, dev@caseparts.com", "CreateAccount - Non Authorized User", summary, false, "", "");
        }

        public static void EmailError(Exception ex)
        {
            var mail = new CPMail.ServiceSoapClient();
            var subject = string.Format("Error has occured in Create Account Wizard");
            var summary = string.Format($"Error has occured. Exception details:" +  Environment.NewLine + Environment.NewLine +  
                                          ex.Message +

                                          ex.ToString());

            mail.Email("operations@caseparts.com", "domingog@caseparts.com", subject, summary, false, "", "");
        }
    }
}
