

using System.Diagnostics;

namespace bruhshot {
    public partial class SettingsForm : Form {
        void updateFontText() {
            Font font = Properties.Settings.Default.TextFont;
            FontText.Text = font.Name + ", " + font.SizeInPoints + "pt";
        }
        void SetUpCheckbox(CheckBox check, string settingName) {
            check.Checked = (bool)Properties.Settings.Default[settingName];
            check.CheckedChanged += (object? sender, EventArgs e) => {
                Properties.Settings.Default[settingName] = check.Checked;
                Properties.Settings.Default.Save();
            };
        }
        public bool DialogOpen = false;

        public SettingsForm() {
            TopMost = true;
            InitializeComponent();
            ColorShowcasePanel.BackColor = Properties.Settings.Default.Color;
            ThicknessValue.ValueChanged += onValueChangedThickness;
            ThicknessValue.Value = (decimal)Properties.Settings.Default.Thickness;
            ShapeSelector.SelectedItem = Properties.Settings.Default.Shape;
            ShapeSelector.SelectedIndexChanged += onSelectionChangedShape;
            LineShapeDropdown.SelectedItem = Properties.Settings.Default.LineShape;
            LineShapeDropdown.SelectedIndexChanged += (object? sender, EventArgs e) => {
                Properties.Settings.Default.LineShape = (string)LineShapeDropdown.SelectedItem;
                Properties.Settings.Default.Save();
            };
            SetUpCheckbox(CaptureCursor, "CaptureCursor");
            SetUpCheckbox(ShapeFillCheck, "FilledShape");
            SetUpCheckbox(AutoSaveOption, "AutoSave");


            updateFontText();

            KeybindTextbox.Text = Properties.Settings.Default.Keybind;
            KeybindTextbox.TextChanged += onKeybindTextChanged;

            AutoSaveLocation.Text = Properties.Settings.Default.AutoSaveLocation;
            AutoSaveLocation.TextChanged += (object? sender, EventArgs e) => {
                Properties.Settings.Default.AutoSaveLocation = AutoSaveLocation.Text;
                Properties.Settings.Default.Save();
            };

            CaptureKeybindInput.Click += captureInput;

            FontPickerButton.Click += (object? sender, EventArgs e) => {
                FontDialog.Font = Properties.Settings.Default.TextFont;
                DialogOpen = true;
                DialogResult result = FontDialog.ShowDialog();
                DialogOpen = false;
                if (result == DialogResult.OK) {
                    Properties.Settings.Default.TextFont = FontDialog.Font;
                    updateFontText();
                    Properties.Settings.Default.Save();
                }
            };

            ChooseFileButton.Click += (object? sender, EventArgs e) => {
                DialogOpen = true;
                DialogResult result = FolderDialog.ShowDialog();
                DialogOpen = false;
                if (result == DialogResult.OK) {
                    AutoSaveLocation.Text = FolderDialog.SelectedPath;
                }
            };
        }

        private void button1_Click(object sender, EventArgs e) {
            ColorDialog.Color = Properties.Settings.Default.Color;
            DialogOpen = true;
            ColorDialog.ShowDialog();
            DialogOpen = false;
            Properties.Settings.Default.Color = ColorDialog.Color;
            ColorShowcasePanel.BackColor = Properties.Settings.Default.Color;
            Properties.Settings.Default.Save();
        }

        GlobalKeyboardHook? _globalKeyboardHook;
        void captureInput(object? sender, EventArgs _) {
            if (_globalKeyboardHook != null) return;
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += (object? sender, GlobalKeyboardHookEventArgs e) => {
                if (e.KeyboardState != GlobalKeyboardHook.KeyboardState.KeyDown) return;
                string loggedKey = e.KeyboardData.Key.ToString();
                KeybindTextbox.Text = loggedKey;
                onKeybindTextChanged(KeybindTextbox, _);
                _globalKeyboardHook.Dispose();
                _globalKeyboardHook = null;
            };
        }

        void onValueChangedThickness(object? sender, EventArgs e) {
            Properties.Settings.Default.Thickness = (int)ThicknessValue.Value;
            Properties.Settings.Default.Save();
        }

        void onSelectionChangedShape(object? sender, EventArgs e) {
            Properties.Settings.Default.Shape = (string)ShapeSelector.SelectedItem;
            Properties.Settings.Default.Save();
        }

        void onKeybindTextChanged(object? sender, EventArgs e) {
            Properties.Settings.Default.Keybind = KeybindTextbox.Text;
            Properties.Settings.Default.Save();
        }
    }
}
