using CPUserControls.AddressModule;
using CPUserControls.ContactModule;
using CreateCustomer.API.Entities;
using CreateAccountWizard.Models;
using CreateAccountWizard.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CreateCustomer.API.DomainServices;
using CreateCustomer.API.Enums;
using System.DirectoryServices.AccountManagement;

namespace CreateAccountWizard
{
    public partial class CreateAccountForm : Form
    {
        public delegate void DoneEventHandler(string custID);
        public event DoneEventHandler Done = delegate { };

        private Customer newCustomer;
        private CustomerService service;

        public CreateAccountForm()
        {
            InitializeComponent();
        }




        //----------------------LOAD-------------------------
        private void CreateAccountWizard_Load(object sender, EventArgs e)
        {
            service = new CustomerService();

            newCustomer = new Customer();
            newCustomer.PrimaryAddress = new Address();
            newCustomer.PrimaryContact = new Contact();
            newCustomer.CustAddresses = new List<CustAddress>();
            newCustomer.DocTransmittals = new List<DocTransmittal>()
            {
                new DocTransmittal{TranType = 501, Customer = newCustomer, EMailFormat = 3, HardCopy = 1},
                new DocTransmittal{TranType = 502, Customer = newCustomer, EMailFormat = 3, HardCopy = 1},
                new DocTransmittal{TranType = 503, Customer = newCustomer, EMailFormat = 3, HardCopy = 1},
                new DocTransmittal{TranType = 505, Customer = newCustomer, EMailFormat = 3, HardCopy = 1},
                new DocTransmittal{TranType = 522, Customer = newCustomer, EMailFormat = 3, HardCopy = 1},
                new DocTransmittal{TranType = 801, Customer = newCustomer, EMailFormat = 3, HardCopy = 1},
                new DocTransmittal{TranType = 835, Customer = newCustomer, EMailFormat = 3, HardCopy = 1},
            };

            NavigateToPrimaryAddressPage();
            btnNext.Enabled = false;
        }


        private bool isAuthorizedUser;
        private void ValidateNonAuthorizedUsers()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string userId = userName.Substring(userName.IndexOf(@"\") + 1);

            using (var context = new PrincipalContext(ContextType.Domain, "caseparts"))
            {
                using (var group = GroupPrincipal.FindByIdentity(context, "LA_AcctsReceivable"))
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, userId);

                    if (user != null)
                    {
                        isAuthorizedUser = user.IsMemberOf(group);
                    }
                    else
                    {
                        isAuthorizedUser = false;
                    }
                }
            }
        }



        #region NAVIGATION
        //----------------------NAVIGATION-------------------------
        int count = 0;
        private void btnNext_Click(object sender, EventArgs e)
        {

            switch (count)
            {
                case (int)Page.PrimaryAddress:
                    newCustomer.PrimaryAddress = SetDefaultAddressProperties(newCustomer.PrimaryAddress);

                    var whseTerr = GetWarehouseAndTerritory(newCustomer.PrimaryAddress.State);
                    newCustomer.CustAddresses.First(c => c.Type == CustAddrType.Primary).WhseKey = whseTerr.Item1;
                    newCustomer.CustAddresses.First(c => c.Type == CustAddrType.Primary).SalesTerritoryKey = whseTerr.Item2;

                    NavigateToContactPage();
                    lblAccountSearch.Visible = true;
                    break;

                case (int)Page.PrimaryContact:
                    SetHiddenContactProperties();
                    NavigateToSettingsPage();
                    btnNext.Text = "Save && Close";
                    break;

                case (int)Page.Settings:
                    SaveCustomer();
                    break;
            }

            count++;
            btnBack.Enabled = true;
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            switch (count)
            {
                case (int)Page.PrimaryContact:
                    NavigateToPrimaryAddressPage();
                    lblAccountSearch.Visible = true;
                    btnBack.Enabled = false;
                    break;

                case (int)Page.Settings:
                    NavigateToContactPage();
                    btnNext.Text = "Next";
                    break;
            }

            count--;
            btnNext.Enabled = true;
        }

