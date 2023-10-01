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
            TextSizeChanger = new NumericUpDown();
            label4 = new Label();
            KeybindTextbox = new TextBox();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)ThicknessValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TextSizeChanger).BeginInit();
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
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 0;
            label1.Text = "Color";
            // 
            // button1
            // 
            button1.Location = new Point(54, 5);
            button1.Name = "button1";
            button1.Size = new Size(111, 23);
            button1.TabIndex = 1;
            button1.Text = "Show color picker";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ColorShowcasePanel
            // 
            ColorShowcasePanel.BorderStyle = BorderStyle.FixedSingle;
            ColorShowcasePanel.Location = new Point(171, 5);
            ColorShowcasePanel.Name = "ColorShowcasePanel";
            ColorShowcasePanel.Size = new Size(251, 23);
            ColorShowcasePanel.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 42);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 3;
            label2.Text = "Thickness";
            // 
            // ThicknessValue
            // 
            ThicknessValue.BorderStyle = BorderStyle.FixedSingle;
            ThicknessValue.Location = new Point(76, 40);
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
            label3.Location = new Point(171, 42);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 5;
            label3.Text = "Shape";
            // 
            // ShapeSelector
            // 
            ShapeSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            ShapeSelector.FormattingEnabled = true;
            ShapeSelector.Items.AddRange(new object[] { "Circle", "Square" });
            ShapeSelector.Location = new Point(216, 40);
            ShapeSelector.Name = "ShapeSelector";
            ShapeSelector.Size = new Size(121, 23);
            ShapeSelector.Sorted = true;
            ShapeSelector.TabIndex = 6;
            // 
            // ShapeFillCheck
            // 
            ShapeFillCheck.AutoSize = true;
            ShapeFillCheck.Location = new Point(171, 69);
            ShapeFillCheck.Name = "ShapeFillCheck";
            ShapeFillCheck.Size = new Size(74, 19);
            ShapeFillCheck.TabIndex = 8;
            ShapeFillCheck.Text = "Shape fill";
            ShapeFillCheck.UseVisualStyleBackColor = true;
            // 
            // TextSizeChanger
            // 
            TextSizeChanger.BorderStyle = BorderStyle.FixedSingle;
            TextSizeChanger.Location = new Point(76, 69);
            TextSizeChanger.Maximum = new decimal(new int[] { 2048, 0, 0, 0 });
            TextSizeChanger.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            TextSizeChanger.Name = "TextSizeChanger";
            TextSizeChanger.Size = new Size(89, 23);
            TextSizeChanger.TabIndex = 10;
            TextSizeChanger.ThousandsSeparator = true;
            TextSizeChanger.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 71);
            label4.Name = "label4";
            label4.Size = new Size(50, 15);
            label4.TabIndex = 9;
            label4.Text = "Text size";
            // 
            // KeybindTextbox
            // 
            KeybindTextbox.AcceptsReturn = true;
            KeybindTextbox.BorderStyle = BorderStyle.FixedSingle;
            KeybindTextbox.Location = new Point(76, 98);
            KeybindTextbox.Name = "KeybindTextbox";
            KeybindTextbox.Size = new Size(89, 23);
            KeybindTextbox.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 101);
            label5.Name = "label5";
            label5.Size = new Size(50, 15);
            label5.TabIndex = 12;
            label5.Text = "Keybind";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 131);
            Controls.Add(label5);
            Controls.Add(KeybindTextbox);
            Controls.Add(TextSizeChanger);
            Controls.Add(label4);
            Controls.Add(ShapeFillCheck);
            Controls.Add(ShapeSelector);
            Controls.Add(label3);
            Controls.Add(ThicknessValue);
            Controls.Add(label2);
            Controls.Add(ColorShowcasePanel);
            Controls.Add(button1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.Manual;
            Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)ThicknessValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)TextSizeChanger).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private NumericUpDown TextSizeChanger;
        private Label label4;
        private TextBox KeybindTextbox;
        private Label label5;
    }
}