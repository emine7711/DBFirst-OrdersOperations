namespace DBFirst_FaturaIslemleri
{
    partial class SiparisDetaySorgulamaFormu
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtOrderAra = new System.Windows.Forms.TextBox();
            this.btnOrderAra = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "OrderID:";
            // 
            // txtOrderAra
            // 
            this.txtOrderAra.Location = new System.Drawing.Point(83, 56);
            this.txtOrderAra.Name = "txtOrderAra";
            this.txtOrderAra.Size = new System.Drawing.Size(100, 20);
            this.txtOrderAra.TabIndex = 1;
//            this.txtOrderAra.TextChanged += new System.EventHandler(this.txtOrderAra_TextChanged);
            // 
            // btnOrderAra
            // 
            this.btnOrderAra.Location = new System.Drawing.Point(97, 82);
            this.btnOrderAra.Name = "btnOrderAra";
            this.btnOrderAra.Size = new System.Drawing.Size(75, 23);
            this.btnOrderAra.TabIndex = 2;
            this.btnOrderAra.Text = "Ara";
            this.btnOrderAra.UseVisualStyleBackColor = true;
            this.btnOrderAra.Click += new System.EventHandler(this.btnOrderAra_Click);
            // 
            // SiparisDetaySorgulamaFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 190);
            this.Controls.Add(this.btnOrderAra);
            this.Controls.Add(this.txtOrderAra);
            this.Controls.Add(this.label1);
            this.Name = "SiparisDetaySorgulamaFormu";
            this.Text = "SiparisDetaySorgulamaFormu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOrderAra;
        private System.Windows.Forms.Button btnOrderAra;
    }
}