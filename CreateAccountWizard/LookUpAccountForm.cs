using CPUserControls.AddressModule;
using CreateCustomer.API.DomainServices;
using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateAccountWizard
{
    public partial class LookUpAccountForm : Form
    {
        public delegate void DoneEventHandler(string custId);
        public event DoneEventHandler DoubleClicked = delegate { };

        private bool allowDoubleClick;

        public LookUpAccountForm()
        {
            InitializeComponent();
        }

        public void EnableDoubleClick()
        {
            allowDoubleClick = true;
        }


        private void ResizeForm(UserControl userControl)
        {
            int width = (userControl as AddressDataGridView).GetColumnWidth();
            width += 55;
            Size = new Size(width, Height);
        }

        private void txtZipCode_TextChanged(object sender, EventArgs e)
        {
            txtPhone.TextChanged -= txtPhone_TextChanged;

            txtPhone.Text = "";
            btnSearch.Enabled = txtZipCode.Text.Length >= 3;

            txtPhone.TextChanged += txtPhone_TextChanged;
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            txtPhone.Text = txtPhone.Text.Replace(" ", "");

            txtZipCode.TextChanged -= txtZipCode_TextChanged;

            txtZipCode.Text = "";
            btnSearch.Enabled = txtPhone.Text.Length >= 8;

            txtZipCode.TextChanged += txtZipCode_TextChanged;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            bool searchingByPhone = txtZipCode.Text == "";

            if (searchingByPhone)
            {
                Cursor = Cursors.WaitCursor;

                var service = new CustomerService();
                var customers = service.GetCustomersByPhone(txtPhone.Text);

                var addresses = new List<BLAddress>();

                foreach (var currentCustomer in customers)
                {
                    var currentAddresses = new List<BLAddress>();

                    foreach (var custAddr in currentCustomer.CustAddresses)
                    {
                        if (custAddr.ShipDays < 90)
                            currentAddresses.Add(new BLAddress { Data = custAddr.Address, CustId = currentCustomer.Id });
                    }

                    addresses.AddRange(await QualifyAddressesAsync(currentAddresses, currentCustomer));
                }

                addresses = addresses
                     .OrderBy(c => c.CustId)
                    .ThenByDescending(c => c.IsPrimaryAddress)
                    .ThenByDescending(c => c.IsDefaultShipping)
                    .ThenBy(c => c.Data.Line1).ToList();

                SetUpDataGrid(addresses);
                SetCountLabel(addresses.Count);

                Cursor = Cursors.Arrow;
            }
            else
            {
                Cursor = Cursors.WaitCursor;

                var service = new CustomerService();
                var addresses = await service.GetAddressesByZipCodeAsync(txtZipCode.Text.TrimEnd());

                var blAddresses = new List<BLAddress>();

                foreach (var address in addresses)
                {
                    if (address.CustAddress == null) continue;
                    if (address.CustAddress.ShipDays > 90) continue;

                    var customer = address.CustAddress.Customer;

                    var currentAddress = new BLAddress { Data = address, CustId = customer.Id };

                    QualifySingleAddress(ref currentAddress, customer);

                    blAddresses.Add(currentAddress);
                }

                blAddresses = blAddresses
                    .OrderBy(c => c.CustId)
                    .ThenByDescending(c => c.IsPrimaryAddress)
                    .ThenByDescending(c => c.IsDefaultShipping)
                    .ThenBy(c => c.Data.Line1).ToList();

                SetUpDataGrid(blAddresses);
                SetCountLabel(blAddresses.Count);

                Cursor = Cursors.Arrow;
            }

        }

        private void SetCountLabel(int count)
        {
            lblCount.ForeColor = count == 0 ? Color.Red : Color.Green;
            lblCount.Text = "Results: " + count.ToString();
        }

        private void SetUpDataGrid(List<BLAddress> addresses)
        {
            var addressDataGridView = new AddressDataGridView { Dock = DockStyle.Fill };
            addressDataGridView.ShowCustIDColumn();
            addressDataGridView.Initialize(addresses);
            addressDataGridView.EscapePressed += () => Close();
            addressDataGridView.CellContentDoubleClicked += AddressDataGridView_CellContentDoubleClicked;

            panelAddress.Controls.Clear();
            panelAddress.Controls.Add(addressDataGridView);
        }

        private void AddressDataGridView_CellContentDoubleClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (!allowDoubleClick) return;
            var dgv = (DataGridView)sender;
            if (dgv.Rows.Count == 0) return;
            if (dgv.SelectedRows.Count == 0) return;
            if (dgv.SelectedRows[0] == null) return;

            var currentRow = dgv.SelectedRows[0];
            var custId = currentRow.Cells["CustID"].Value.ToString();
            DoubleClicked(custId);
            Close();
        }

        private async Task<List<BLAddress>> QualifyAddressesAsync(List<BLAddress> addresses, Customer customer)
        {
            await Task.Run(() =>
            {
                foreach (var addr in addresses)
                {
                    string type = addr.Type == null ? "" : addr.Type;

                    if (customer != null && string.IsNullOrEmpty(type))
                    {
                        if (addr.Data.Key == customer.DfltShipToAddrKey)
                        {
                            type = type.Insert(0, "Ship");
                            addr.IsDefaultShipping = true;
                        }
                        else
                            addr.IsDefaultShipping = false;

                        if (addr.Data.Key == customer.PrimaryAddrKey)
                        {
                            type = type.Insert(0, "Bill");
                            addr.IsPrimaryAddress = true;
                        }
                        else
                            addr.IsPrimaryAddress = false;

                        if (type.Length == 0)
                        {
                            type = "CSA";
                        }

                        addr.Type = type;
                    }
                }
            });

            return addresses;
        }

        private void QualifySingleAddress(ref BLAddress addr, Customer customer)
        {
            string type = addr.Type == null ? "" : addr.Type;

            if (customer != null && string.IsNullOrEmpty(type))
            {
                if (addr.Data.Key == customer.DfltShipToAddrKey)
                {
                    type = type.Insert(0, "Ship");
                    addr.IsDefaultShipping = true;
                }
                else
                    addr.IsDefaultShipping = false;

                if (addr.Data.Key == customer.DfltBillToAddrKey)
                {
                    type = type.Insert(0, "Bill");
                    addr.IsPrimaryAddress = true;
                    addr.IsDefaultBilling = true;
                }
                else
                {
                    addr.IsDefaultBilling = false;
                    addr.IsPrimaryAddress = false;
                }

                if (type.Length == 0)
                {
                    type = "CSA";
                }

                addr.Type = type;
            }
        }

        private void txtPhone_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void txtZipCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }

            if(e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void btnSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void LookUpAccountForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
