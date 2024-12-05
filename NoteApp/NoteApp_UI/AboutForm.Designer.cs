
namespace NoteApp_UI
{
    partial class AboutForm
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
            this.aboutMainTextBox = new System.Windows.Forms.TextBox();
            this.aboutTitleTextBox = new System.Windows.Forms.TextBox();
            this.AAboutStatusGroupBox0 = new System.Windows.Forms.Panel();
            this.aboutMainTextBox2 = new System.Windows.Forms.TextBox();
            this.AAboutStatusGroupBox0.SuspendLayout();
            this.SuspendLayout();
            // 
            // aboutMainTextBox
            // 
            this.aboutMainTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.aboutMainTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.aboutMainTextBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.aboutMainTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.aboutMainTextBox.Location = new System.Drawing.Point(0, 52);
            this.aboutMainTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.aboutMainTextBox.Multiline = true;
            this.aboutMainTextBox.Name = "aboutMainTextBox";
            this.aboutMainTextBox.ReadOnly = true;
            this.aboutMainTextBox.Size = new System.Drawing.Size(255, 136);
            this.aboutMainTextBox.TabIndex = 0;
            this.aboutMainTextBox.Text = "Учебный проект для университета\r\n\r\nv1.0.0\r\n\r\nAuthor: Artem Karev\r\n\r\nGitHub: https" +
    "://github.com/Molegen/NoteApp.git";
            this.aboutMainTextBox.TextChanged += new System.EventHandler(this.aboutMainTextBox_TextChanged);
            // 
            // aboutTitleTextBox
            // 
            this.aboutTitleTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.aboutTitleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.aboutTitleTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.8F);
            this.aboutTitleTextBox.Location = new System.Drawing.Point(2, 2);
            this.aboutTitleTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.aboutTitleTextBox.Name = "aboutTitleTextBox";
            this.aboutTitleTextBox.ReadOnly = true;
            this.aboutTitleTextBox.Size = new System.Drawing.Size(123, 24);
            this.aboutTitleTextBox.TabIndex = 1;
            this.aboutTitleTextBox.Text = "NoteApp";
            this.aboutTitleTextBox.TextChanged += new System.EventHandler(this.aboutTitleTextBox_TextChanged);
            // 
            // AAboutStatusGroupBox0
            // 
            this.AAboutStatusGroupBox0.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AAboutStatusGroupBox0.AutoSize = true;
            this.AAboutStatusGroupBox0.Controls.Add(this.aboutTitleTextBox);
            this.AAboutStatusGroupBox0.Controls.Add(this.aboutMainTextBox2);
            this.AAboutStatusGroupBox0.Controls.Add(this.aboutMainTextBox);
            this.AAboutStatusGroupBox0.Location = new System.Drawing.Point(9, 15);
            this.AAboutStatusGroupBox0.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AAboutStatusGroupBox0.Name = "AAboutStatusGroupBox0";
            this.AAboutStatusGroupBox0.Size = new System.Drawing.Size(274, 281);
            this.AAboutStatusGroupBox0.TabIndex = 14;
            this.AAboutStatusGroupBox0.Paint += new System.Windows.Forms.PaintEventHandler(this.AAboutStatusGroupBox0_Paint);
            // 
            // aboutMainTextBox2
            // 
            this.aboutMainTextBox2.BackColor = System.Drawing.SystemColors.Control;
            this.aboutMainTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.aboutMainTextBox2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.aboutMainTextBox2.Enabled = false;
            this.aboutMainTextBox2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.aboutMainTextBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.aboutMainTextBox2.Location = new System.Drawing.Point(0, 249);
            this.aboutMainTextBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.aboutMainTextBox2.Multiline = true;
            this.aboutMainTextBox2.Name = "aboutMainTextBox2";
            this.aboutMainTextBox2.ReadOnly = true;
            this.aboutMainTextBox2.Size = new System.Drawing.Size(50, 29);
            this.aboutMainTextBox2.TabIndex = 0;
            this.aboutMainTextBox2.Text = "2024";
            this.aboutMainTextBox2.TextChanged += new System.EventHandler(this.aboutMainTextBox2_TextChanged);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(364, 302);
            this.Controls.Add(this.AAboutStatusGroupBox0);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AboutForm";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.AAboutStatusGroupBox0.ResumeLayout(false);
            this.AAboutStatusGroupBox0.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox aboutMainTextBox;
        private System.Windows.Forms.TextBox aboutTitleTextBox;
        private System.Windows.Forms.Panel AAboutStatusGroupBox0;
        private System.Windows.Forms.TextBox aboutMainTextBox2;
    }
}