        #endregion






        #region SETTING DEFAULT PROPERTIES
        private void ResizeForm(UserControl userControl)
        {
            int widthDiff = userControl.Width - mainPanel.Width;
            int heightDiff = userControl.Height - mainPanel.Height;

            Width += widthDiff;
            Height += heightDiff;
        }


        private void SetHiddenContactProperties()
        {
            newCustomer.PrimaryContact.CreateDate = DateTime.Now;
            newCustomer.PrimaryContact.CreateUserID = Environment.UserName;
            newCustomer.PrimaryContact.EMailFormat = 3;
            newCustomer.PrimaryContact.EntityType = 501; //customer
            newCustomer.PrimaryContact.Module = "AR";
            newCustomer.PrimaryContact.CCCreditMemo = 0;
            newCustomer.PrimaryContact.CCCustStmnt = 0;
            newCustomer.PrimaryContact.CCDebitMemo = 0;
            newCustomer.PrimaryContact.CCEFTRemittance = 0;
            newCustomer.PrimaryContact.CCFinanceCharge = 0;
            newCustomer.PrimaryContact.CCInvoice = 0;
            newCustomer.PrimaryContact.CCPurchaseOrder = 0;
            newCustomer.PrimaryContact.CCRMA = 0;
            newCustomer.PrimaryContact.CCSalesOrder = 0;
            newCustomer.PrimaryContact.Deleted = 0;
            newCustomer.PrimaryContact.CCCreditMemo = 0;
            newCustomer.PrimaryContact.UpdateCounter = 0;

            //set properties to blank to avoid adding nulls in database
            newCustomer.PrimaryContact.Email = newCustomer.PrimaryContact.Email ?? "";
            newCustomer.PrimaryContact.PhoneExt = newCustomer.PrimaryContact.PhoneExt ?? "";
            newCustomer.PrimaryContact.Fax = newCustomer.PrimaryContact.Fax ?? "";
            newCustomer.PrimaryContact.FaxExt = newCustomer.PrimaryContact.FaxExt ?? "";
            newCustomer.PrimaryContact.MobilePhone = newCustomer.PrimaryContact.MobilePhone ?? "";
        }

        private Address SetDefaultAddressProperties(Address address)
        {
            address.Line2 = address.Line2 ?? "";
            address.Line3 = address.Line3 ?? "";
            address.Line4 = address.Line4 ?? "";
            address.Line5 = address.Line5 ?? "";
            address.Phone = address.Phone ?? "";
            address.PhoneExt = address.PhoneExt ?? "";
            address.MobilePhone = address.MobilePhone ?? "";

            address.TransactionOverride = 0;
            address.UpdateCounter = 0;

            return address;
        }

        private Tuple<int, int> GetWarehouseAndTerritory(string state)
        {
            var lookupService = new LookUpService();
            var branchID = lookupService.GetBranchIDByState(state);
            var warehouses = lookupService.GetBranches();

            var whseKey = warehouses.FirstOrDefault(wh => wh.BranchID == branchID).Key;
            var salesTerritoryKey = warehouses.FirstOrDefault(wh => wh.BranchID == branchID).SalesTerritoryKey;

            return new Tuple<int, int>(whseKey, salesTerritoryKey);
        }
        #endregion






        private void SaveCustomer()
        {
            try
            {
                service.AddCustomerWithDependencies(newCustomer);
                Done(newCustomer.Id);
                Close();
            }
            catch(Exception ex)
            {
                EmailSender.EmailError(ex);
                MessageBox.Show("An error has occured while saving new customer. An email has been sent to the development team. Please restart application.");
            }
        }





