
namespace bruhshot {
    partial class ScreenshotState {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenshotState));
			ToolBar = new Panel();
			CloseButton = new Button();
			SettingsButton = new Button();
			QuickSettingsStrip = new ContextMenuStrip(components);
			QuickColorSetting = new ToolStripMenuItem();
			QuickFillSetting = new ToolStripMenuItem();
			ShapeTool = new Button();
			RedoButton = new Button();
			UndoButton = new Button();
			Seperator1 = new Panel();
			LineTool = new Button();
			TextTool = new Button();
			ClipboardButton = new Button();
			SaveButton = new Button();
			PenTool = new Button();
			Seperator2 = new Panel();
			InvisibleTextbox = new TextBox();
			toolTip1 = new ToolTip(components);
			ToolBar.SuspendLayout();
			QuickSettingsStrip.SuspendLayout();
			SuspendLayout();
			// 
			// ToolBar
			// 
			ToolBar.BackColor = Color.FromArgb(224, 224, 224);
			ToolBar.BorderStyle = BorderStyle.FixedSingle;
			ToolBar.Controls.Add(CloseButton);
			ToolBar.Controls.Add(SettingsButton);
			ToolBar.Controls.Add(ShapeTool);
			ToolBar.Controls.Add(RedoButton);
			ToolBar.Controls.Add(UndoButton);
			ToolBar.Controls.Add(Seperator1);
			ToolBar.Controls.Add(LineTool);
			ToolBar.Controls.Add(TextTool);
			ToolBar.Controls.Add(ClipboardButton);
			ToolBar.Controls.Add(SaveButton);
			ToolBar.Controls.Add(PenTool);
			ToolBar.Controls.Add(Seperator2);
			ToolBar.Location = new Point(518, 150);
			ToolBar.Margin = new Padding(4, 3, 4, 3);
			ToolBar.Name = "ToolBar";
			ToolBar.Size = new Size(29, 295);
			ToolBar.TabIndex = 1;
			// 
			// CloseButton
			// 
			CloseButton.FlatStyle = FlatStyle.Flat;
			CloseButton.Image = Properties.Resources.CloseButton;
			CloseButton.Location = new Point(-1, 265);
			CloseButton.Margin = new Padding(4, 3, 4, 3);
			CloseButton.Name = "CloseButton";
			CloseButton.Size = new Size(29, 29);
			CloseButton.TabIndex = 12;
			toolTip1.SetToolTip(CloseButton, "Cancel");
			CloseButton.UseVisualStyleBackColor = true;
			// 
			// SettingsButton
			// 
			SettingsButton.ContextMenuStrip = QuickSettingsStrip;
			SettingsButton.FlatStyle = FlatStyle.Flat;
			SettingsButton.Image = Properties.Resources.SettingsTool;
			SettingsButton.Location = new Point(-1, 111);
			SettingsButton.Margin = new Padding(4, 3, 4, 3);
			SettingsButton.Name = "SettingsButton";
			SettingsButton.Size = new Size(29, 29);
			SettingsButton.TabIndex = 11;
			toolTip1.SetToolTip(SettingsButton, "Settings");
			SettingsButton.UseVisualStyleBackColor = true;
			// 
			// QuickSettingsStrip
			// 
			QuickSettingsStrip.Items.AddRange(new ToolStripItem[] { QuickColorSetting, QuickFillSetting });
			QuickSettingsStrip.Name = "contextMenuStrip1";
			QuickSettingsStrip.Size = new Size(125, 48);
			// 
			// QuickColorSetting
			// 
			QuickColorSetting.Name = "QuickColorSetting";
			QuickColorSetting.Size = new Size(124, 22);
			QuickColorSetting.Text = "Color";
			// 
			// QuickFillSetting
			// 
			QuickFillSetting.Name = "QuickFillSetting";
			QuickFillSetting.Size = new Size(124, 22);
			QuickFillSetting.Text = "Shape Fill";
			// 
			// ShapeTool
			// 
			ShapeTool.FlatStyle = FlatStyle.Flat;
			ShapeTool.Image = Properties.Resources.ShapeTool;
			ShapeTool.Location = new Point(-1, 83);
			ShapeTool.Margin = new Padding(0);
			ShapeTool.Name = "ShapeTool";
			ShapeTool.Size = new Size(29, 29);
			ShapeTool.TabIndex = 10;
			toolTip1.SetToolTip(ShapeTool, "Shape");
			ShapeTool.UseVisualStyleBackColor = true;
			// 
			// RedoButton
			// 
			RedoButton.FlatStyle = FlatStyle.Flat;
			RedoButton.Image = Properties.Resources.RedoButton;
			RedoButton.Location = new Point(-1, 174);
			RedoButton.Margin = new Padding(4, 3, 4, 3);
			RedoButton.Name = "RedoButton";
			RedoButton.Size = new Size(29, 29);
			RedoButton.TabIndex = 9;
			toolTip1.SetToolTip(RedoButton, "Redo");
			RedoButton.UseVisualStyleBackColor = true;
			// 
			// UndoButton
			// 
			UndoButton.FlatStyle = FlatStyle.Flat;
			UndoButton.Image = Properties.Resources.UndoButton;
			UndoButton.Location = new Point(-1, 146);
			UndoButton.Margin = new Padding(4, 3, 4, 3);
			UndoButton.Name = "UndoButton";
			UndoButton.Size = new Size(29, 29);
			UndoButton.TabIndex = 8;
			toolTip1.SetToolTip(UndoButton, "Undo");
			UndoButton.UseVisualStyleBackColor = true;
			// 
			// Seperator1
			// 
			Seperator1.BackColor = Color.FromArgb(130, 130, 130);
			Seperator1.Location = new Point(2, 142);
			Seperator1.Margin = new Padding(4, 3, 4, 3);
			Seperator1.Name = "Seperator1";
			Seperator1.Size = new Size(23, 2);
			Seperator1.TabIndex = 7;
			// 
			// LineTool
			// 
			LineTool.FlatStyle = FlatStyle.Flat;
			LineTool.Image = Properties.Resources.LineTool;
			LineTool.Location = new Point(-1, 55);
			LineTool.Margin = new Padding(4, 3, 4, 3);
			LineTool.Name = "LineTool";
			LineTool.Size = new Size(29, 29);
			LineTool.TabIndex = 6;
			toolTip1.SetToolTip(LineTool, "Line");
			LineTool.UseVisualStyleBackColor = true;
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
			toolTip1.SetToolTip(TextTool, "Text");
			TextTool.UseVisualStyleBackColor = true;
			// 
			// ClipboardButton
			// 
			ClipboardButton.FlatStyle = FlatStyle.Flat;
			ClipboardButton.Image = Properties.Resources.ClipboardButton;
			ClipboardButton.Location = new Point(-1, 209);
			ClipboardButton.Margin = new Padding(4, 3, 4, 3);
			ClipboardButton.Name = "ClipboardButton";
			ClipboardButton.Size = new Size(29, 29);
			ClipboardButton.TabIndex = 4;
			toolTip1.SetToolTip(ClipboardButton, "Copy to clipboard");
			ClipboardButton.UseVisualStyleBackColor = true;
			// 
			// SaveButton
			// 
			SaveButton.FlatStyle = FlatStyle.Flat;
			SaveButton.Image = Properties.Resources.SaveTool;
			SaveButton.Location = new Point(-1, 237);
			SaveButton.Margin = new Padding(4, 3, 4, 3);
			SaveButton.Name = "SaveButton";
			SaveButton.Size = new Size(29, 29);
			SaveButton.TabIndex = 3;
			toolTip1.SetToolTip(SaveButton, "Save");
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
			toolTip1.SetToolTip(PenTool, "Pen");
			PenTool.UseVisualStyleBackColor = true;
			// 
			// Seperator2
			// 
			Seperator2.BackColor = Color.FromArgb(130, 130, 130);
			Seperator2.Location = new Point(2, 205);
			Seperator2.Margin = new Padding(4, 3, 4, 3);
			Seperator2.Name = "Seperator2";
			Seperator2.Size = new Size(23, 2);
			Seperator2.TabIndex = 2;
			// 
			// InvisibleTextbox
			// 
			InvisibleTextbox.AcceptsReturn = true;
			InvisibleTextbox.AcceptsTab = true;
			InvisibleTextbox.Enabled = false;
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
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(4, 3, 4, 3);
			Name = "ScreenshotState";
			ShowInTaskbar = false;
			Text = "Screenshot";
			ToolBar.ResumeLayout(false);
			QuickSettingsStrip.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private Panel ToolBar;
        private Button PenTool;
        private Button SaveButton;
        private Panel Seperator2;
        private Button ClipboardButton;
        private Button TextTool;
        private TextBox InvisibleTextbox;
        private ToolTip toolTip1;
        private Button LineTool;
        private Button RedoButton;
        private Button UndoButton;
        private Panel Seperator1;
        private Button ShapeTool;
        private Button SettingsButton;
        private Button CloseButton;
		private ToolStripMenuItem QuickColorSetting;
		private ToolStripMenuItem QuickFillSetting;
		public ContextMenuStrip QuickSettingsStrip;
	}
}