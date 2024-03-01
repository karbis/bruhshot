namespace bruhshot {
    partial class SettingsForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            ColorDialog = new ColorDialog();
            label1 = new Label();
            button1 = new Button();
            ColorShowcasePanel = new Panel();
            label2 = new Label();
            ThicknessValue = new NumericUpDown();
            label3 = new Label();
            ShapeSelector = new ComboBox();
            ShapeFillCheck = new CheckBox();
            label4 = new Label();
            KeybindTextbox = new TextBox();
            label5 = new Label();
            label6 = new Label();
            CaptureKeybindInput = new Button();
            FontDialog = new FontDialog();
            FontText = new TextBox();
            FontPickerButton = new Button();
            LineShapeDropdown = new ComboBox();
            label7 = new Label();
            tabControl1 = new TabControl();
            EditorPage = new TabPage();
            OutputPage = new TabPage();
            CaptureCursor = new CheckBox();
            label14 = new Label();
            AutoSaveOption = new CheckBox();
            label12 = new Label();
            ChooseFileButton = new Button();
            AutoSaveLocation = new TextBox();
            label11 = new Label();
            InfoPage = new TabPage();
            label8 = new Label();
            label9 = new Label();
            panel2 = new Panel();
            panel1 = new Panel();
            label10 = new Label();
            linkLabel1 = new LinkLabel();
            FolderDialog = new FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)ThicknessValue).BeginInit();
            tabControl1.SuspendLayout();
            EditorPage.SuspendLayout();
            OutputPage.SuspendLayout();
            InfoPage.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // ColorDialog
            // 
            ColorDialog.AnyColor = true;
            ColorDialog.Color = Color.Red;
            ColorDialog.FullOpen = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 10);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 0;
            label1.Text = "Color";
            // 
            // button1
            // 
            button1.Location = new Point(156, 6);
            button1.Name = "button1";
            button1.Size = new Size(58, 23);
            button1.TabIndex = 1;
            button1.Text = "Choose";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ColorShowcasePanel
            // 
            ColorShowcasePanel.BorderStyle = BorderStyle.FixedSingle;
            ColorShowcasePanel.Location = new Point(220, 6);
            ColorShowcasePanel.Name = "ColorShowcasePanel";
            ColorShowcasePanel.Size = new Size(89, 22);
            ColorShowcasePanel.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 36);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 3;
            label2.Text = "Thickness";
            // 
            // ThicknessValue
            // 
            ThicknessValue.BorderStyle = BorderStyle.FixedSingle;
            ThicknessValue.Location = new Point(220, 34);
            ThicknessValue.Maximum = new decimal(new int[] { 2048, 0, 0, 0 });
            ThicknessValue.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ThicknessValue.Name = "ThicknessValue";
            ThicknessValue.Size = new Size(89, 23);
            ThicknessValue.TabIndex = 4;
            ThicknessValue.ThousandsSeparator = true;
            ThicknessValue.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 92);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 5;
            label3.Text = "Shape";
            // 
            // ShapeSelector
            // 
            ShapeSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            ShapeSelector.FormattingEnabled = true;
            ShapeSelector.Items.AddRange(new object[] { "Circle", "Square", "Triangle" });
            ShapeSelector.Location = new Point(188, 92);
            ShapeSelector.Name = "ShapeSelector";
            ShapeSelector.Size = new Size(121, 23);
            ShapeSelector.Sorted = true;
            ShapeSelector.TabIndex = 6;
            // 
            // ShapeFillCheck
            // 
            ShapeFillCheck.AutoSize = true;
            ShapeFillCheck.CheckAlign = ContentAlignment.MiddleRight;
            ShapeFillCheck.Location = new Point(294, 121);
            ShapeFillCheck.Name = "ShapeFillCheck";
            ShapeFillCheck.Size = new Size(15, 14);
            ShapeFillCheck.TabIndex = 8;
            ShapeFillCheck.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 63);
            label4.Name = "label4";
            label4.Size = new Size(53, 15);
            label4.TabIndex = 9;
            label4.Text = "Text font";
            // 
            // KeybindTextbox
            // 
            KeybindTextbox.AcceptsReturn = true;
            KeybindTextbox.BorderStyle = BorderStyle.FixedSingle;
            KeybindTextbox.Location = new Point(218, 6);
            KeybindTextbox.Name = "KeybindTextbox";
            KeybindTextbox.Size = new Size(89, 23);
            KeybindTextbox.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 10);
            label5.Name = "label5";
            label5.Size = new Size(50, 15);
            label5.TabIndex = 12;
            label5.Text = "Keybind";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 120);
            label6.Name = "label6";
            label6.Size = new Size(57, 15);
            label6.TabIndex = 13;
            label6.Text = "Shape Fill";
            // 
            // CaptureKeybindInput
            // 
            CaptureKeybindInput.Location = new Point(156, 6);
            CaptureKeybindInput.Name = "CaptureKeybindInput";
            CaptureKeybindInput.Size = new Size(58, 23);
            CaptureKeybindInput.TabIndex = 14;
            CaptureKeybindInput.Text = "Capture";
            CaptureKeybindInput.UseVisualStyleBackColor = true;
            // 
            // FontDialog
            // 
            FontDialog.FontMustExist = true;
            // 
            // FontText
            // 
            FontText.AcceptsReturn = true;
            FontText.BorderStyle = BorderStyle.FixedSingle;
            FontText.Location = new Point(220, 63);
            FontText.Name = "FontText";
            FontText.ReadOnly = true;
            FontText.Size = new Size(89, 23);
            FontText.TabIndex = 15;
            // 
            // FontPickerButton
            // 
            FontPickerButton.Location = new Point(156, 63);
            FontPickerButton.Name = "FontPickerButton";
            FontPickerButton.Size = new Size(58, 23);
            FontPickerButton.TabIndex = 16;
            FontPickerButton.Text = "Choose";
            FontPickerButton.UseVisualStyleBackColor = true;
            // 
            // LineShapeDropdown
            // 
            LineShapeDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            LineShapeDropdown.FormattingEnabled = true;
            LineShapeDropdown.Items.AddRange(new object[] { "Arrow", "Dashed", "Line" });
            LineShapeDropdown.Location = new Point(188, 141);
            LineShapeDropdown.Name = "LineShapeDropdown";
            LineShapeDropdown.Size = new Size(121, 23);
            LineShapeDropdown.Sorted = true;
            LineShapeDropdown.TabIndex = 18;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 147);
            label7.Name = "label7";
            label7.Size = new Size(63, 15);
            label7.TabIndex = 17;
            label7.Text = "Line shape";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(EditorPage);
            tabControl1.Controls.Add(OutputPage);
            tabControl1.Controls.Add(InfoPage);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(321, 206);
            tabControl1.TabIndex = 19;
            // 
            // EditorPage
            // 
            EditorPage.Controls.Add(LineShapeDropdown);
            EditorPage.Controls.Add(label7);
            EditorPage.Controls.Add(FontPickerButton);
            EditorPage.Controls.Add(FontText);
            EditorPage.Controls.Add(label6);
            EditorPage.Controls.Add(label4);
            EditorPage.Controls.Add(ShapeFillCheck);
            EditorPage.Controls.Add(ShapeSelector);
            EditorPage.Controls.Add(label3);
            EditorPage.Controls.Add(ThicknessValue);
            EditorPage.Controls.Add(label2);
            EditorPage.Controls.Add(ColorShowcasePanel);
            EditorPage.Controls.Add(button1);
            EditorPage.Controls.Add(label1);
            EditorPage.Location = new Point(4, 24);
            EditorPage.Name = "EditorPage";
            EditorPage.Padding = new Padding(3);
            EditorPage.Size = new Size(313, 178);
            EditorPage.TabIndex = 0;
            EditorPage.Text = "Editor";
            EditorPage.UseVisualStyleBackColor = true;
            // 
            // OutputPage
            // 
            OutputPage.Controls.Add(CaptureCursor);
            OutputPage.Controls.Add(label14);
            OutputPage.Controls.Add(AutoSaveOption);
            OutputPage.Controls.Add(label12);
            OutputPage.Controls.Add(ChooseFileButton);
            OutputPage.Controls.Add(AutoSaveLocation);
            OutputPage.Controls.Add(label11);
            OutputPage.Controls.Add(CaptureKeybindInput);
            OutputPage.Controls.Add(label5);
            OutputPage.Controls.Add(KeybindTextbox);
            OutputPage.Location = new Point(4, 24);
            OutputPage.Name = "OutputPage";
            OutputPage.Padding = new Padding(3);
            OutputPage.Size = new Size(313, 178);
            OutputPage.TabIndex = 1;
            OutputPage.Text = "Capturing";
            OutputPage.UseVisualStyleBackColor = true;
            // 
            // CaptureCursor
            // 
            CaptureCursor.AutoSize = true;
            CaptureCursor.CheckAlign = ContentAlignment.MiddleRight;
            CaptureCursor.Location = new Point(290, 84);
            CaptureCursor.Name = "CaptureCursor";
            CaptureCursor.Size = new Size(15, 14);
            CaptureCursor.TabIndex = 23;
            CaptureCursor.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(12, 83);
            label14.Name = "label14";
            label14.Size = new Size(85, 15);
            label14.TabIndex = 22;
            label14.Text = "Capture cursor";
            // 
            // AutoSaveOption
            // 
            AutoSaveOption.AutoSize = true;
            AutoSaveOption.CheckAlign = ContentAlignment.MiddleRight;
            AutoSaveOption.Location = new Point(290, 35);
            AutoSaveOption.Name = "AutoSaveOption";
            AutoSaveOption.Size = new Size(15, 14);
            AutoSaveOption.TabIndex = 19;
            AutoSaveOption.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(12, 34);
            label12.Name = "label12";
            label12.Size = new Size(123, 15);
            label12.TabIndex = 18;
            label12.Text = "Auto save to directory";
            // 
            // ChooseFileButton
            // 
            ChooseFileButton.Location = new Point(156, 55);
            ChooseFileButton.Name = "ChooseFileButton";
            ChooseFileButton.Size = new Size(58, 23);
            ChooseFileButton.TabIndex = 17;
            ChooseFileButton.Text = "Choose";
            ChooseFileButton.UseVisualStyleBackColor = true;
            // 
            // AutoSaveLocation
            // 
            AutoSaveLocation.BorderStyle = BorderStyle.FixedSingle;
            AutoSaveLocation.Location = new Point(218, 55);
            AutoSaveLocation.Name = "AutoSaveLocation";
            AutoSaveLocation.Size = new Size(89, 23);
            AutoSaveLocation.TabIndex = 16;
            AutoSaveLocation.TextAlign = HorizontalAlignment.Right;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(12, 59);
            label11.Name = "label11";
            label11.Size = new Size(105, 15);
            label11.TabIndex = 15;
            label11.Text = "Auto save location";
            // 
            // InfoPage
            // 
            InfoPage.Controls.Add(label8);
            InfoPage.Controls.Add(label9);
            InfoPage.Controls.Add(panel2);
            InfoPage.Controls.Add(panel1);
            InfoPage.Location = new Point(4, 24);
            InfoPage.Name = "InfoPage";
            InfoPage.Size = new Size(313, 178);
            InfoPage.TabIndex = 2;
            InfoPage.Text = "Info";
            InfoPage.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point);
            label8.Location = new Point(129, 45);
            label8.Name = "label8";
            label8.Size = new Size(51, 21);
            label8.TabIndex = 8;
            label8.Text = "v1.5.1";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(121, 3);
            label9.Name = "label9";
            label9.Size = new Size(170, 50);
            label9.TabIndex = 7;
            label9.Text = "Bruhshot";
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.BackgroundImage = Properties.Resources.IconImg;
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.Location = new Point(12, 10);
            panel2.Name = "panel2";
            panel2.Size = new Size(104, 104);
            panel2.TabIndex = 6;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(label10);
            panel1.Controls.Add(linkLabel1);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 120);
            panel1.Name = "panel1";
            panel1.Size = new Size(313, 58);
            panel1.TabIndex = 5;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(12, 30);
            label10.Name = "label10";
            label10.Size = new Size(31, 15);
            label10.TabIndex = 1;
            label10.Text = "2024";
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
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(321, 206);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.Manual;
            Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)ThicknessValue).EndInit();
            tabControl1.ResumeLayout(false);
            EditorPage.ResumeLayout(false);
            EditorPage.PerformLayout();
            OutputPage.ResumeLayout(false);
            OutputPage.PerformLayout();
            InfoPage.ResumeLayout(false);
            InfoPage.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ColorDialog ColorDialog;
        private Label label1;
        private Button button1;
        private Panel ColorShowcasePanel;
        private Label label2;
        private NumericUpDown ThicknessValue;
        private Label label3;
        private ComboBox ShapeSelector;
        private CheckBox ShapeFillCheck;
        private Label label4;
        private TextBox KeybindTextbox;
        private Label label5;
        private Label label6;
        private Button CaptureKeybindInput;
        private FontDialog FontDialog;
        private TextBox FontText;
        private Button FontPickerButton;
        private ComboBox LineShapeDropdown;
        private Label label7;
        private TabPage EditorPage;
        private TabPage OutputPage;
        private TabPage InfoPage;
        private Label label8;
        private Label label9;
        private Panel panel2;
        private Panel panel1;
        private Label label10;
        private LinkLabel linkLabel1;
        public TabControl tabControl1;
        private Button ChooseFileButton;
        private TextBox AutoSaveLocation;
        private Label label11;
        private CheckBox AutoSaveOption;
        private Label label12;
        private CheckBox CaptureCursor;
        private Label label14;
        private FolderBrowserDialog FolderDialog;
    }
}