
namespace bruhshot
{
    partial class ScreenshotState
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
            ToolBar = new Panel();
            TextTool = new Button();
            ClipboardButton = new Button();
            SaveButton = new Button();
            PenTool = new Button();
            panel1 = new Panel();
            FullImage = new PictureBox();
            InvisibleTextbox = new TextBox();
            ToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)FullImage).BeginInit();
            SuspendLayout();
            // 
            // ToolBar
            // 
            ToolBar.BackColor = Color.FromArgb(224, 224, 224);
            ToolBar.BorderStyle = BorderStyle.FixedSingle;
            ToolBar.Controls.Add(TextTool);
            ToolBar.Controls.Add(ClipboardButton);
            ToolBar.Controls.Add(SaveButton);
            ToolBar.Controls.Add(PenTool);
            ToolBar.Controls.Add(panel1);
            ToolBar.Location = new Point(518, 243);
            ToolBar.Margin = new Padding(4, 3, 4, 3);
            ToolBar.Name = "ToolBar";
            ToolBar.Size = new Size(29, 120);
            ToolBar.TabIndex = 1;
            // 
            // TextTool
            // 
            TextTool.FlatStyle = FlatStyle.Flat;
            TextTool.Image = Properties.Resources.TextTool;
            TextTool.Location = new Point(-1, 27);
            TextTool.Margin = new Padding(4, 3, 4, 3);
            TextTool.Name = "TextTool";
            TextTool.Size = new Size(29, 29);
            TextTool.TabIndex = 5;
            TextTool.UseVisualStyleBackColor = true;
            // 
            // ClipboardButton
            // 
            ClipboardButton.FlatStyle = FlatStyle.Flat;
            ClipboardButton.Image = Properties.Resources.ClipboardButton;
            ClipboardButton.Location = new Point(-1, 62);
            ClipboardButton.Margin = new Padding(4, 3, 4, 3);
            ClipboardButton.Name = "ClipboardButton";
            ClipboardButton.Size = new Size(29, 29);
            ClipboardButton.TabIndex = 4;
            ClipboardButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            SaveButton.FlatStyle = FlatStyle.Flat;
            SaveButton.Image = Properties.Resources.SaveTool;
            SaveButton.Location = new Point(-1, 90);
            SaveButton.Margin = new Padding(4, 3, 4, 3);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(29, 29);
            SaveButton.TabIndex = 3;
            SaveButton.UseVisualStyleBackColor = true;
            // 
            // PenTool
            // 
            PenTool.FlatStyle = FlatStyle.Flat;
            PenTool.Image = Properties.Resources.PenTool;
            PenTool.Location = new Point(-1, -1);
            PenTool.Margin = new Padding(4, 3, 4, 3);
            PenTool.Name = "PenTool";
            PenTool.Size = new Size(29, 29);
            PenTool.TabIndex = 2;
            PenTool.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(130, 130, 130);
            panel1.Location = new Point(2, 58);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(23, 2);
            panel1.TabIndex = 2;
            // 
            // FullImage
            // 
            FullImage.Dock = DockStyle.Fill;
            FullImage.Location = new Point(0, 0);
            FullImage.Margin = new Padding(0);
            FullImage.Name = "FullImage";
            FullImage.Size = new Size(933, 519);
            FullImage.TabIndex = 0;
            FullImage.TabStop = false;
            FullImage.WaitOnLoad = true;
            // 
            // InvisibleTextbox
            // 
            InvisibleTextbox.AcceptsReturn = true;
            InvisibleTextbox.AcceptsTab = true;
            InvisibleTextbox.Location = new Point(-1167, -1154);
            InvisibleTextbox.Margin = new Padding(4, 3, 4, 3);
            InvisibleTextbox.Multiline = true;
            InvisibleTextbox.Name = "InvisibleTextbox";
            InvisibleTextbox.Size = new Size(116, 22);
            InvisibleTextbox.TabIndex = 2;
            InvisibleTextbox.TabStop = false;
            // 
            // ScreenshotState
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(InvisibleTextbox);
            Controls.Add(ToolBar);
            Controls.Add(FullImage);
            Margin = new Padding(4, 3, 4, 3);
            Name = "ScreenshotState";
            Text = "ScreenshotState";
            ToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)FullImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox FullImage;
        private Panel ToolBar;
        private Button PenTool;
        private Button SaveButton;
        private Panel panel1;
        private Button ClipboardButton;
        private Button TextTool;
        private TextBox InvisibleTextbox;
    }
}