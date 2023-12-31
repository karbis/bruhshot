﻿using System;
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

            TextSizeChanger.Value = (decimal)Properties.Settings.Default.TextSize;
            TextSizeChanger.ValueChanged += onValueChangedTextSize;

            KeybindTextbox.Text = Properties.Settings.Default.Keybind;
            KeybindTextbox.TextChanged += onKeybindTextChanged;

        }

        private void button1_Click(object sender, EventArgs e) {
            DialogResult result = ColorDialog.ShowDialog();
            if (result == DialogResult.OK) {
                Properties.Settings.Default.Color = ColorDialog.Color;
                ColorShowcasePanel.BackColor = Properties.Settings.Default.Color;
                Properties.Settings.Default.Save();
            }
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

        void onValueChangedTextSize(object? sender, EventArgs e) {
            Properties.Settings.Default.TextSize = (int)TextSizeChanger.Value;
            Properties.Settings.Default.Save();
        }

        void onKeybindTextChanged(object? sender, EventArgs e) {
            Properties.Settings.Default.Keybind = KeybindTextbox.Text;
            Properties.Settings.Default.Save();
        }
    }
}
