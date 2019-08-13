namespace ImageProcessingEditor
{
    partial class Form2
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
            this.label2 = new System.Windows.Forms.Label();
            this.histogramaDesenat1 = new Histogram.HistogramCiz();
            this.Histogram = new Histogram.HistogramCiz();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 305);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Renkli Görüntü Histogramı";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(819, 305);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Gri Seviye Görüntü Histogramı";
            // 
            // histogramaDesenat1
            // 
            this.histogramaDesenat1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.histogramaDesenat1.DisplayColor = System.Drawing.Color.Black;
            this.histogramaDesenat1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.histogramaDesenat1.Location = new System.Drawing.Point(822, 22);
            this.histogramaDesenat1.Name = "histogramaDesenat1";
            this.histogramaDesenat1.Offset = 20;
            this.histogramaDesenat1.Size = new System.Drawing.Size(650, 280);
            this.histogramaDesenat1.TabIndex = 13;
            this.histogramaDesenat1.Visible = false;
            // 
            // Histogram
            // 
            this.Histogram.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Histogram.DisplayColor = System.Drawing.Color.Black;
            this.Histogram.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Histogram.Location = new System.Drawing.Point(12, 12);
            this.Histogram.Name = "Histogram";
            this.Histogram.Offset = 20;
            this.Histogram.Size = new System.Drawing.Size(650, 280);
            this.Histogram.TabIndex = 12;
            this.Histogram.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 406);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "label3";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1494, 455);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.histogramaDesenat1);
            this.Controls.Add(this.Histogram);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.Resize += new System.EventHandler(this.Form2_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Histogram.HistogramCiz histogramaDesenat1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}