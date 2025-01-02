using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bruhshot {
	internal class QuickSettingDropDown : ToolStripMenuItem {
		string SettingName;
		string SettingsFormName;
		ToolStripMenuItem placeholderMenuItem;

		public QuickSettingDropDown(string text, string settingsFormName, string settingName) {
			Text = text;
			SettingName = settingName;
			SettingsFormName = settingsFormName;

			placeholderMenuItem = new ToolStripMenuItem();
			DropDownItems.Add(placeholderMenuItem);

			DropDownOpening += (_, _) => {
				InitializeItems();
			};
		}

		public void InitializeItems() {
			if (placeholderMenuItem.IsDisposed) return; // already initialized
			placeholderMenuItem.Dispose();

			SettingsForm tempForm = new SettingsForm();
			ComboBox settingDropdown = GetDropdownInForm(tempForm);
			tempForm.Dispose();

			foreach (string name in settingDropdown.Items) {
				ToolStripMenuItem tool = new ToolStripMenuItem(name);
				UpdateDropdownItem(tool);

				tool.Click += (_, _) => {
					SettingsForm form = new SettingsForm();
					GetDropdownInForm(form).SelectedItem = name;
					form.Dispose();

					foreach (ToolStripMenuItem item in DropDownItems) {
						UpdateDropdownItem(item);
					}
				};

				DropDownItems.Add(tool);
			}
		}

		public ComboBox GetDropdownInForm(SettingsForm form) {
			foreach (Control control in form.EditorPage.Controls) {
				if (control.Name != SettingsFormName) continue;
				return (ComboBox)control;
			}
			return null;
		}

		public void UpdateDropdownItem(ToolStripMenuItem item) {
			item.Checked = (string)Properties.Settings.Default[SettingName] == item.Text;
		}
	}
}
