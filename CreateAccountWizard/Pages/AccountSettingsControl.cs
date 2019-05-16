using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CreateCustomer.API.DomainServices;
using CreateCustomer.API.Entities;
using CreateAccountWizard.Models;
using System.Text.RegularExpressions;

namespace CreateAccountWizard.Pages
{
    public partial class AccountSettingsControl : UserControl
    {
        //PUBLIC EVENTS
        public delegate void DoneEventHandler(AccountSettings accountSettings);
        public event DoneEventHandler Done = delegate { };

        public event Action Invalid = delegate { };
        public event Action EnterPressed = delegate { };

        //PUBLIC FIELDS
        public int CustClassKey { get; set; }
        public int? PaymentTermsKey { get; set; }
        public decimal CreditLimit { get; set; }
        public int? ShipMethodKey { get; set; }
        public string PricePackingSlip { get; set; }
        public short ReqPO { get; set; }
        public string InvoiceCopiesValue { get; set; }



        //PRIVATE FIELDS
        private LookUpService service;
        private List<CustClass> custTypes;
        private List<PaymentTerms> paymentTerms;
        private List<ShipMethod> shipMethods;
        private List<string> invoiceCopies;

        private CustClass custClass;
        private AccountSettings settings;


        public AccountSettingsControl()
        {
            InitializeComponent();
        }




        #region CONTROL LOAD
        //-----------------------LOAD-------------------------
        private void Billing_Load(object sender, EventArgs e)
        {
            settings = new AccountSettings();
            service = new LookUpService();

            var custClasses = service.GetCustClasses();
            custClass = custClasses.FirstOrDefault(c => c.Key == CustClassKey);

            custTypes = service.GetCustClasses();
            paymentTerms = service.GetPaymentTerms();
            shipMethods = service.GetShipMethods();

            //do we need?
            invoiceCopies = service.GetDistinctInvoiceCopies();
            invoiceCopies = invoiceCopies.OrderBy(c => c).ToList();
            invoiceCopies.Insert(0, "");
            invoiceCopies.RemoveAll(c => c == null);

            cboPaymentTerms.DisplayMember = "Id";
            cboPaymentTerms.ValueMember = "Key";
            cboPaymentTerms.DataSource = paymentTerms;

            cboShipMethod.DisplayMember = "ShipMethID";
            cboShipMethod.ValueMember = "Key";
            cboShipMethod.DataSource = shipMethods;

            lblStatus.Text = "Set additional account settings.";
            lblStatus.ForeColor = Color.Red;

            if (CustClassKey == 0)
                rdoEndUser.Checked = true;
            else
                SetControlsFromPassedInFields();

            containerValidator.Validate();
        }

        private void SetControlsFromPassedInFields()
        {
            var classID = custTypes.FirstOrDefault(type => type.Key == CustClassKey).Id.TrimEnd();

            rdoEndUser.Checked = classID == "EndUser" ? true : false;
            rdoDealer.Checked = classID == "Dealer" ? true : false;
            rdoWholesaler.Checked = classID == "Wholesale" ? true : false;

            cboPaymentTerms.SelectedIndex = paymentTerms.FindIndex(c => c.Key == PaymentTermsKey);
            txtCreditLimit.Text = CreditLimit.ToString();
            cboShipMethod.SelectedIndex = shipMethods.FindIndex(meth => meth.Key == ShipMethodKey);

            chkPricePackingSlip.Checked = PricePackingSlip == "No" ? false : true;
            chkPORequired.Checked = ReqPO == 0 ? false : true;
        }

        #endregion





        #region SETTING CUSTOMER DEFAULTS
        //------------------CUSTOMER CLASS----------------------
        private void rdoDealer_CheckedChanged(object sender, EventArgs e)
        {
            SetDefaultSettings("Dealer");

            SetStatusLabel(Color.Green, "Default settings for dealer have been set.");

            containerValidator.Validate();
        }


        private void rdoWholesaler_CheckedChanged(object sender, EventArgs e)
        {
            SetDefaultSettings("Wholesaler");

            SetStatusLabel(Color.Green, "Default settings for wholesaler have been set.");

            containerValidator.Validate();
        }

        private void rdoEndUser_CheckedChanged(object sender, EventArgs e)
        {
            SetDefaultSettings("End User");

            SetStatusLabel(Color.Green, "Default settings for end user have been set.");

            containerValidator.Validate();
        }

        private void SetStatusLabel(Color color, string status)
        {
            lblStatus.Text = status;
            lblStatus.ForeColor = color;
        }