        #region PRIMARY ADDRESS
        //-------------------------PRIMARY ADDRESS-------------------------------
        private void NavigateToPrimaryAddressPage()
        {
            string companyName = newCustomer.Name ?? "";
            string custId = newCustomer.Id ?? "";
            string line1 = newCustomer.PrimaryAddress.Line1 ?? "";
            string line2 = newCustomer.PrimaryAddress.Line2 ?? "";
            string city = newCustomer.PrimaryAddress.City ?? "";
            string state = newCustomer.PrimaryAddress.State ?? "";
            string country = newCustomer.PrimaryAddress.Country ?? "";
            string zip = newCustomer.PrimaryAddress.Zip ?? "";
            short residential = newCustomer.PrimaryAddress.Residential;

            var primaryCustAddr = newCustomer.CustAddresses.FirstOrDefault(c => c.Type == CustAddrType.Primary);

            int taxRateKey = primaryCustAddr == null ? 0 : primaryCustAddr.STaxSchdKey == null ? 0 : (int)primaryCustAddr.STaxSchdKey;

            var addressPage = new AddressControl()
            {
                Dock = DockStyle.Fill,
                ZipCode = zip,
                CoName = companyName,
                CustId = custId,
                Line1 = line1,
                Line2 = line2,
                City = city,
                State = state,
                Country = country,
                TaxKey = taxRateKey,
                Residential = residential,
                IsGovernment = primaryIsGov
            };

            addressPage.Done += AddressPage_Done;
            addressPage.Invalid += () => btnNext.Enabled = false;
            addressPage.EnterPressed += () => btnNext.PerformClick();

            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(addressPage);

            lblCurrent.Text = "Billing Address";
        }


        bool primaryIsGov;
        private void AddressPage_Done(BLAddress address)
        {
            btnNext.Enabled = true;

            newCustomer.Id = address.CustId;
            newCustomer.Name = address.Data.Name;
            newCustomer.PrimaryAddress.Name = address.Data.Name;
            newCustomer.PrimaryAddress.Line1 = address.Data.Line1;
            newCustomer.PrimaryAddress.Line2 = address.Data.Line2;
            newCustomer.PrimaryAddress.City = address.Data.City;
            newCustomer.PrimaryAddress.State = address.Data.State;
            newCustomer.PrimaryAddress.Zip = address.Data.Zip;
            newCustomer.PrimaryAddress.Country = address.Data.Country;
            newCustomer.PrimaryAddress.Residential = address.Data.Residential;

            var primaryCustAddr = newCustomer.CustAddresses.FirstOrDefault(c => c.Type == CustAddrType.Primary);
            if (primaryCustAddr == null)
            {
                primaryCustAddr = new CustAddress { Type = CustAddrType.Primary };
                newCustomer.CustAddresses.Add(primaryCustAddr);
            }

            primaryCustAddr.STaxSchdKey = address.SalesTaxKey;
            primaryIsGov = address.IsGovernment;
        }
        #endregion









        #region PRIMARY CONTACT
        //----------------------------PRIMARY CONTACT---------------------------------
        private void NavigateToContactPage()
        {
            string name = newCustomer.PrimaryContact.Name ?? "";
            string email = newCustomer.PrimaryContact.Email ?? "";
            string phone = newCustomer.PrimaryContact.Phone ?? "";
            string phoneExt = newCustomer.PrimaryContact.PhoneExt ?? "";
            string fax = newCustomer.PrimaryContact.Fax ?? "";
            string faxExt = newCustomer.PrimaryContact.FaxExt ?? "";
            string cell = newCustomer.PrimaryContact.MobilePhone ?? "";
            string title = newCustomer.PrimaryContact.Title ?? "";

            var contactControl = new ContactControl
            {
                Dock = DockStyle.Fill,
                FullName = name,
                Email = email,
                Phone = phone,
                PhoneExt = phoneExt,
                Fax = fax,
                FaxExt = faxExt,
                Cell = cell,
                Title = title
            };

            contactControl.Done += (contact) =>
            {
                newCustomer.PrimaryContact = contact;
                btnNext.Enabled = true;
            };

            contactControl.EnterPressed += () => btnNext.PerformClick();
            contactControl.Invalid += () => btnNext.Enabled = false;
            contactControl.SetStatus(Color.Red, "Please enter the primary contact.");

            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(contactControl);
            btnNext.Enabled = string.IsNullOrEmpty(newCustomer.PrimaryContact.Name) && string.IsNullOrEmpty(newCustomer.PrimaryContact.Phone) ? false : true;

            lblCurrent.Text = "Primary Contact";
        }
        #endregion







