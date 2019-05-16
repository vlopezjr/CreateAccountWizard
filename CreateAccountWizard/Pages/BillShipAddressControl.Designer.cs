namespace CreateAccountWizard.Pages
{
    partial class BillShipAddressControl
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
            this.groupBoxStatus = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelAddress = new System.Windows.Forms.Panel();
            this.chkSameAsPrim = new System.Windows.Forms.CheckBox();
            this.groupBoxStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxStatus
            // 
            this.groupBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxStatus.Controls.Add(this.lblStatus);
            this.groupBoxStatus.Location = new System.Drawing.Point(-14, 282);
            this.groupBoxStatus.Name = "groupBoxStatus";
            this.groupBoxStatus.Size = new System.Drawing.Size(344, 42);
            this.groupBoxStatus.TabIndex = 68;
            this.groupBoxStatus.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Green;
            this.lblStatus.Location = new System.Drawing.Point(15, 11);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(59, 13);
            this.lblStatus.TabIndex = 15;
            this.lblStatus.Text = "{{ Status }}";
            // 
            // panelAddress
            // 
            this.panelAddress.Location = new System.Drawing.Point(6, 23);
            this.panelAddress.Name = "panelAddress";
            this.panelAddress.Size = new System.Drawing.Size(294, 244);
            this.panelAddress.TabIndex = 74;
            // 
            // chkSameAsPrim
            // 
            this.chkSameAsPrim.AutoSize = true;
            this.chkSameAsPrim.Checked = true;
            this.chkSameAsPrim.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSameAsPrim.Location = new System.Drawing.Point(107, 3);
            this.chkSameAsPrim.Name = "chkSameAsPrim";
            this.chkSameAsPrim.Size = new System.Drawing.Size(104, 17);
            this.chkSameAsPrim.TabIndex = 73;
            this.chkSameAsPrim.Text = "Same as Primary";
            this.chkSameAsPrim.UseVisualStyleBackColor = true;
            this.chkSameAsPrim.CheckedChanged += new System.EventHandler(this.chkSameAsPrim_CheckedChanged);
            // 
            // BillShipAddressControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelAddress);
            this.Controls.Add(this.chkSameAsPrim);
            this.Controls.Add(this.groupBoxStatus);
            this.Name = "BillShipAddressControl";
            this.Size = new System.Drawing.Size(315, 309);
            this.Load += new System.EventHandler(this.BillingAndShippingAddressControl_Load);
            this.groupBoxStatus.ResumeLayout(false);
            this.groupBoxStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelAddress;
        private System.Windows.Forms.CheckBox chkSameAsPrim;
    }
}
