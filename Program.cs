using bruhshot.Properties;
using System;
using System.Collections.Generic;
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
        private NotifyIcon trayIcon;
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
                g.CopyFromScreen(Point.Empty, Point.Empty, screenBounds.Size);
            }
            return bitmap;
        }

        public CustomApplicationContext()
        {
            // Initialize Tray Icon

            var contextMenu = new ContextMenuStrip();
            ToolStripMenuItem titleThingy = new ToolStripMenuItem("Bruhshot", null, null, "Bruhshot");
            contextMenu.Items.Add(titleThingy);
            contextMenu.Items.Add(new ToolStripSeparator());

            var screenshotButton = new ToolStripMenuItem();
            contextMenu.Items.AddRange(new ToolStripItem[] { screenshotButton });
            screenshotButton.Text = "Screenshot";
            screenshotButton.Click += startScreenshottingSender;

            var exitButton = new ToolStripMenuItem();
            contextMenu.Items.AddRange(new ToolStripItem[] { exitButton });
            exitButton.Text = "Exit";
            exitButton.Click += Exit;

            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;

            trayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenuStrip = contextMenu,
                Visible = true
            };
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
                int loggedVkCode = e.KeyboardData.VirtualCode;
                switch (loggedKey.ToString().ToLower())
                {
                    case "f2":
                        startScreenshotting();
                        break;
                    case "escape":
                        if (screenshotState != null && !screenshotState.IsDisposed)
                        {
                            screenshotState.Close();
                        }
                        break;
                    default:
                        break;
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
