using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bruhshot {
	internal class DelayedActionDropDown : ToolStripMenuItem {
		public DelayedActionDropDown(string text, int[] delays, Action action) {
			Text = text;

			foreach (int delay in delays) {
				string timeText = (delay == 1) ? $"{delay} second" : $"{delay} seconds";
				ToolStripMenuItem delayedAction = new ToolStripMenuItem(timeText);

				delayedAction.Click += async (_, _) => {
					Enabled = false;
					await Task.Delay(delay * 1000);
					action();
					Enabled = true;
				};

				DropDownItems.Add(delayedAction);
			}
		}
	}
}
