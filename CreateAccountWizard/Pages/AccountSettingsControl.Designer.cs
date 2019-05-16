namespace CreateAccountWizard.Pages
{
    partial class AccountSettingsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountSettingsControl));
            this.txtCreditLimit = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBoxStatus = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.rdoEndUser = new System.Windows.Forms.RadioButton();
            this.rdoDealer = new System.Windows.Forms.RadioButton();
            this.rdoWholesaler = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPaymentTerms = new System.Windows.Forms.ComboBox();
            this.chkPORequired = new System.Windows.Forms.CheckBox();
            this.chkPricePackingSlip = new System.Windows.Forms.CheckBox();
            this.rfvCreditLimit = new Genghis.Windows.Forms.RequiredFieldValidator();
            this.containerValidator = new Genghis.Windows.Forms.ContainerValidator();
            this.cboShipMethod = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxCredit = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkStatements = new System.Windows.Forms.CheckBox();
            this.groupBoxStatus.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rfvCreditLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.containerValidator)).BeginInit();
            this.groupBoxCredit.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCreditLimit
            // 
            this.txtCreditLimit.Location = new System.Drawing.Point(97, 29);
            this.txtCreditLimit.Name = "txtCreditLimit";
            this.txtCreditLimit.Size = new System.Drawing.Size(121, 20);
            this.txtCreditLimit.TabIndex = 74;
            this.txtCreditLimit.TextChanged += new System.EventHandler(this.txtCreditLimit_TextChanged);
            this.txtCreditLimit.Enter += new System.EventHandler(this.txtCreditLimit_Enter);
            this.txtCreditLimit.Leave += new System.EventHandler(this.txtCreditLimit_Leave);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(7, 32);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(58, 13);
            this.label18.TabIndex = 73;
            this.label18.Text = "Credit Limit";
            // 
            // groupBoxStatus
            // 
            this.groupBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxStatus.Controls.Add(this.lblStatus);
            this.groupBoxStatus.Location = new System.Drawing.Point(-11, 298);
            this.groupBoxStatus.Name = "groupBoxStatus";
            this.groupBoxStatus.Size = new System.Drawing.Size(315, 40);
            this.groupBoxStatus.TabIndex = 89;
            this.groupBoxStatus.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Green;
            this.lblStatus.Location = new System.Drawing.Point(11, 12);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(59, 13);
            this.lblStatus.TabIndex = 15;
            this.lblStatus.Text = "{{ Status }}";
            // 
            // rdoEndUser
            // 
            this.rdoEndUser.AutoSize = true;
            this.rdoEndUser.Location = new System.Drawing.Point(10, 19);
            this.rdoEndUser.Name = "rdoEndUser";
            this.rdoEndUser.Size = new System.Drawing.Size(66, 17);
            this.rdoEndUser.TabIndex = 90;
            this.rdoEndUser.Text = "EndUser";
            this.rdoEndUser.UseVisualStyleBackColor = true;
            this.rdoEndUser.CheckedChanged += new System.EventHandler(this.rdoEndUser_CheckedChanged);
            // 
            // rdoDealer
            // 
            this.rdoDealer.AutoSize = true;
            this.rdoDealer.Location = new System.Drawing.Point(89, 19);
            this.rdoDealer.Name = "rdoDealer";
            this.rdoDealer.Size = new System.Drawing.Size(56, 17);
            this.rdoDealer.TabIndex = 91;
            this.rdoDealer.Text = "Dealer";
            this.rdoDealer.UseVisualStyleBackColor = true;
            this.rdoDealer.CheckedChanged += new System.EventHandler(this.rdoDealer_CheckedChanged);
            // 
            // rdoWholesaler
            // 
            this.rdoWholesaler.AutoSize = true;
            this.rdoWholesaler.Location = new System.Drawing.Point(161, 19);
            this.rdoWholesaler.Name = "rdoWholesaler";
            this.rdoWholesaler.Size = new System.Drawing.Size(78, 17);
            this.rdoWholesaler.TabIndex = 92;
            this.rdoWholesaler.Text = "Wholesaler";
            this.rdoWholesaler.UseVisualStyleBackColor = true;
            this.rdoWholesaler.CheckedChanged += new System.EventHandler(this.rdoWholesaler_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoDealer);
            this.groupBox1.Controls.Add(this.rdoEndUser);
            this.groupBox1.Controls.Add(this.rdoWholesaler);
            this.groupBox1.Location = new System.Drawing.Point(20, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 50);
            this.groupBox1.TabIndex = 93;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 94;
            this.label1.Text = "Payment Terms";
            // 
            // cboPaymentTerms
            // 
            this.cboPaymentTerms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentTerms.FormattingEnabled = true;
            this.cboPaymentTerms.Location = new System.Drawing.Point(97, 55);
            this.cboPaymentTerms.Name = "cboPaymentTerms";
            this.cboPaymentTerms.Size = new System.Drawing.Size(121, 21);
            this.cboPaymentTerms.TabIndex = 95;
            this.cboPaymentTerms.SelectedIndexChanged += new System.EventHandler(this.cboPaymentTerms_SelectedIndexChanged);
            // 
            // chkPORequired
            // 
            this.chkPORequired.AutoSize = true;
            this.chkPORequired.Location = new System.Drawing.Point(15, 65);
            this.chkPORequired.Name = "chkPORequired";
            this.chkPORequired.Size = new System.Drawing.Size(87, 17);
            this.chkPORequired.TabIndex = 103;
            this.chkPORequired.Text = "PO Required";
            this.chkPORequired.UseVisualStyleBackColor = true;
            this.chkPORequired.CheckedChanged += new System.EventHandler(this.chkPORequired_CheckedChanged);
            // 
            // chkPricePackingSlip
            // 
            this.chkPricePackingSlip.AutoSize = true;
            this.chkPricePackingSlip.Location = new System.Drawing.Point(15, 42);
            this.chkPricePackingSlip.Name = "chkPricePackingSlip";
            this.chkPricePackingSlip.Size = new System.Drawing.Size(112, 17);
            this.chkPricePackingSlip.TabIndex = 106;
            this.chkPricePackingSlip.Text = "Price Packing Slip";
            this.chkPricePackingSlip.UseVisualStyleBackColor = true;
            this.chkPricePackingSlip.CheckedChanged += new System.EventHandler(this.chkPricePackingSlip_CheckedChanged);
            // 
            // rfvCreditLimit
            // 
            this.rfvCreditLimit.ControlToValidate = this.txtCreditLimit;
            this.rfvCreditLimit.ErrorMessage = "Cannot be left blank. Mark 0 if no credit limit.";
            this.rfvCreditLimit.Icon = ((System.Drawing.Icon)(resources.GetObject("rfvCreditLimit.Icon")));
            // 
            // containerValidator
            // 
            this.containerValidator.ContainerToValidate = this;
            // 
            // cboShipMethod
            // 
            this.cboShipMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShipMethod.FormattingEnabled = true;
            this.cboShipMethod.Location = new System.Drawing.Point(96, 94);
            this.cboShipMethod.Name = "cboShipMethod";
            this.cboShipMethod.Size = new System.Drawing.Size(121, 21);
            this.cboShipMethod.TabIndex = 137;
            this.cboShipMethod.SelectedIndexChanged += new System.EventHandler(this.cboShipMethod_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 136;
            this.label3.Text = "Ship Via";
            // 
            // groupBoxCredit
            // 
            this.groupBoxCredit.Controls.Add(this.cboPaymentTerms);
            this.groupBoxCredit.Controls.Add(this.label1);
            this.groupBoxCredit.Controls.Add(this.txtCreditLimit);
            this.groupBoxCredit.Controls.Add(this.label18);
            this.groupBoxCredit.Location = new System.Drawing.Point(20, 59);
            this.groupBoxCredit.Name = "groupBoxCredit";
            this.groupBoxCredit.Size = new System.Drawing.Size(244, 99);
            this.groupBoxCredit.TabIndex = 142;
            this.groupBoxCredit.TabStop = false;
            this.groupBoxCredit.Text = "Credit";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkStatements);
            this.groupBox3.Controls.Add(this.cboShipMethod);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.chkPORequired);
            this.groupBox3.Controls.Add(this.chkPricePackingSlip);
            this.groupBox3.Location = new System.Drawing.Point(21, 167);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(243, 128);
            this.groupBox3.TabIndex = 144;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // chkStatements
            // 
            this.chkStatements.AutoSize = true;
            this.chkStatements.Location = new System.Drawing.Point(15, 19);
            this.chkStatements.Name = "chkStatements";
            this.chkStatements.Size = new System.Drawing.Size(79, 17);
            this.chkStatements.TabIndex = 138;
            this.chkStatements.Text = "Statements";
            this.chkStatements.UseVisualStyleBackColor = true;
            this.chkStatements.CheckedChanged += new System.EventHandler(this.chkInvoiceCopies_CheckedChanged);
            // 
            // AccountSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBoxCredit);
            this.Controls.Add(this.groupBoxStatus);
            this.Controls.Add(this.groupBox1);
            this.Name = "AccountSettingsControl";
            this.Size = new System.Drawing.Size(286, 327);
            this.Load += new System.EventHandler(this.Billing_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AccountSettingsControl_KeyDown);
            this.groupBoxStatus.ResumeLayout(false);
            this.groupBoxStatus.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rfvCreditLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.containerValidator)).EndInit();
            this.groupBoxCredit.ResumeLayout(false);
            this.groupBoxCredit.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtCreditLimit;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBoxStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.RadioButton rdoEndUser;
        private System.Windows.Forms.RadioButton rdoDealer;
        private System.Windows.Forms.RadioButton rdoWholesaler;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPaymentTerms;
        private System.Windows.Forms.CheckBox chkPORequired;
        private System.Windows.Forms.CheckBox chkPricePackingSlip;
        private Genghis.Windows.Forms.RequiredFieldValidator rfvCreditLimit;
        private Genghis.Windows.Forms.ContainerValidator containerValidator;
        private System.Windows.Forms.ComboBox cboShipMethod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBoxCredit;
        private System.Windows.Forms.CheckBox chkStatements;
    }
}