        #region ACCOUNT SETTINGS
        //----------------------------ACCOUNT SETTINGS---------------------------------
        private void NavigateToSettingsPage()
        {
            var primaryCustAddr = newCustomer.CustAddresses.FirstOrDefault(c => c.Type == CustAddrType.Primary);

            int? shipMethodKey = primaryCustAddr.ShipMethKey;
            int? packListFormKey = primaryCustAddr.PackListFormKey;
            int? paymentTermsKey = primaryCustAddr.PmtTermsKey;

            var settingsControl = new AccountSettingsControl
            {
                Dock = DockStyle.Fill,
                CustClassKey = newCustomer.CustClassKey,
                PaymentTermsKey = paymentTermsKey,
                CreditLimit = newCustomer.CreditLimit,
                ShipMethodKey = shipMethodKey,
                PricePackingSlip = newCustomer.UserFld2,
                ReqPO = newCustomer.ReqPO,
            };

            settingsControl.Done += settingsControl_Done;
            settingsControl.Invalid += () => btnNext.Enabled = false;
            settingsControl.EnterPressed += () => btnNext.PerformClick();

            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(settingsControl);

            lblCurrent.Text = "Account Settings";
        }

        private void settingsControl_Done(AccountSettings accountSettings)
        {
            var primaryCustAddr = newCustomer.CustAddresses.FirstOrDefault(c => c.Type == CustAddrType.Primary);

            primaryCustAddr.CustAddrID = newCustomer.Id;

            MapCustAddrSettings(ref primaryCustAddr, accountSettings);
            MapCustomerSettings(ref newCustomer, accountSettings);

            SetCustAddrDefaultSettings(ref primaryCustAddr);
            SetCustomerDefaultSettings(ref newCustomer);


            //removed the ability to add billing and shipping addresses on creation.
            //will be managed in the maintain customer form instead.

            //if (!billingSameAsPrimary)
            //{
            //    var billToCustAddr = newCustomer.CustAddresses.First(c => c.Type == CustAddrType.BillTo);

            //    MapCustAddrSettings(ref billToCustAddr, accountSettings);
            //    SetCustAddrDefaultSettings(ref billToCustAddr);
            //}



            //if (!shippingSameAsPrimary)
            //{
            //    var shipToCustAddr = newCustomer.CustAddresses.FirstOrDefault(c => c.Type == CustAddrType.ShipTo);

            //    MapCustAddrSettings(ref shipToCustAddr, accountSettings);
            //    SetCustAddrDefaultSettings(ref shipToCustAddr);
            //}

            btnNext.Enabled = true;
        }


        private void MapCustAddrSettings(ref CustAddress custAddr, AccountSettings accountSettings)
        {
            custAddr.PmtTermsKey = accountSettings.CustClass.PmtTermsKey;
            custAddr.ShipMethKey = accountSettings.CustClass.ShipMethKey;
            custAddr.InvcFormKey = accountSettings.CustClass.InvcFormKey;

            //not on form
            int key = accountSettings.CustClass.Key;
            custAddr.CustPriceGroupKey = key == 44 ? 7 : key == 40 ? 8 : key == 38 ? 9 : 0;
            custAddr.CurrID = accountSettings.CustClass.CurrID;
            custAddr.FOBKey = accountSettings.CustClass.FOBKey;
            custAddr.LanguageID = accountSettings.CustClass.LanguageID;
            custAddr.RequireSOAck = accountSettings.CustClass.RequireSOAck;
            custAddr.ShipComplete = accountSettings.CustClass.ShipComplete;
            custAddr.ShipLabelFormKey = accountSettings.CustClass.ShipLabelFormKey;
            custAddr.SOAckFormKey = accountSettings.CustClass.SOAckFormKey;
            custAddr.SperKey = accountSettings.CustClass.SperKey;
        }

