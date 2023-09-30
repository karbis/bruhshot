namespace bruhshot
{
    partial class InfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoForm));
            panel1 = new Panel();
            BackButton = new Button();
            label3 = new Label();
            linkLabel1 = new LinkLabel();
            panel2 = new Panel();
            label1 = new Label();
            label2 = new Label();
            tabControl1 = new TabControl();
            SettingsTab = new TabPage();
            label4 = new Label();
            InfoTab = new TabPage();
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            SettingsTab.SuspendLayout();
            InfoTab.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(BackButton);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(linkLabel1);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(3, 127);
            panel1.Name = "panel1";
            panel1.Size = new Size(363, 100);
            panel1.TabIndex = 1;
            // 
            // BackButton
            // 
            BackButton.Location = new Point(283, 72);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(75, 23);
            BackButton.TabIndex = 2;
            BackButton.Text = "Close";
            BackButton.UseVisualStyleBackColor = true;
            BackButton.Click += BackButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 30);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 1;
            label3.Text = "2023";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(12, 12);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(158, 15);
            linkLabel1.TabIndex = 0;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "github.com/karbis/bruhshot";
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.BackgroundImage = Properties.Resources.IconImg;
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.Location = new Point(12, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(104, 104);
            panel2.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(121, 5);
            label1.Name = "label1";
            label1.Size = new Size(170, 50);
            label1.TabIndex = 3;
            label1.Text = "Bruhshot";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point);
            label2.Location = new Point(129, 47);
            label2.Name = "label2";
            label2.Size = new Size(39, 21);
            label2.TabIndex = 4;
            label2.Text = "v1.3";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(SettingsTab);
            tabControl1.Controls.Add(InfoTab);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.ImeMode = ImeMode.NoControl;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.Padding = new Point(0, 0);
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(377, 258);
            tabControl1.TabIndex = 5;
            // 
            // SettingsTab
            // 
            SettingsTab.Controls.Add(label4);
            SettingsTab.Location = new Point(4, 24);
            SettingsTab.Name = "SettingsTab";
            SettingsTab.Padding = new Padding(3);
            SettingsTab.Size = new Size(369, 230);
            SettingsTab.TabIndex = 1;
            SettingsTab.Text = "Settings";
            SettingsTab.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 15);
            label4.Name = "label4";
            label4.Size = new Size(54, 15);
            label4.TabIndex = 0;
            label4.Text = "soon:tm:";
            // 
            // InfoTab
            // 
            InfoTab.Controls.Add(label2);
            InfoTab.Controls.Add(label1);
            InfoTab.Controls.Add(panel2);
            InfoTab.Controls.Add(panel1);
            InfoTab.Location = new Point(4, 24);
            InfoTab.Name = "InfoTab";
            InfoTab.Padding = new Padding(3);
            InfoTab.Size = new Size(369, 230);
            InfoTab.TabIndex = 0;
            InfoTab.Text = "Info";
            InfoTab.UseVisualStyleBackColor = true;
            // 
            // InfoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(377, 258);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "InfoForm";
            Text = "Settings";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            SettingsTab.ResumeLayout(false);
            SettingsTab.PerformLayout();
            InfoTab.ResumeLayout(false);
            InfoTab.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private Label label2;
        private Label label3;
        private LinkLabel linkLabel1;
        private Button BackButton;
        private TabControl tabControl1;
        private TabPage InfoTab;
        private TabPage SettingsTab;
        private Label label4;
    }
}