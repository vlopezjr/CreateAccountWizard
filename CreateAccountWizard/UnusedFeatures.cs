using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccountWizard
{
    public class UnusedFeatures
    {
        //This is code to set up bill to / ship addresses in the wizard. 
        //This will handle the controls when navigating to or from these pages.
        //The user control is in the Pages folder.

        #region BILLING ADDRESS
        ////------------------------BILLING ADDRESS -------------------------
        //bool billingIsGov;
        //bool billingSameAsPrimary = true;
        //int billTaxKey;

        //private void NavigateToBillingAddressPage()
        //{
        //    var primaryCustAddr = newCustomer.CustAddresses.FirstOrDefault(c => c.Type == CustAddrType.Primary);
        //    int taxRateKey = primaryCustAddr.STaxSchdKey == null ? 0 : (int)primaryCustAddr.STaxSchdKey;

        //    var billingAddressControl = new BillShipAddressControl()
        //    {
        //        Dock = DockStyle.Fill,
        //        PrimaryAddress = newCustomer.PrimaryAddress,
        //        Address = newCustomer.DefaultBillToAddress ?? null,
        //        TaxKey = billTaxKey,
        //        AddrIsGovernment = billingIsGov,
        //        PrimTaxKey = taxRateKey,
        //        PrimIsGovernment = primaryIsGov,
        //        AddrSameAsPrimary = billingSameAsPrimary,
        //    };

        //    billingAddressControl.SetStatusLabel(Color.Green, "Set billing address here or leave same as primary.");
        //    billingAddressControl.Done += BillingAddressPage_Done;
        //    billingAddressControl.Invalid += BillingAddressControl_Invalid;
        //    billingAddressControl.EnterPressed += () => btnNext.PerformClick();

        //    mainPanel.Controls.Clear();
        //    mainPanel.Controls.Add(billingAddressControl);

        //    lblCurrent.Text = "Default Billing Address";
        //}

        //private void BillingAddressControl_Invalid()
        //{
        //    btnNext.Enabled = false;
        //    var cntrl = mainPanel.Controls.Cast<Control>().FirstOrDefault(c => c.Name.Contains("BillShipAddressControl"));
        //    (cntrl as BillShipAddressControl).SetStatusLabel(Color.Red, "Billing address has been modified. Must be validated to proceed.");
        //}

        //private void BillingAddressPage_Done(BLAddress billingAddress)
        //{
        //    btnNext.Enabled = true;

        //    billingIsGov = billingAddress.IsGovernment;
        //    billTaxKey = billingAddress.SalesTaxKey;
        //    billingSameAsPrimary = billingAddress.IsSameAsPrimary;

        //    if (billingSameAsPrimary)
        //    {
        //        newCustomer.DefaultBillToAddress = null;

        //        var billToCustAddr = newCustomer.CustAddresses.FirstOrDefault(c => c.Type == CustAddrType.BillTo);
        //        if (billToCustAddr != null)
        //        {
        //            newCustomer.CustAddresses.Remove(billToCustAddr);
        //        }
        //    }
        //    else
        //    {
        //        newCustomer.DefaultBillToAddress = new Address();

        //        newCustomer.DefaultBillToAddress.Name = billingAddress.Data.Name;
        //        newCustomer.DefaultBillToAddress.Line1 = billingAddress.Data.Line1;
        //        newCustomer.DefaultBillToAddress.Line2 = billingAddress.Data.Line2;
        //        newCustomer.DefaultBillToAddress.City = billingAddress.Data.City;
        //        newCustomer.DefaultBillToAddress.State = billingAddress.Data.State;
        //        newCustomer.DefaultBillToAddress.Zip = billingAddress.Data.Zip;
        //        newCustomer.DefaultBillToAddress.Country = billingAddress.Data.Country;

        //        newCustomer.DefaultBillToAddress = SetDefaultAddressProperties(newCustomer.DefaultBillToAddress);

        //        var whseTerr = GetWarehouseAndTerritory(newCustomer.DefaultBillToAddress.State);
        //        var billToCustAddr = newCustomer.CustAddresses.FirstOrDefault(c => c.Type == CustAddrType.BillTo);
        //        if (billToCustAddr == null)
        //        {
        //            billToCustAddr = new CustAddress();
        //            billToCustAddr.Type = CustAddrType.BillTo;
        //            billToCustAddr.WhseKey = whseTerr.Item1;
        //            billToCustAddr.SalesTerritoryKey = whseTerr.Item2;
        //            billToCustAddr.STaxSchdKey = billTaxKey;
        //            billToCustAddr.CustAddrID = "";

        //            newCustomer.CustAddresses.Add(billToCustAddr);
        //        }
        //        else
        //        {
        //            billToCustAddr.WhseKey = whseTerr.Item1;
        //            billToCustAddr.SalesTerritoryKey = whseTerr.Item2;
        //            billToCustAddr.STaxSchdKey = billTaxKey;
        //            billToCustAddr.CustAddrID = "";
        //        }
        //    }
        //}

        //#endregion







        //#region SHIPPING ADDRESS
        ////---------------------SHIPPING ADDRESS -------------------------
        //bool shippingIsGov;
        //bool shippingSameAsPrimary = true;
        //int shipTaxKey;

        //private void NavigateToShippingAddressPage()
        //{
        //    var primaryCustAddr = newCustomer.CustAddresses.FirstOrDefault(c => c.Type == CustAddrType.Primary);

        //    int taxRateKey = primaryCustAddr.STaxSchdKey == null ? 0 : (int)primaryCustAddr.STaxSchdKey;

        //    var shippingAddressControl = new BillShipAddressControl()
        //    {
        //        Dock = DockStyle.Fill,
        //        PrimaryAddress = newCustomer.PrimaryAddress,
        //        Address = newCustomer.DefaultShipToAddress ?? null,
        //        TaxKey = shipTaxKey,
        //        AddrIsGovernment = shippingIsGov,
        //        PrimTaxKey = taxRateKey,
        //        PrimIsGovernment = primaryIsGov,
        //        AddrSameAsPrimary = shippingSameAsPrimary,
        //    };

        //    shippingAddressControl.SetStatusLabel(Color.Green, "Set shipping address here or leave same as primary.");
        //    shippingAddressControl.Done += ShippingAddressControl_Done;
        //    shippingAddressControl.Invalid += ShippingAddressControl_Invalid;
        //    shippingAddressControl.EnterPressed += () => btnNext.PerformClick();

        //    mainPanel.Controls.Clear();
        //    mainPanel.Controls.Add(shippingAddressControl);

        //    lblCurrent.Text = "Default Shipping Address";
        //}

        //private void ShippingAddressControl_Invalid()
        //{
        //    btnNext.Enabled = false;
        //    var cntrl = mainPanel.Controls.Cast<Control>().FirstOrDefault(c => c.Name.Contains("BillShipAddressControl"));
        //    (cntrl as BillShipAddressControl).SetStatusLabel(Color.Red, "Shipping address has been modified. Must be validated to proceed.");
        //}

        //private void ShippingAddressControl_Done(BLAddress shippingAddress)
        //{
        //    btnNext.Enabled = true;

        //    shippingIsGov = shippingAddress.IsGovernment;
        //    shipTaxKey = shippingAddress.SalesTaxKey;
        //    shippingSameAsPrimary = shippingAddress.IsSameAsPrimary;

        //    if (shippingSameAsPrimary)
        //    {
        //        newCustomer.DefaultShipToAddress = null;

        //        var shipToCustAddr = newCustomer.CustAddresses.FirstOrDefault(c => c.Type == CustAddrType.ShipTo);
        //        if (shipToCustAddr != null)
        //        {
        //            newCustomer.CustAddresses.Remove(shipToCustAddr);
        //        }
        //    }
        //    else
        //    {
        //        newCustomer.DefaultShipToAddress = new Address();

        //        newCustomer.DefaultShipToAddress.Name = shippingAddress.Data.Name;
        //        newCustomer.DefaultShipToAddress.Line1 = shippingAddress.Data.Line1;
        //        newCustomer.DefaultShipToAddress.Line2 = shippingAddress.Data.Line2;
        //        newCustomer.DefaultShipToAddress.City = shippingAddress.Data.City;
        //        newCustomer.DefaultShipToAddress.State = shippingAddress.Data.State;
        //        newCustomer.DefaultShipToAddress.Zip = shippingAddress.Data.Zip;
        //        newCustomer.DefaultShipToAddress.Country = shippingAddress.Data.Country;

        //        newCustomer.DefaultShipToAddress = SetDefaultAddressProperties(newCustomer.DefaultShipToAddress);

        //        var whseTerr = GetWarehouseAndTerritory(newCustomer.DefaultShipToAddress.State);

        //        var shipToCustAddr = newCustomer.CustAddresses.FirstOrDefault(c => c.Type == CustAddrType.ShipTo);
        //        if (shipToCustAddr == null)
        //        {
        //            shipToCustAddr = new CustAddress();
        //            shipToCustAddr.Type = CustAddrType.ShipTo;
        //            shipToCustAddr.WhseKey = whseTerr.Item1;
        //            shipToCustAddr.SalesTerritoryKey = whseTerr.Item2;
        //            shipToCustAddr.STaxSchdKey = shipTaxKey;
        //            shipToCustAddr.CustAddrID = "";

        //            newCustomer.CustAddresses.Add(shipToCustAddr);
        //        }
        //        else
        //        {
        //            shipToCustAddr.WhseKey = whseTerr.Item1;
        //            shipToCustAddr.SalesTerritoryKey = whseTerr.Item2;
        //            shipToCustAddr.STaxSchdKey = shipTaxKey;
        //            shipToCustAddr.CustAddrID = "";
        //        }
        //    }
        //}
        #endregion

    }
}