        private void MapCustomerSettings(ref Customer customer, AccountSettings accountSettings)
        {
            customer.CreditLimit = accountSettings.CustClass.CreditLimit;
            customer.ReqPO = accountSettings.CustClass.ReqPO;
            customer.UserFld2 = accountSettings.PricePackSlip;
            customer.UserFld3 = accountSettings.UserFld3;

            //not on form
            customer.BillingType = accountSettings.CustClass.BillingType;
            customer.CustClassKey = accountSettings.CustClass.Key;
            customer.DfltSalesAcctKey = accountSettings.CustClass.DfltSalesAcctKey;
            customer.FinChgFlatAmt = accountSettings.CustClass.FinChgFlatAmt;
            customer.FinChgPct = accountSettings.CustClass.FinChgPct;
            customer.RetntPct = accountSettings.CustClass.RetntPct;
            customer.TradeDiscPct = accountSettings.CustClass.TradeDiscPct;
        }


        private void SetCustAddrDefaultSettings(ref CustAddress custAddr)
        {
            custAddr.AllowInvtSubst = 1;
            custAddr.BackOrdPrice = 0;
            custAddr.BOLReqd = 0;
            custAddr.CarrierAcctNo = "";
            custAddr.CarrierBillMeth = 6; //default
            custAddr.CloseSOLineOnFirstShip = 0;
            custAddr.CloseSOOnFirstShip = 0;
            custAddr.CreateDate = DateTime.Now;
            custAddr.CreateType = 0;
            custAddr.CreateUserID = Environment.UserName;
            custAddr.FreightMethod = 2; //default
            custAddr.InvcMsg = "";
            custAddr.InvoiceReqd = 0;
            custAddr.InvcFormKey = 79; //printed invoices by default
            custAddr.PackListContentsReqd = 0;
            custAddr.PackListFormKey = 84;
            custAddr.PackListReqd = 0;
            custAddr.PriceAdj = 0;
            custAddr.PriceBase = 0;
            custAddr.PrintOrderAck = 0;
            custAddr.ShipDays = 3; //default
            custAddr.ShipLabelsReqd = 0;
            custAddr.SOAckMeth = 0;
            custAddr.UsePromoPrice = 0;
        }

        private void SetCustomerDefaultSettings(ref Customer customer)
        {
            customer.AcctType = "SA";
            customer.ABANo = null;
            customer.AllowCustRefund = 1;
            customer.AllowWriteOff = 1; //always true
            customer.CreateDate = DateTime.Now;
            customer.CreateType = 1;
            customer.CreateUserID = Environment.UserName;
            customer.CompanyId = "CPC";
            customer.CreditLimitAgeCat = 0; //none
            customer.CreditLimitUsed = 0; //off
            customer.CRMCustID = "";
            customer.DateEstab = DateTime.Now;
            customer.Hold = 0;
            customer.PrintDunnMsg = 0;
            customer.PmtByNationalAcctParent = 0;
            customer.ReqCreditLimit = 0;
            customer.SalesSourceKey = null; //not being used
            customer.ShipPriority = 3; //default
            customer.Status = 1; //default
            customer.StmtFormKey = 78; //default
            customer.StmtCycleKey = 21; //monthly
        }

        #endregion



        private void lblAccountSearch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var lookUpAcctForm = new LookUpAccountForm();
            lookUpAcctForm.ShowDialog();
        }
    }

    internal enum Page
    {
        PrimaryAddress,
        PrimaryContact,
        Settings,
    }
}
