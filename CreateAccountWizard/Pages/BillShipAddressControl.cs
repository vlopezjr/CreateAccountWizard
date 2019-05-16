using System;
using System.Linq;
using System.Windows.Forms;
using CPUserControls.AddressModule;
using CPUserControls.UPSOnline;
using CreateCustomer.API.DomainServices;
using CPUserControls.CASalesTax;
using CPUserControls.WASalesTax;
using System.Drawing;
using CreateCustomer.API.Entities;
using CPUserControls.Services;

namespace CreateAccountWizard.Pages
{
    public partial class BillShipAddressControl : UserControl
    {
        //PUBLIC EVENTS
        public delegate void DoneEventHandler(BLAddress address);
        public event DoneEventHandler Done = delegate { };

        public event Action Invalid = delegate { };
        public event Action EnterPressed = delegate { };

        //PUBLIC FIELDS
        public Address PrimaryAddress { get; set; }
        public Address Address { get; set; }
        public int PrimTaxKey { get; set; }
        public int TaxKey { get; set; }
        public bool PrimIsGovernment { get; set; }
        public bool AddrIsGovernment { get; set; }
        public bool AddrSameAsPrimary { get; set; }

        private AddressControl addressControl;
        private BLAddress address;
        private bool loading;

        public void SetStatusLabel(Color labelColor, string statusMessage)
        {
            lblStatus.ForeColor = labelColor;
            lblStatus.Text = statusMessage;
        }

        public BillShipAddressControl()
        {
            InitializeComponent();
        }





        //CONTROL LOAD
        private void BillingAndShippingAddressControl_Load(object sender, EventArgs e)
        {
            address = new BLAddress(new TaxService(), new ValidationService());

            address.IsSameAsPrimary = AddrSameAsPrimary;
            address.IsValidated = address.IsSameAsPrimary;

            if (address.IsSameAsPrimary)
                MapAddressToBLAddress(PrimaryAddress, address, "Primary");
            else
                MapAddressToBLAddress(Address, address, "Billing");

            loading = true;
            chkSameAsPrim.Checked = address.IsSameAsPrimary;
            loading = false;

            CreateAddressControlForBilling();

            Done(address);
        }


        private void MapAddressToBLAddress(Address sourceAddress, BLAddress destinationAddress, string sourceAddrType)
        {
            destinationAddress.Data.Name = sourceAddress.Name ?? "";
            destinationAddress.Data.Line1 = sourceAddress.Line1 ?? "";
            destinationAddress.Data.Line2 = sourceAddress.Line2 ?? "";
            destinationAddress.Data.City = sourceAddress.City ?? "";
            destinationAddress.Data.Zip = sourceAddress.Zip;
            destinationAddress.Data.State = sourceAddress.State ?? "";
            destinationAddress.Data.Country = string.IsNullOrEmpty(sourceAddress.Country) ? "US" : sourceAddress.Country;
            destinationAddress.Data.Residential = sourceAddress.Residential;

            destinationAddress.SplitZipCode();

            switch (sourceAddrType)
            {
                case "Primary":
                    destinationAddress.SalesTaxKey = PrimTaxKey;
                    destinationAddress.IsGovernment = PrimIsGovernment;
                    break;

                case "Billing":
                    destinationAddress.SalesTaxKey = TaxKey;
                    destinationAddress.IsGovernment = AddrIsGovernment;
                    break;
            }
        }

        private void CreateAddressControlForBilling()
        {
            addressControl = new AddressControl
            {
                Dock = DockStyle.Fill,
                CoName = address.Data.Name,
                Line1 = address.Data.Line1,
                Line2 = address.Data.Line2,
                City = address.Data.City,
                State = address.Data.State,
                ZipCode = address.Data.Zip,
                Country = address.Data.Country,
                TaxKey = address.SalesTaxKey,
                Residential = address.Data.Residential,
                IsGovernment = address.IsGovernment
            };

            addressControl.HideCustId();
            addressControl.HideStatus();

            addressControl.Done += AddressControl_Done;
            addressControl.Invalid += AddressControl_Invalid;
            addressControl.FormCleared += () => chkSameAsPrim.Checked = false;
            addressControl.EnterPressed += () => EnterPressed();

            panelAddress.Controls.Clear();
            panelAddress.Controls.Add(addressControl);
        }







        //USER CONTROL EVENT HANDLERS
        private void AddressControl_Invalid()
        {
            Invalid();
            loading = true;
            chkSameAsPrim.Checked = false;
            loading = false;
        }

        private void AddressControl_Done(BLAddress billingAddr)
        {
            address = billingAddr;
            Done(address);

            lblStatus.ForeColor = Color.Green;
            lblStatus.Text = "Address have been validated.";
        }





        //CHECK CHANGED HANDLER
        private void chkSameAsPrim_CheckedChanged(object sender, EventArgs e)
        {
            if (loading) return;
            if (chkSameAsPrim.Checked)
            {
                MapAddressToBLAddress(PrimaryAddress, address, "Primary");
                address.IsSameAsPrimary = true;
                address.IsValidated = true;

                addressControl.CoName = address.Data.Name;
                addressControl.Line1 = address.Data.Line1;
                addressControl.Line2 = address.Data.Line2;
                addressControl.City = address.Data.City;
                addressControl.State = address.Data.State;
                addressControl.ZipCode = address.Data.Zip;
                addressControl.Country = address.Data.Country;
                addressControl.TaxKey = address.SalesTaxKey;
                addressControl.IsGovernment = address.IsGovernment;
                addressControl.Refresh();

                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = "Address has been set to same as primary";

                if (address.IsValidated && address.SalesTaxKey != 0)
                    Done(address);
            }
            else
            {
                addressControl.ClearForm();
                address.IsSameAsPrimary = false;

                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "Address cleared. Please enter address";

                Invalid();
            }
        }
    }
}
