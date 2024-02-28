namespace DiscosDB
{
    partial class frmDiscos
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
            this.dgvDiscos = new System.Windows.Forms.DataGridView();
            this.imgDisco = new System.Windows.Forms.PictureBox();
            this.leyenda = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiscos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgDisco)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDiscos
            // 
            this.dgvDiscos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiscos.Location = new System.Drawing.Point(12, 25);
            this.dgvDiscos.Name = "dgvDiscos";
            this.dgvDiscos.Size = new System.Drawing.Size(570, 169);
            this.dgvDiscos.TabIndex = 0;
            this.dgvDiscos.SelectionChanged += new System.EventHandler(this.dgvDiscos_SelectionChanged);
            // 
            // imgDisco
            // 
            this.imgDisco.Location = new System.Drawing.Point(629, 25);
            this.imgDisco.Name = "imgDisco";
            this.imgDisco.Size = new System.Drawing.Size(189, 169);
            this.imgDisco.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgDisco.TabIndex = 1;
            this.imgDisco.TabStop = false;
            // 
            // leyenda
            // 
            this.leyenda.AutoSize = true;
            this.leyenda.Location = new System.Drawing.Point(667, 9);
            this.leyenda.Name = "leyenda";
            this.leyenda.Size = new System.Drawing.Size(89, 13);
            this.leyenda.TabIndex = 3;
            this.leyenda.Text = "Portada del disco";
            // 
            // frmDiscos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(830, 234);
            this.Controls.Add(this.leyenda);
            this.Controls.Add(this.imgDisco);
            this.Controls.Add(this.dgvDiscos);
            this.Name = "frmDiscos";
            this.Text = "Discos";
            this.Load += new System.EventHandler(this.frmDiscos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiscos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgDisco)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDiscos;
        private System.Windows.Forms.PictureBox imgDisco;
        private System.Windows.Forms.Label leyenda;
    }
}

