using bruhshot.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bruhshot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CustomApplicationContext());
        }
    }

    public class CustomApplicationContext : ApplicationContext
    {
        public NotifyIcon trayIcon;
        GlobalKeyboardHook _globalKeyboardHook;
        ScreenshotState? screenshotState;

        Bitmap takeScreenshot()
        {
            Screen? curScreen = Screen.FromPoint(Cursor.Position);
            if (curScreen == null) { return new Bitmap(1,1); }
            Rectangle screenBounds = curScreen.Bounds;
            Bitmap bitmap = new Bitmap(screenBounds.Width, screenBounds.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Black);
                g.CopyFromScreen(curScreen.Bounds.Location, Point.Empty, screenBounds.Size);
            }
            return bitmap;
        }

        SettingsForm? settingForm;
        public CustomApplicationContext()
        {
            // Initialize Tray Icon

            var contextMenu = new ContextMenuStrip();
            ToolStripMenuItem titleThingy = new ToolStripMenuItem("Bruhshot", null, null, "Bruhshot");
            titleThingy.Click += showInfo;
            contextMenu.Items.Add(titleThingy);
            contextMenu.Items.Add(new ToolStripSeparator());

            var screenshotButton = new ToolStripMenuItem();
            screenshotButton.Text = "Screenshot";
            screenshotButton.Click += startScreenshottingSender;

            var exitButton = new ToolStripMenuItem();
            exitButton.Text = "Exit";
            exitButton.Click += Exit;

            var settingsButton = new ToolStripMenuItem();
            settingsButton.Text = "Settings";
            settingsButton.Click += (object? sender, EventArgs e) => {
                if (settingForm != null) return;
                settingForm = new SettingsForm();
                settingForm.Show();
            };

            contextMenu.Items.AddRange(new ToolStripItem[] { screenshotButton, settingsButton, exitButton });


            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;

            trayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenuStrip = contextMenu,
                Visible = true
            };

            trayIcon.MouseClick += (object? sender, MouseEventArgs e) => {
                if (e.Button != MouseButtons.Left) return;
                startScreenshotting();
            };
        }

        void showInfo(object? sender, EventArgs e) {
            InfoForm form = new InfoForm();
            form.Show();
        }

        void startScreenshottingSender(object? sender, EventArgs e)
        {
            startScreenshotting();
        }

        void startScreenshotting()
        {
            if (screenshotState == null || (screenshotState != null && screenshotState.IsDisposed))
            {
                screenshotState = new ScreenshotState(takeScreenshot());
                screenshotState.Show();
                screenshotState.Focus();
            }
        }

        private void OnKeyPressed(object? sender, GlobalKeyboardHookEventArgs e)
        {
            // EDT: No need to filter for VkSnapshot anymore. This now gets handled
            // through the constructor of GlobalKeyboardHook(...).
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                // Now you can access both, the key and virtual code
                Keys loggedKey = e.KeyboardData.Key;
                string key = loggedKey.ToString().ToLower();
                if (key == Settings.Default.Keybind.ToLower()) {
                    startScreenshotting();
                } else if (key == "escape") {
                    if (screenshotState != null && !screenshotState.IsDisposed) {
                        screenshotState.Close();
                    }
                }
            }
        }
        void Exit(object? sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
