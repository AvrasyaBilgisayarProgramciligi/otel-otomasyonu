namespace otel_otomasyonu
{
    partial class OzellikEkle
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.cikis_buton = new System.Windows.Forms.Button();
            this.ozellik = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ozellik_kayıt = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel2.Controls.Add(this.cikis_buton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(324, 22);
            this.panel2.TabIndex = 14;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel2_MouseDown);
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel2_MouseMove);
            this.panel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel2_MouseUp);
            // 
            // cikis_buton
            // 
            this.cikis_buton.BackColor = System.Drawing.Color.DimGray;
            this.cikis_buton.BackgroundImage = global::otel_otomasyonu.Properties.Resources.icons8_delete_32;
            this.cikis_buton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cikis_buton.Dock = System.Windows.Forms.DockStyle.Left;
            this.cikis_buton.FlatAppearance.BorderSize = 0;
            this.cikis_buton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cikis_buton.Location = new System.Drawing.Point(0, 0);
            this.cikis_buton.Name = "cikis_buton";
            this.cikis_buton.Size = new System.Drawing.Size(35, 22);
            this.cikis_buton.TabIndex = 0;
            this.cikis_buton.UseVisualStyleBackColor = false;
            this.cikis_buton.Click += new System.EventHandler(this.Cikis_buton_Click);
            // 
            // ozellik
            // 
            this.ozellik.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ozellik.Location = new System.Drawing.Point(37, 78);
            this.ozellik.Name = "ozellik";
            this.ozellik.Size = new System.Drawing.Size(168, 26);
            this.ozellik.TabIndex = 32;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label8.Location = new System.Drawing.Point(34, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 18);
            this.label8.TabIndex = 41;
            this.label8.Text = "Özellik Gir";
            // 
            // ozellik_kayıt
            // 
            this.ozellik_kayıt.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ozellik_kayıt.FlatAppearance.BorderSize = 0;
            this.ozellik_kayıt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ozellik_kayıt.ForeColor = System.Drawing.SystemColors.Control;
            this.ozellik_kayıt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ozellik_kayıt.Location = new System.Drawing.Point(211, 78);
            this.ozellik_kayıt.Name = "ozellik_kayıt";
            this.ozellik_kayıt.Size = new System.Drawing.Size(85, 26);
            this.ozellik_kayıt.TabIndex = 42;
            this.ozellik_kayıt.Text = "Özelliği Kaydet";
            this.ozellik_kayıt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ozellik_kayıt.UseVisualStyleBackColor = false;
            this.ozellik_kayıt.Click += new System.EventHandler(this.Ozellik_kayıt_Click);
            // 
            // OzellikEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 135);
            this.Controls.Add(this.ozellik_kayıt);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ozellik);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OzellikEkle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OzellikEkle";
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button cikis_buton;
        private System.Windows.Forms.TextBox ozellik;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button ozellik_kayıt;
    }
}