using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace bruhshot
{
    public partial class ScreenshotState : Form
    {
        //PictureBox fullImage; 
        Point startingClickPoint;
        Point endingClickPoint = new Point(-1, 0);
        Image newImage;
        bool mouseDownTransparent = false;
        List<Dictionary<string, dynamic>> edits = new List<Dictionary<string, dynamic>>();
        bool infoPanelDown = false;
        Point lastClickPointTools;
        string currentTool = "None";
        Button[] tools;
        UndoManager undoManager = new UndoManager();

        public ScreenshotState(Bitmap image)
        {
            //TopMost = true;
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            KeyPreview = true;

            Screen? curScreen = Screen.FromPoint(Cursor.Position);
            Rectangle screenBounds = (curScreen == null) ? new Rectangle(0, 0, 0, 0) : curScreen.Bounds;
            Size = new Size(screenBounds.Width, screenBounds.Height);
            FullImage.MouseDown += TransparentMouseDown;
            FullImage.MouseMove += TransparentMouseMove;
            FullImage.MouseUp += TransparentMouseUp;
            KeyDown += ScreenshotState_KeyDown;
            FormClosed += onFormClosed;
            SaveButton.Click += promptToSaveSender;
            ClipboardButton.Click += copyToClipboardSender;
            InvisibleTextbox.GotFocus += invalidateImage;
            InvisibleTextbox.LostFocus += invalidateImage;
            UndoButton.Click += undoButtonClicked;
            RedoButton.Click += redoButtonClicked;

            FullImage.Paint += imageDarken;
            FullImage.Image = image;
            newImage = image;
            DoubleBuffered = true;
            ToolBar.Visible = false;
            tools = new Button[] { PenTool, TextTool, LineTool };

            InvisibleTextbox.TextChanged += onTextChanged;

            foreach (Button v in tools)
            {
                void handleClick(object? sender, EventArgs e)
                {
                    switchMode(v.Name.Substring(0, v.Name.Length - 4));
                }
                v.Click += handleClick;
            }

            undoManager.makeWaypoint(edits);

            //Opacity = 0.25;
        }

        void applyImageEdits(Graphics g, bool relative)
        {
            Size size = Size;
            Point location = new Point();
            if (relative)
            {
                Rectangle crop = getCropRectangle();
                size = crop.Size;
                location = crop.Location;
            }
            Boolean boundCheck(int x1, int y1)
            {
                return x1 > size.Width || x1 < 0 || y1 > size.Height || y1 < 0;
            }
            Dictionary<Color, Brush> brushCache = new Dictionary<Color, Brush>();
            Brush getFromBrushCache(Color col)
            {
                if (!brushCache.ContainsKey(col))
                {
                    brushCache[col] = new SolidBrush(col);
                }
                return brushCache[col];
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;
            int x, y;
            foreach (Dictionary<string, dynamic> v in edits)
            {
                switch (v["Type"])
                {
                    case "Pen":
                        x = v["Location"].X - location.X;
                        y = v["Location"].Y - location.Y;
                        if (boundCheck(x, y)) { return; }
                        g.FillEllipse(getFromBrushCache(Color.Red), new Rectangle(new Point(x, y), new Size(5, 5)));
                        break;
                    case "Text":
                        x = v["Location"].X - location.X;
                        y = v["Location"].Y - location.Y;
                        Font font = new Font("Arial", 12);
                        g.DrawString(v["Text"], font, getFromBrushCache(Color.Red), new Point(x, y));
                        font.Dispose();
                        break;
                    case "Line":
                        using (Pen pen = new Pen(getFromBrushCache(Color.Red)))
                        {
                            pen.Width = 4f;
                            x = v["StartLocation"].X - location.X;
                            y = v["StartLocation"].Y - location.Y;
                            int x2 = v["EndLocation"].X - location.X;
                            int y2 = v["EndLocation"].Y - location.Y;
                            g.DrawLine(pen, new Point(x, y), new Point(x2, y2));
                        }
                        break;
                    default:
                        break;
                }
            }
            foreach (Brush brush in brushCache.Values)
            {
                brush.Dispose();
            }
        }

        void cropImage()
        {
            if (endingClickPoint.X == -1) { return; }
            Rectangle crop = getCropRectangle();
            Bitmap croppedImage = new Bitmap(Math.Max(1, crop.Width), Math.Max(1, crop.Height));
            using (Graphics g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(newImage, new Rectangle(0, 0, crop.Width, crop.Height), crop, GraphicsUnit.Pixel);
                applyImageEdits(g, true);
            }
            newImage = croppedImage;
        }

        Point lerpPoint(Point start, Point end, double t)
        {
            int x = (int)(start.X + (end.X - start.X) * t);
            int y = (int)(start.Y + (end.Y - start.Y) * t);

            return new Point(x, y);
        }
        bool rectangleContains(Point point, Rectangle rectangle)
        {
            return point.X >= rectangle.Left + 1 && point.X <= rectangle.Right - 1 &&
                   point.Y >= rectangle.Top + 1 && point.Y <= rectangle.Bottom - 1;
        }

        void copyToClipboard()
        {
            cropImage();
            MemoryStream pngConverter = new MemoryStream();
            newImage.Save(pngConverter, System.Drawing.Imaging.ImageFormat.Png);
            Clipboard.SetImage(Image.FromStream(pngConverter));

            pngConverter.Dispose();
            newImage.Dispose();
            Close();
        }

        void ScreenshotState_KeyDown(object? sender, KeyEventArgs e)
        {
            if (!e.Control) { return; }
            switch (e.KeyCode)
            {
                case Keys.C:
                    copyToClipboard();
                    break;
                case Keys.S:
                    promptToSave();
                    break;
                case Keys.Z:
                    edits = undoManager.undo();
                    FullImage.Invalidate();
                    break;
                case Keys.Y:
                    edits = undoManager.redo();
                    FullImage.Invalidate();
                    break;
                default:
                    break;
            }
        }
        
        void undoButtonClicked(object? sender, EventArgs e) {
            edits = undoManager.undo();
            FullImage.Invalidate();
        }
        void redoButtonClicked(object? sender, EventArgs e) {
            edits = undoManager.redo();
            FullImage.Invalidate();
        }

        void TransparentMouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { return; }
            if (rectangleContains(e.Location, getCropRectangle()))
            {
                InfoPanelDown(sender, e);
                return;
            }
            startingClickPoint = e.Location;
            mouseDownTransparent = true;
            infoPanelDown = false;
            ToolBar.Visible = false;
        }
        void TransparentMouseMove(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { return; }
            if (rectangleContains(e.Location, getCropRectangle()) && !mouseDownTransparent)
            {
                InfoPanelMove(sender, e);
                return;
            }
            if (mouseDownTransparent)
            {
                endingClickPoint = e.Location;
                FullImage.Invalidate();
            }
        }
        void TransparentMouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { return; }
            if (rectangleContains(e.Location, getCropRectangle()))
            {
                InfoPanelUp(sender, e);
                return;
            }
            mouseDownTransparent = false;
            ToolBar.Visible = true;
        }

        void drawPen(Point location)
        {
            Point pointAToPointB = new Point(location.X - lastClickPointTools.X, location.Y - lastClickPointTools.Y);
            double amountOfDots = Math.Max(1, Math.Round(Math.Abs(Math.Sqrt(pointAToPointB.X * pointAToPointB.X + pointAToPointB.Y * pointAToPointB.Y)) / (5 / Math.PI)));
            for (int i = 0; i < amountOfDots; i++)
            {
                edits.Add(new Dictionary<string, dynamic> { { "Type", "Pen" }, { "Location", lerpPoint(lastClickPointTools, location, i / amountOfDots) } });
            }
            FullImage.Invalidate();

        }

        void InfoPanelDown(object? sender, MouseEventArgs e)
        {
            infoPanelDown = true;
            lastClickPointTools = e.Location;
            switch (currentTool)
            {
                case "Pen":
                    drawPen(e.Location);
                    break;
                case "Text":
                    if (!InvisibleTextbox.Focused)
                    {
                        InvisibleTextbox.Focus();
                        InvisibleTextbox.Text = "";
                        edits.Add(new Dictionary<string, dynamic> { { "Type", "Text" }, { "Text", "" }, { "Location", e.Location } });
                    }
                    break;
                case "Line":
                    edits.Add(new Dictionary<string, dynamic> { { "Type", "Line" }, { "StartLocation", e.Location }, { "EndLocation", e.Location } });
                    break;
                default:
                    break;
            }
        }
        void InfoPanelUp(object? sender, MouseEventArgs e)
        {
            if (currentTool != "None" && infoPanelDown)
            {
                Trace.WriteLine("History");
                undoManager.makeWaypoint(edits);
            }
            infoPanelDown = false;
        }
        void InfoPanelMove(object? sender, MouseEventArgs e)
        {
            if (!infoPanelDown) { return; }
            Point offset;
            switch (currentTool)
            {
                case "Pen":
                    drawPen(e.Location);
                    break;
                case "Text":
                    if (InvisibleTextbox.Focused)
                    {
                        offset = new Point(e.Location.X - lastClickPointTools.X, e.Location.Y - lastClickPointTools.Y);
                        Dictionary<string, dynamic> lastIndex = edits[edits.Count - 1];
                        lastIndex["Location"] = new Point(lastIndex["Location"].X + offset.X, lastIndex["Location"].Y + offset.Y);
                        FullImage.Invalidate();
                    }
                    break;
                case "None":
                    offset = new Point(e.Location.X - lastClickPointTools.X, e.Location.Y - lastClickPointTools.Y);
                    startingClickPoint = new Point(Math.Clamp(startingClickPoint.X + offset.X, 0, Size.Width), Math.Clamp(startingClickPoint.Y + offset.Y, 0, Size.Height));
                    endingClickPoint = new Point(Math.Clamp(endingClickPoint.X + offset.X, 0, Size.Width), Math.Clamp(endingClickPoint.Y + offset.Y, 0, Size.Height));
                    FullImage.Invalidate();
                    break;
                case "Line":
                    edits[edits.Count - 1]["EndLocation"] = e.Location;
                    FullImage.Invalidate();
                    break;
                default:
                    break;
            }

            //edits.Add(new {Type="Pen",Location=e.Location });
            lastClickPointTools = e.Location;
        }

        Rectangle getCropRectangle()
        {
            if (endingClickPoint.X == -1) { return new Rectangle(); }
            return correctRectangle(new Rectangle(startingClickPoint.X, startingClickPoint.Y, endingClickPoint.X - startingClickPoint.X, endingClickPoint.Y - startingClickPoint.Y));
        }

        Rectangle correctRectangle(Rectangle rectangle)
        {
            Rectangle newRectangle = new Rectangle(rectangle.X, rectangle.Y, Math.Abs(rectangle.Width), Math.Abs(rectangle.Height));
            if (rectangle.Width < 0)
            {
                newRectangle.X += rectangle.Width;
            }
            if (rectangle.Height < 0)
            {
                newRectangle.Y += rectangle.Height;
            }
            return newRectangle;
        }

        void imageDarken(object? sender, PaintEventArgs e)
        {
            //e.Graphics.DrawImage(FullImage.Image, FullImage.ClientRectangle);
            //base.OnPaint(e);
            Rectangle regionRectangle = getCropRectangle();
            Region oldClip = e.Graphics.Clip.Clone();

            using (Brush brush = new SolidBrush(Color.FromArgb(128, Color.Black)))
            {
                if (endingClickPoint.X != -1)
                {
                    e.Graphics.ExcludeClip(regionRectangle);
                }
                e.Graphics.FillRectangle(brush, FullImage.ClientRectangle);
            }
            e.Graphics.Clip = oldClip;

            if (endingClickPoint.X != -1)
            {
                Pen dashedPen = new Pen(Color.LightGray);
                dashedPen.DashStyle = DashStyle.Dash;
                dashedPen.Width = 2f;

                e.Graphics.DrawRectangle(dashedPen, new Rectangle(regionRectangle.X - 1, regionRectangle.Y - 1, regionRectangle.Width + 1, regionRectangle.Height + 1));

                if (InvisibleTextbox.Focused)
                {
                    Point textLocation = edits[edits.Count - 1]["Location"];
                    dashedPen.Width = 1f;
                    dashedPen.Color = Color.FromArgb(128, Color.LightGray);
                    e.Graphics.DrawRectangle(dashedPen, new Rectangle(textLocation.X - 1, textLocation.Y - 1, 100, 50));
                }

                dashedPen.Dispose();
            }

            applyImageEdits(e.Graphics, false);
            int offsetX = regionRectangle.Width + 5;
            int offsetY = -2;
            if (regionRectangle.Bottom + 5 > Size.Height)
            {
                offsetY = -ToolBar.Size.Height - 5;
                if (regionRectangle.Y + offsetY < 0)
                {
                    offsetY = Size.Height - 5 - ToolBar.Size.Height;
                }
            }
            if (regionRectangle.Right + 30 > Size.Width)
            {
                offsetX = -30;
                if (regionRectangle.X - 30 < 0)
                {
                    offsetY = regionRectangle.Height + 5;
                    offsetX = Size.Width - 30;
                }
            }
            ToolBar.Location = new Point(regionRectangle.X + offsetX, regionRectangle.Y + offsetY);

        }
        void onFormClosed(object? sender, FormClosedEventArgs e)
        {
            newImage.Dispose();
        }

        void promptToSave()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            saveFileDialog.Title = "Save Screenshot";
            saveFileDialog.DefaultExt = "png";
            saveFileDialog.AddExtension = true;

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                cropImage();
                newImage.Save(saveFileDialog.FileName);
                Close();
            }
        }

        void promptToSaveSender(object? sender, EventArgs e)
        {
            promptToSave();
        }

        void switchMode(string newMode)
        {
            if (currentTool == newMode)
            {
                currentTool = "None";
            } else
            {
                currentTool = newMode;
            }
            foreach (Button v in tools)
            {
                v.BackColor = (v.Name == (currentTool + "Tool")) ? Color.FromArgb(255, 224, 224, 255) : Color.FromArgb(255, 224, 224, 224);
            }
        }

        void copyToClipboardSender(object? sender, EventArgs e)
        {
            copyToClipboard();
        }

        void onTextChanged(object? sender, EventArgs e)
        {
            if (sender == null) { return; }
            edits[edits.Count - 1]["Text"] = ((System.Windows.Forms.TextBox)sender).Text;
            FullImage.Invalidate();
        }

        void invalidateImage(object? sender, EventArgs e)
        {
            FullImage.Invalidate();
        }
    }
}