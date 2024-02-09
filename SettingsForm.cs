using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bruhshot {
    public partial class SettingsForm : Form {
        void updateFontText() {
            Font font = Properties.Settings.Default.TextFont;
            FontText.Text = font.Name + ", " + font.SizeInPoints + "pt";
        }

        public SettingsForm() {
            TopMost = true;
            InitializeComponent();
            ColorShowcasePanel.BackColor = Properties.Settings.Default.Color;
            ThicknessValue.ValueChanged += onValueChangedThickness;
            ThicknessValue.Value = (decimal)Properties.Settings.Default.Thickness;
            ShapeSelector.SelectedItem = Properties.Settings.Default.Shape;
            ShapeSelector.SelectedIndexChanged += onSelectionChangedShape;
            ShapeFillCheck.Checked = Properties.Settings.Default.FilledShape;
            ShapeFillCheck.CheckedChanged += onShapeFillChanged;
            LineShapeDropdown.SelectedItem = Properties.Settings.Default.LineShape;
            LineShapeDropdown.SelectedIndexChanged += (object? sender, EventArgs e) => {
                Properties.Settings.Default.LineShape = (string)LineShapeDropdown.SelectedItem;
                Properties.Settings.Default.Save();
            };

            updateFontText();

            KeybindTextbox.Text = Properties.Settings.Default.Keybind;
            KeybindTextbox.TextChanged += onKeybindTextChanged;

            CaptureKeybindInput.Click += captureInput;

            // TODO: Make font text actually work
            FontPickerButton.Click += (object? sender, EventArgs e) => {
                FontDialog.Font = Properties.Settings.Default.TextFont;
                DialogResult result = FontDialog.ShowDialog();
                if (result == DialogResult.OK) {
                    Properties.Settings.Default.TextFont = FontDialog.Font;
                    updateFontText();
                    Properties.Settings.Default.Save();
                }
            };
        }

        private void button1_Click(object sender, EventArgs e) {
            ColorDialog.Color = Properties.Settings.Default.Color;
            DialogResult result = ColorDialog.ShowDialog();
            if (result == DialogResult.OK) {
                Properties.Settings.Default.Color = ColorDialog.Color;
                ColorShowcasePanel.BackColor = Properties.Settings.Default.Color;
                Properties.Settings.Default.Save();
            }
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

        void onShapeFillChanged(object? sender, EventArgs e) {
            Properties.Settings.Default.FilledShape = ShapeFillCheck.Checked;
            Properties.Settings.Default.Save();
        }

        void onKeybindTextChanged(object? sender, EventArgs e) {
            Properties.Settings.Default.Keybind = KeybindTextbox.Text;
            Properties.Settings.Default.Save();
        }
    }
}