        private void SetDefaultSettings(string accountType)
        {
            CustClass type = custTypes.First(c => c.Name == accountType);

            settings.CustClass = type;
            settings.PricePackSlip = chkPricePackingSlip.Checked ? "Yes" : "No";
            settings.CustClass.StmtFormKey = type.StmtFormKey;
            settings.CustClass.ShipMethKey = type.ShipMethKey;

            cboPaymentTerms.SelectedIndex = paymentTerms.FindIndex(terms => terms.Key == type.PmtTermsKey);
            var periodIndex = type.CreditLimit.ToString().IndexOf('.');
            if (periodIndex == -1)
                txtCreditLimit.Text = "$" + type.CreditLimit.ToString() + ".00";
            else
                txtCreditLimit.Text = "$" + type.CreditLimit.ToString().Substring(0, periodIndex + 3);
            cboShipMethod.SelectedIndex = shipMethods.FindIndex(meth => meth.Key == type.ShipMethKey);
        }
        #endregion






        #region CONTROL STATE CHANGED
        //------------------CONTROL STATE CHANGED-----------------------
        private void UnsubscribeFromSelectionChanged()
        {
            cboPaymentTerms.SelectedIndexChanged -= cboPaymentTerms_SelectedIndexChanged;
            txtCreditLimit.TextChanged -= txtCreditLimit_TextChanged;
            cboShipMethod.SelectedIndexChanged -= cboShipMethod_SelectedIndexChanged;
        }

        private void SubscribeToSelectionChanged()
        {
            cboPaymentTerms.SelectedIndexChanged += cboPaymentTerms_SelectedIndexChanged;
            txtCreditLimit.TextChanged += txtCreditLimit_TextChanged;
            cboShipMethod.SelectedIndexChanged += cboShipMethod_SelectedIndexChanged;
        }

        private void RaiseDoneOrInvalidEvent()
        {
            if (containerValidator.IsValid() && (rdoDealer.Checked || rdoWholesaler.Checked || rdoEndUser.Checked))
            {
                Done(settings);
                lblStatus.Text = "Account settings have been set!";
                lblStatus.ForeColor = Color.Green;

            }
            else
            {
                Invalid();
                lblStatus.Text = "There are still missing required fields.";
                lblStatus.ForeColor = Color.Red;
            }
        }


        private void cboPaymentTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.CustClass.PmtTermsKey = (int)cboPaymentTerms.SelectedValue;
            RaiseDoneOrInvalidEvent();
        }

        private void txtCreditLimit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var input = txtCreditLimit.Text.TrimEnd().Replace("$", "");
                settings.CustClass.CreditLimit = Convert.ToDecimal(input);
                RaiseDoneOrInvalidEvent();
            }
            catch
            {
                settings.CustClass.CreditLimit = 0;
                lblStatus.Text = "Credit limit error. Must be numeric - no commas & no '$' sign.";
                lblStatus.ForeColor = Color.Red;
            }
        }


        private void chkPORequired_CheckedChanged(object sender, EventArgs e)
        {
            settings.CustClass.ReqPO = chkPORequired.Checked ? (short)1 : (short)0;
            RaiseDoneOrInvalidEvent();
        }

        private void chkPricePackingSlip_CheckedChanged(object sender, EventArgs e)
        {
            settings.PricePackSlip = chkPricePackingSlip.Checked ? "Yes" : "No";

            RaiseDoneOrInvalidEvent();
        }

        private void chkInvoiceCopies_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStatements.Checked)
                settings.UserFld3 = "99";
            else
                settings.UserFld3 = "1";
        }

        private void chkPricePackSlip_CheckedChanged(object sender, EventArgs e)
        {
            settings.PricePackSlip = chkPricePackingSlip.Checked ? "Yes" : "No";
        }

        private void cboShipMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.CustClass.ShipMethKey = (int)cboShipMethod.SelectedValue;
            RaiseDoneOrInvalidEvent();
        }

        #endregion

        private void AccountSettingsControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && containerValidator.IsValid())
                EnterPressed();
        }

        private void txtCreditLimit_Enter(object sender, EventArgs e)
        {
            txtCreditLimit.TextChanged -= txtCreditLimit_TextChanged;

            txtCreditLimit.Text = txtCreditLimit.Text.Replace("$", "");
            txtCreditLimit.SelectionStart = 0;
            txtCreditLimit.SelectionLength = txtCreditLimit.Text.Length;

            txtCreditLimit.TextChanged += txtCreditLimit_TextChanged;
        }

        private void txtCreditLimit_Leave(object sender, EventArgs e)
        {
            txtCreditLimit.TextChanged -= txtCreditLimit_TextChanged;

            var inputWithoutCurrencyChar = txtCreditLimit.Text.Replace("$", "");

            var suceeded = decimal.TryParse(inputWithoutCurrencyChar, out decimal creditLimit);
            if (suceeded)
            {
                var periodIndex = creditLimit.ToString().IndexOf('.');
                if (periodIndex == -1)
                    txtCreditLimit.Text = "$" + creditLimit.ToString() + ".00";
                else
                    txtCreditLimit.Text = "$" + creditLimit.ToString().Substring(0, periodIndex + 3);
            }
            txtCreditLimit.TextChanged += txtCreditLimit_TextChanged;

        }
    }
}
