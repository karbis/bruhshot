using bruhshot.Properties;
using System.Drawing.Drawing2D;
using System.Reflection;
using Button = System.Windows.Forms.Button;

namespace bruhshot {
	public partial class ScreenshotState : Form {
		//PictureBox fullImage; 
		public Point startingClickPoint;
		public Point endingClickPoint = new Point(-1, 0);
		Image newImage;
		bool mouseDownTransparent = false;
		List<Dictionary<string, dynamic>> edits = new List<Dictionary<string, dynamic>>();
		bool infoPanelDown = false;
		Point lastClickPointTools;
		string currentTool = "None";
		Button[] tools;
		UndoManager undoManager = new UndoManager();
		public SettingsForm? settingsForm = null;
		Bitmap image;
		int? dragId;

		public ScreenshotState(Bitmap screenImage) {
			image = screenImage;
			TopMost = true;
			InitializeComponent();
			Focus();
			FormBorderStyle = FormBorderStyle.None;
			WindowState = FormWindowState.Maximized;
			KeyPreview = true;

			Screen? curScreen = Screen.FromPoint(Cursor.Position);

			if (curScreen != null) {
				StartPosition = FormStartPosition.Manual;
				Location = curScreen.WorkingArea.Location;
			}

			Rectangle screenBounds = (curScreen == null) ? new Rectangle(0, 0, 0, 0) : curScreen.Bounds;
			Size = new Size(screenBounds.Width, screenBounds.Height);
			MouseDown += TransparentMouseDown;
			MouseMove += TransparentMouseMove;
			MouseUp += TransparentMouseUp;
			KeyDown += ScreenshotState_KeyDown;
			FormClosed += onFormClosed;
			SaveButton.Click += promptToSaveSender;
			ClipboardButton.Click += copyToClipboardSender;
			InvisibleTextbox.GotFocus += invalidateImage;
			InvisibleTextbox.LostFocus += invalidateImage;
			UndoButton.Click += undoButtonClicked;
			RedoButton.Click += redoButtonClicked;
			SettingsButton.Click += showSettings;
			CloseButton.Click += closeForm;
			InvisibleTextbox.LostFocus += disableTextbox;
			MouseMove += GlobalMouseMove;

			//FullImage.Paint += imageDarken;
			//FullImage.Image = image;
			Paint += imageDarken;
			newImage = image;
			DoubleBuffered = true;
			ToolBar.Visible = false;
			tools = new Button[] { PenTool, TextTool, LineTool, ShapeTool };

			InvisibleTextbox.TextChanged += onTextChanged;
			Seperator1.BackColor = Color.FromArgb(128, 0, 0, 0);
			Seperator2.BackColor = Color.FromArgb(128, 0, 0, 0);

			typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, ToolBar, new object[] { true });

			foreach (Button v in tools) {
				void handleClick(object? sender, EventArgs e) {
					switchMode(v.Name.Substring(0, v.Name.Length - 4));
				}
				v.Click += handleClick;
			}

			undoManager.makeWaypoint(edits);
			GlobalMouseMove();
			setupQuickSettings();
			UpdateToolTips();

			// dpi fix
			foreach (Control control in ToolBar.Controls) {
				if (control.GetType() != typeof(Button)) continue;
				Button button = (Button)control;
				if (button.Image == null) continue;

				Size size = control.Size;
				if (size == new Size(29, 29)) break; // using 100% scaling, no fix needed
				int minSize = Math.Min(size.Width, size.Height);
				int finalSize = minSize * 25 / 29;
				finalSize -= Math.Abs(minSize % 2 - finalSize % 2); // center
				Image newImage = new Bitmap(finalSize, finalSize);
				using (Graphics g = Graphics.FromImage(newImage)) {
					g.PixelOffsetMode = PixelOffsetMode.HighQuality;
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;
					g.DrawImage(button.Image, new Rectangle(Point.Empty, newImage.Size));
				}
				button.Image = newImage;
			}

			//Opacity = 0.25;
		}

		GraphicsPath CreateTriangle(int x, int y, int x2, int y2) {
			GraphicsPath path = new GraphicsPath();
			if (x > x2) {
				int tempX = x;
				x = x2;
				x2 = tempX;
			}
			if (y > y2) {
				int tempY = y;
				y = y2;
				y2 = tempY;
			}
			int width = x2 - x;
			path.StartFigure();
			path.AddLine(new Point(x, y2), new Point(x + width / 2, y));
			path.AddLine(new Point(x + width / 2, y), new Point(x2, y2));
			path.CloseFigure();

			return path;
		}

		void applyImageEdits(Graphics g, bool relative) {
			Size size = Size;
			Point location = new Point();
			if (relative) {
				Rectangle crop = getCropRectangle();
				size = crop.Size;
				location = crop.Location;
			}
			Dictionary<Color, Brush> brushCache = new Dictionary<Color, Brush>();
			Brush getFromBrushCache(Color col) {
				if (!brushCache.ContainsKey(col)) {
					brushCache[col] = new SolidBrush(col);
				}
				return brushCache[col];
			}

			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
			int x, y;
			foreach (Dictionary<string, dynamic> v in edits) {
				switch (v["Type"]) {
					case "Pen":
						if (v["FromPoint"] == v["ToPoint"]) {
							x = v["FromPoint"].X - location.X;
							y = v["FromPoint"].Y - location.Y;
							int thickness = v["Thickness"];
							g.FillEllipse(getFromBrushCache(v["Color"]), new Rectangle(x - thickness / 2, y - thickness / 2, thickness, thickness));
							break;
						}
						using (Pen pen = new Pen(getFromBrushCache(v["Color"]))) {
							pen.Width = v["Thickness"];
							pen.StartCap = LineCap.Round;
							pen.EndCap = LineCap.Round;
							x = v["FromPoint"].X - location.X;
							y = v["FromPoint"].Y - location.Y;
							int x2 = v["ToPoint"].X - location.X;
							int y2 = v["ToPoint"].Y - location.Y;
							g.DrawLine(pen, new Point(x, y), new Point(x2, y2));
						}
						break;
					case "Text":
						x = v["Location"].X - location.X;
						y = v["Location"].Y - location.Y;
						g.DrawString(v["Text"], v["Font"], getFromBrushCache(v["Color"]), new Point(x, y));
						break;
					case "Line":
						using (Pen pen = new Pen(getFromBrushCache(v["Color"]))) {
							pen.Width = v["Thickness"];
							pen.DashStyle = v["Shape"] == "Dashed" ? DashStyle.Dash : DashStyle.Solid;
							if (v["Shape"] == "Arrow") {
								pen.StartCap = LineCap.Round;
								pen.EndCap = LineCap.Round;
							}
							x = v["StartLocation"].X - location.X;
							y = v["StartLocation"].Y - location.Y;
							int x2 = v["EndLocation"].X - location.X;
							int y2 = v["EndLocation"].Y - location.Y;
							g.DrawLine(pen, new Point(x, y), new Point(x2, y2));

							if (v["Shape"] != "Arrow") continue;
							double angle = -Math.Atan2(y2 - y, x2 - x);
							double RAD_CONSTANT = Math.PI / 180;
							Point lineSize = new Point(x2 - x, y2 - y);
							double arrowSize = Math.Min(Math.Clamp(pen.Width, 3, 6)*10, Math.Sqrt(Math.Pow(lineSize.X, 2) + Math.Pow(lineSize.Y, 2)) / 2.25);
							int offsetX = (int)(Math.Sin(angle + 66 * RAD_CONSTANT) * arrowSize);
							int offsetY = (int)(Math.Cos(angle + 66 * RAD_CONSTANT) * arrowSize);
							int offsetX2 = (int)(Math.Sin(angle - 66 * RAD_CONSTANT) * arrowSize);
							int offsetY2 = (int)(Math.Cos(angle - 66 * RAD_CONSTANT) * arrowSize);
							g.DrawLine(pen, new Point(x2, y2), new Point(x2 - offsetX, y2 - offsetY));
							g.DrawLine(pen, new Point(x2, y2), new Point(x2 + offsetX2, y2 + offsetY2));
						}
						break;
					case "Shape":
						using (Pen pen = new Pen(getFromBrushCache(v["Color"]))) {
							pen.Width = v["Thickness"];
							x = v["StartLocation"].X - location.X;
							y = v["StartLocation"].Y - location.Y;
							int x2 = v["EndLocation"].X - location.X - x;
							int y2 = v["EndLocation"].Y - location.Y - y;
							Pen a = pen;
							Rectangle b = correctRectangle(new Rectangle(new Point(x, y), new Size(x2, y2)));
							Brush c = getFromBrushCache(v["Color"]);
							switch (v["Shape"]) {
								case "Square":
									g.DrawRectangle(a, b);
									break;
								case "Circle":
									g.DrawEllipse(a, b);
									break;
								case "Triangle":
									g.DrawPath(a, CreateTriangle(x, y, x2 + x, y2 + y));
									break;
								case "FilledSquare":
									g.FillRectangle(c, b);
									break;
								case "FilledCircle":
									g.FillEllipse(c, b);
									break;
								case "FilledTriangle":
									g.FillPath(c, CreateTriangle(v["StartLocation"].X, v["StartLocation"].Y, v["EndLocation"].X, v["EndLocation"].Y));
									break;
							}
						}
						break;
					default:
						break;
				}
			}
			foreach (Brush brush in brushCache.Values) {
				brush.Dispose();
			}
		}

		void cropImage() {
			if (endingClickPoint.X == -1) { return; }
			Rectangle crop = getCropRectangle();
			Bitmap croppedImage = new Bitmap(Math.Max(1, crop.Width), Math.Max(1, crop.Height));
			using (Graphics g = Graphics.FromImage(croppedImage)) {
				g.DrawImage(newImage, new Rectangle(0, 0, crop.Width, crop.Height), crop, GraphicsUnit.Pixel);
				applyImageEdits(g, true);
				/*using (Brush b = new SolidBrush(Color.FromArgb(128, 255, 255, 255))) {
					Font font = new Font("Arial", 10);
					SizeF measured = g.MeasureString("IMAGE TAKEN WITH BRUHSHOT", font);
					float pixelsPerEm = measured.Width / 10;
					float min = Math.Min(crop.Width,crop.Height)/pixelsPerEm;
					font.Dispose();
					Font newFont = new Font("Arial", min);
					g.DrawString("IMAGE TAKEN WITH BRUHSHOT",newFont,b,new PointF(crop.Width/2-(pixelsPerEm*min/2),crop.Height/2-measured.Height/2));
				};*/ // not actually adding this
			}
			newImage = croppedImage;
		}

		bool rectangleContains(Point point, Rectangle rectangle) {
			return point.X >= rectangle.Left + 1 && point.X <= rectangle.Right - 1 &&
				   point.Y >= rectangle.Top + 1 && point.Y <= rectangle.Bottom - 1;
		}

		void copyToClipboard() {
			cropImage();
			MemoryStream pngConverter = new MemoryStream();
			newImage.Save(pngConverter, System.Drawing.Imaging.ImageFormat.Png);
			try {
				Clipboard.SetImage(Image.FromStream(pngConverter));
			} catch (Exception e) {
				MessageBox.Show("Failed to copy image to clipboard\n\n" + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			string autoSaveLocation = getSetting("AutoSaveLocation");
			if (getSetting("AutoSave") && Directory.Exists(autoSaveLocation)) {
				CustomApplicationContext.SaveImage(newImage, Path.Combine(autoSaveLocation, GetCurrentTime() + ".png"));
			}

			pngConverter.Dispose();
			newImage.Dispose();
			Close();
		}

		string GetCurrentTime() {
			DateTime now = DateTime.Now;
			return now.Year + "-" + now.Month.ToString().PadLeft(2, '0') + "-" + now.Day.ToString().PadLeft(2, '0') + " "
				+ now.Hour + "-" + now.Minute.ToString().PadLeft(2, '0') + "-" + now.Second.ToString().PadLeft(2, '0');
		}

		void ScreenshotState_KeyDown(object? sender, KeyEventArgs e) {
			if (!e.Control) { return; }
			switch (e.KeyCode) {
				case Keys.C:
					copyToClipboard();
					break;
				case Keys.S:
					promptToSave();
					break;
				case Keys.Z:
					if (currentTool != "None" && infoPanelDown) {
						undoManager.makeWaypoint(edits);
						infoPanelDown = false;
					}
					undoButton();
					break;
				case Keys.Y:
					if (currentTool != "None" && infoPanelDown) break;
					redoButton();
					break;
				case Keys.A:
					startingClickPoint = new Point();
					endingClickPoint = new Point(Size.Width, Size.Height);
					ToolBar.Visible = true;
					Invalidate();
					break;
				case Keys.Oemplus:
				case Keys.Add:
					IncrementZoom(1);
					break;
				case Keys.OemMinus:
				case Keys.Subtract:
					IncrementZoom(-1);
					break;
				default:
					break;
			}
		}

		void IncrementZoom(short delta) {
			if (currentTool == "None") return;
			if (settingsForm != null && !settingsForm.IsDisposed) return;
			string editProperty;
			object value;

			float zoomIn(float n, float amount, float min, float max) {
				if ((n <= min && delta == -1) || (n >= max && delta == 1)) return n;
				float mult = 1.25f + (float)Math.Clamp((30-n+delta)/30, 0, .25);
				mult = (mult - 1) * amount + 1;
				float floored = (float)Math.Floor((double)((delta == 1) ? n * mult : n / mult));
				if (floored == n) return n + delta;
				return Math.Clamp(floored + n % 1, min, max);
			}

			if (currentTool == "Text") {
				Font curFont = getSetting("TextFont");
				Font font = new Font(curFont.FontFamily, zoomIn(curFont.Size, .4f, 8, 400), curFont.Style);
				Settings.Default.TextFont = font;
				Settings.Default.Save();
				editProperty = "Font";
				value = font;
			} else {
				Settings.Default.Thickness = (int)zoomIn(getSetting("Thickness"), 1, 1, 500);
				Settings.Default.Save();
				editProperty = "Thickness";
				value = getSetting("Thickness");
			}

			int lastEditIndex = edits.Count - 1;
			if (edits.ElementAtOrDefault(lastEditIndex) != null && edits[lastEditIndex]["Type"] == currentTool && (infoPanelDown || (currentTool == "Text" && InvisibleTextbox.Focused))) {
				edits[lastEditIndex][editProperty] = value;
			}
			Invalidate();
		}
		void undoButton() {
			if (InvisibleTextbox.Focused) { return; }
			edits = undoManager.undo();
			Invalidate();
		}
		void redoButton() {
			if (InvisibleTextbox.Focused) { return; }
			edits = undoManager.redo();
			Invalidate();
		}

		dynamic getSetting(string setting) {
			return Properties.Settings.Default[setting];
		}

		public void UpdateToolTips() {
			string curLine = getSetting("LineShape");
			string curShape = getSetting("Shape");
			string shapeText = getSetting("FilledShape") ? $"Filled {curShape.ToLower()}" : curShape;
			string lineAppend = (curLine == "Line") ? "" : $" ({curLine})";

			ToolTip.SetToolTip(LineTool, "Line" + lineAppend);
			ToolTip.SetToolTip(ShapeTool, $"Shape ({shapeText})");
		}

		void undoButtonClicked(object? sender, EventArgs e) {
			undoButton();
		}
		void redoButtonClicked(object? sender, EventArgs e) {
			redoButton();
		}

		void CornerPosCheck(Point loc) {
			Rectangle[] cornerRects = GetCornerRectangles(1);
			for (int i = 0; i < cornerRects.Length; i++) {
				Rectangle rect = cornerRects[i];
				if (!rectangleContains(loc, rect)) continue;
				dragId = i;

				Rectangle crop = getCropRectangle();
				startingClickPoint = new Point(crop.X, crop.Y);
				endingClickPoint = new Point(crop.Right, crop.Bottom);
				Point curStart = startingClickPoint;
				Point curEnd = endingClickPoint;
				if (dragId == 1 || dragId == 7) {
					startingClickPoint = new Point(curEnd.X, curStart.Y);
					endingClickPoint = new Point(curStart.X, curEnd.Y);
				} else if (dragId == 2 || dragId == 5) {
					startingClickPoint = new Point(curStart.X, curEnd.Y);
					endingClickPoint = new Point(curEnd.X, curStart.Y);
				} else if (dragId == 3 || dragId == 6) {
					startingClickPoint = curEnd;
					endingClickPoint = curStart;
				}

				return;
			}
		}
		void CornerRectChange(Point loc) {
			if (dragId != null) {
				switch (dragId) {
					case 6:
					case 4:
						startingClickPoint = new Point(startingClickPoint.X, loc.Y);
						break;
					case 7:
					case 5:
						startingClickPoint = new Point(loc.X, startingClickPoint.Y);
						break;
					default:
						startingClickPoint = loc;
						break;
				}
				if ((ModifierKeys & Keys.Shift) == Keys.Shift) {
					startingClickPoint = CreateSquarePoints(endingClickPoint, loc);
				}
				Invalidate();
			}
		}

		void TransparentMouseDown(object? sender, MouseEventArgs e) {
			if (e.Button != MouseButtons.Left) { return; }
			CornerPosCheck(e.Location);
			if (dragId != null) return;
			if (rectangleContains(e.Location, getCropRectangle())) {
				InfoPanelDown(sender, e);
				return;
			}
			startingClickPoint = e.Location;
			mouseDownTransparent = true;
			infoPanelDown = false;
			ToolBar.Visible = false;
		}
		void TransparentMouseMove(object? sender, MouseEventArgs e) {
			if (currentTool == "Pen") {
				Invalidate();
			}
			CornerRectChange(e.Location);
			if (dragId != null) return;
			if (e.Button != MouseButtons.Left) { return; }
			if (rectangleContains(e.Location, getCropRectangle()) && !mouseDownTransparent) {
				InfoPanelMove(sender, e);
				return;
			}
			if (mouseDownTransparent) {
				endingClickPoint = new Point(e.Location.X + 1, e.Location.Y + 1);
				if ((ModifierKeys & Keys.Shift) == Keys.Shift) {
					endingClickPoint = CreateSquarePoints(startingClickPoint, endingClickPoint);
				}
				Invalidate();
			}
		}
		void TransparentMouseUp(object? sender, MouseEventArgs e) {
			if (e.Button != MouseButtons.Left) { return; }
			dragId = null;
			if (rectangleContains(new Point(e.Location.X + 1, e.Location.Y + 1), getCropRectangle())) {
				InfoPanelUp(sender, e);
				return;
			}
			mouseDownTransparent = false;
			ToolBar.Visible = true;
		}

		void drawPen(Point location) {
			edits.Add(new Dictionary<string, dynamic> { { "Type", "Pen" }, { "FromPoint", lastClickPointTools }, { "ToPoint", location }, { "Color", getSetting("Color") }, { "Thickness", getSetting("Thickness") } });
			Invalidate();
		}

		void InfoPanelDown(object? sender, MouseEventArgs e) {
			//CornerPosCheck(e.Location);
			//if (dragId != null) return;
			infoPanelDown = true;
			lastClickPointTools = e.Location;
			switch (currentTool) {
				case "Pen":
					drawPen(e.Location);
					break;
				case "Text":
					bool allowed = true;
					Dictionary<string, dynamic>? lastIndex = (edits.Count >= 1) ? edits[edits.Count - 1] : null;
					if (lastIndex != null && lastIndex["Type"] == "Text") {
						Size size = GetTextboxSize(lastIndex["Text"], lastIndex["Font"]);
						if (new Rectangle(lastIndex["Location"], size).Contains(e.Location)) {
							allowed = false;
						}
					}
					if (allowed) {
						edits.Add(new Dictionary<string, dynamic> { { "Type", "Text" }, { "Text", "" }, { "Location", e.Location }, { "Color", getSetting("Color") }, { "Font", getSetting("TextFont") } });
						InvisibleTextbox.Enabled = true;
						InvisibleTextbox.Focus();
						InvisibleTextbox.Text = "";
					}
					break;
				case "Line":
					edits.Add(new Dictionary<string, dynamic> { { "Type", "Line" }, { "StartLocation", e.Location }, { "EndLocation", e.Location }, { "Color", getSetting("Color") }, { "Thickness", getSetting("Thickness") }, { "Shape", getSetting("LineShape") } });
					break;
				case "Shape":
					edits.Add(new Dictionary<string, dynamic> { { "Type", "Shape" }, { "Shape", ((getSetting("FilledShape")) ? "Filled" : "") + getSetting("Shape") }, { "StartLocation", e.Location }, { "EndLocation", e.Location }, { "Color", getSetting("Color") }, { "Thickness", getSetting("Thickness") } });
					break;
				default:
					break;
			}
		}
		void InfoPanelUp(object? sender, MouseEventArgs e) {
			if (e.Button != MouseButtons.Left) { return; }
			if (currentTool != "None" && infoPanelDown) {
				undoManager.makeWaypoint(edits);
			}
			infoPanelDown = false;
			dragId = null;
		}

		Point correctPoint(Point point) {
			return new Point(Math.Clamp(point.X, 0, Size.Width), Math.Clamp(point.Y, 0, Size.Height));
		}

		Rectangle centerRectangle(Rectangle rectangle) {
			int size = rectangle.Size.Width;
			return new Rectangle(new Point(rectangle.X - size / 2, rectangle.Y - size / 2), rectangle.Size);
		}

		Point CreateSquarePoints(Point startLoc, Point endLoc) {
			int size = (int)Math.Min(Math.Abs(endLoc.X - startLoc.X), Math.Abs(endLoc.Y - startLoc.Y));
			int xSign = Math.Sign(endLoc.X - startLoc.X);
			int ySign = Math.Sign(endLoc.Y - startLoc.Y);
			return new Point(startLoc.X + size * xSign, startLoc.Y + size * ySign);
		}

		void InfoPanelMove(object? sender, MouseEventArgs e) {
			CornerRectChange(e.Location);
			if (dragId != null) return;
			if (!infoPanelDown) { return; }
			Point offset;
			switch (currentTool) {
				case "Pen":
					drawPen(e.Location);
					break;
				case "Text":
					if (InvisibleTextbox.Focused) {
						offset = new Point(e.Location.X - lastClickPointTools.X, e.Location.Y - lastClickPointTools.Y);
						Dictionary<string, dynamic> lastIndex = edits[edits.Count - 1];
						lastIndex["Location"] = new Point(lastIndex["Location"].X + offset.X, lastIndex["Location"].Y + offset.Y);
						Invalidate();
					}
					break;
				case "None":
					offset = new Point(e.Location.X - lastClickPointTools.X, e.Location.Y - lastClickPointTools.Y);
					startingClickPoint = correctPoint(new Point(startingClickPoint.X + offset.X, startingClickPoint.Y + offset.Y));
					endingClickPoint = correctPoint(new Point(endingClickPoint.X + offset.X, endingClickPoint.Y + offset.Y));
					Invalidate();
					break;
				case "Line":
					Dictionary<string, dynamic> edit = edits[edits.Count - 1];
					edit["EndLocation"] = e.Location;
					if ((ModifierKeys & Keys.Shift) == Keys.Shift) {
						int x2 = edit["EndLocation"].X;
						int x = edit["StartLocation"].X;
						int y2 = edit["EndLocation"].Y;
						int y = edit["StartLocation"].Y;
						double angle = -Math.Atan2(y2 - y, x2 - x);
						const double FIFTEEN_DEGREES = Math.PI / 180 * 15;
						angle = Math.Floor(angle / FIFTEEN_DEGREES + FIFTEEN_DEGREES / 2) * FIFTEEN_DEGREES + FIFTEEN_DEGREES * 6;
						Point lineSize = new Point(x2 - x, y2 - y);
						double lineLength = Math.Sqrt(Math.Pow(lineSize.X, 2) + Math.Pow(lineSize.Y, 2));
						edit["EndLocation"] = new Point(x + (int)(Math.Sin(angle) * lineLength), y + (int)(Math.Cos(angle) * lineLength));
					}
					Invalidate();
					break;
				case "Shape":
					edits[edits.Count - 1]["EndLocation"] = e.Location;
					if ((ModifierKeys & Keys.Shift) == Keys.Shift) {
						edits[edits.Count - 1]["EndLocation"] = CreateSquarePoints(edits[edits.Count - 1]["StartLocation"], e.Location);
					}
					Invalidate();
					break;
				default:
					break;
			}

			//edits.Add(new {Type="Pen",Location=e.Location });
			lastClickPointTools = e.Location;
		}

		Rectangle getCropRectangle() {
			if (endingClickPoint.X == -1) { return new Rectangle(); }
			return correctRectangle(new Rectangle(startingClickPoint.X, startingClickPoint.Y, endingClickPoint.X - startingClickPoint.X, endingClickPoint.Y - startingClickPoint.Y));
		}

		Rectangle correctRectangle(Rectangle rectangle) {
			Rectangle newRectangle = new Rectangle(rectangle.X, rectangle.Y, Math.Abs(rectangle.Width), Math.Abs(rectangle.Height));
			if (rectangle.Width < 0) {
				newRectangle.X += rectangle.Width;
			}
			if (rectangle.Height < 0) {
				newRectangle.Y += rectangle.Height;
			}
			return newRectangle;
		}

		const int DRAG_SIZE = 8;
		Rectangle[] GetCornerRectangles(int extraSize = 0) {
			Rectangle crop = getCropRectangle();
			if (crop.Width < 22 || crop.Height < 22) {
				return new Rectangle[0];
			}
			const int smallSize = DRAG_SIZE - 2;
			int doubleSize = extraSize * 2;
			Rectangle rect1 = new Rectangle(crop.X - DRAG_SIZE / 2 - extraSize, crop.Y - DRAG_SIZE / 2 - extraSize, DRAG_SIZE + doubleSize, DRAG_SIZE + doubleSize); // top left
			Rectangle rect2 = new Rectangle(crop.Right - DRAG_SIZE / 2 - extraSize, crop.Y - DRAG_SIZE / 2 - extraSize, DRAG_SIZE + doubleSize, DRAG_SIZE + doubleSize); // top right
			Rectangle rect3 = new Rectangle(crop.X - DRAG_SIZE / 2 - extraSize, crop.Bottom - DRAG_SIZE / 2 - extraSize, DRAG_SIZE + doubleSize, DRAG_SIZE + doubleSize); // bottom left
			Rectangle rect4 = new Rectangle(crop.Right - DRAG_SIZE / 2 - extraSize, crop.Bottom - DRAG_SIZE / 2 - extraSize, DRAG_SIZE + doubleSize, DRAG_SIZE + doubleSize); // bottom right
			Rectangle rect5 = new Rectangle(crop.X - smallSize / 2 - extraSize + crop.Width / 2, crop.Y - smallSize / 2 - extraSize, smallSize + doubleSize, smallSize + doubleSize); // top center
			Rectangle rect6 = new Rectangle(crop.X - smallSize / 2 - extraSize, crop.Y - smallSize / 2 - extraSize + crop.Height / 2, smallSize + doubleSize, smallSize + doubleSize); // left center
			Rectangle rect7 = new Rectangle(crop.X - smallSize / 2 - extraSize + crop.Width / 2, crop.Bottom - smallSize / 2 - extraSize, smallSize + doubleSize, smallSize + doubleSize); // bottom center
			Rectangle rect8 = new Rectangle(crop.Right - smallSize / 2 - extraSize, crop.Y - smallSize / 2 - extraSize + crop.Height / 2, smallSize + doubleSize, smallSize + doubleSize); // right center

			return new Rectangle[8] { rect1, rect2, rect3, rect4, rect5, rect6, rect7, rect8 };
		}

		Size GetTextboxSize(string text, Font font, Graphics? g = null) {
			SizeF textSize = (g != null) ? g.MeasureString(text, font) : TextRenderer.MeasureText(text, font);
			return new Size(Math.Max(50, (int)textSize.Width), Math.Max(25, (int)textSize.Height));
		}

		GraphicsPath CreateRoundedRect(Rectangle rect, int radius) {
			GraphicsPath path = new GraphicsPath();

			if (radius <= 0) {
				path.StartFigure();
				path.AddRectangle(rect);
				path.CloseFigure();
				return path;
			}

			path.StartFigure();
			int diameter = radius * 2;
			diameter = Math.Min(diameter, Math.Min(rect.Width, rect.Height));
			path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
			path.AddArc(rect.X + rect.Width - diameter, rect.Y, diameter, diameter, 270, 90);
			path.AddArc(rect.X + rect.Width - diameter, rect.Y + rect.Height - diameter, diameter, diameter, 0, 90);
			path.AddArc(rect.X, rect.Y + rect.Height - diameter, diameter, diameter, 90, 90);
			path.CloseFigure();

			return path;
		}

		Font resolutionDisplayFont = new Font("Arial", 10, FontStyle.Bold);
		void imageDarken(object? sender, PaintEventArgs e) {
			e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;

			e.Graphics.DrawImage(image, ClientRectangle);
			//base.OnPaint(e);
			Rectangle regionRectangle = getCropRectangle();
			Region oldClip = e.Graphics.Clip.Clone();
			string resolutionString = regionRectangle.Width + "x" + regionRectangle.Height;
			SizeF resolutionTextSize = e.Graphics.MeasureString(resolutionString, resolutionDisplayFont);
			Point resolutionDrawPoint = new Point(regionRectangle.X, regionRectangle.Y - (int)resolutionTextSize.Height - 7);
			if (resolutionDrawPoint.Y < 0) {
				resolutionDrawPoint.Y = regionRectangle.Bottom + 7;
				if (resolutionDrawPoint.Y > Size.Height - (int)resolutionTextSize.Height - 7) {
					if (regionRectangle.X - resolutionTextSize.Width - 5 > 0) {
						resolutionDrawPoint.X = regionRectangle.X - (int)resolutionTextSize.Width - 5;
					} else {
						resolutionDrawPoint.X = regionRectangle.X + 6;
					}
					if (regionRectangle.X + regionRectangle.Width + ToolBar.Width + 5 > Width && ToolBar.Visible && regionRectangle.X - resolutionTextSize.Width - 5 > 0) {
						resolutionDrawPoint.Y = ToolBar.Location.Y + ToolBar.Height + 1;
					} else {
						resolutionDrawPoint.Y = 6;
					}
				}
			}
			if (resolutionDrawPoint.X+resolutionTextSize.Width > Size.Width) {
				resolutionDrawPoint.X = (int)(Size.Width - resolutionTextSize.Width - 1);
			}

			if (endingClickPoint.X != -1) {
				using (Brush brush = new SolidBrush(Color.FromArgb(150, Color.Black))) {
					e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
					e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
					e.Graphics.FillPath(brush, CreateRoundedRect(new Rectangle(resolutionDrawPoint, new Size((int)resolutionTextSize.Width, (int)resolutionTextSize.Height + 1)), 2));
					e.Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
					e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;

					e.Graphics.ExcludeClip(regionRectangle);
				}
			}
			using (Brush brush = new SolidBrush(Color.FromArgb(128, Color.Black))) {
				e.Graphics.FillRectangle(brush, ClientRectangle);
			}
			e.Graphics.Clip = oldClip;

			applyImageEdits(e.Graphics, false);

			if (endingClickPoint.X != -1) {
				Pen dashedPen = new Pen(Color.LightGray);
				dashedPen.DashStyle = DashStyle.Dash;
				dashedPen.Width = 1.75f;

				e.Graphics.DrawRectangle(dashedPen, new Rectangle(regionRectangle.X - 1, regionRectangle.Y - 1, regionRectangle.Width + 1, regionRectangle.Height + 1));

				if (InvisibleTextbox.Focused) {
					Dictionary<string, dynamic> lastIndex = edits[edits.Count - 1];
					Point textLocation = lastIndex["Location"];
					Size textBoxSize = GetTextboxSize(lastIndex["Text"], lastIndex["Font"], e.Graphics);
					Rectangle rect = new Rectangle(textLocation.X - 1, textLocation.Y - 1, textBoxSize.Width, textBoxSize.Height);
					dashedPen.Width = 1.2f;
					dashedPen.Color = Color.FromArgb(160, Color.LightGray);

					using (Pen secondaryPen = new Pen(Color.Black)) {
						secondaryPen.Width = 1.2f;
						secondaryPen.Color = Color.FromArgb(32, Color.DarkGray);
						e.Graphics.DrawRectangle(secondaryPen, rect);
					}

					e.Graphics.DrawRectangle(dashedPen, rect);
				}

				dashedPen.Dispose();

				e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
				using (Brush brush = new SolidBrush(Color.White)) {
					resolutionDrawPoint.X += 1;
					e.Graphics.DrawString(resolutionString, resolutionDisplayFont, brush, resolutionDrawPoint);
				}
				using (Pen pen = new Pen(Color.LightGray)) {
					Rectangle[] cornerRectangles = GetCornerRectangles(1);
					foreach (Rectangle rect in cornerRectangles) {
						e.Graphics.DrawRectangle(pen, rect);
					}
				}
				using (Brush brush = new SolidBrush(Color.FromArgb(128, 44, 44, 44))) {
					Rectangle[] cornerRectangles = GetCornerRectangles();
					foreach (Rectangle rect in cornerRectangles) {
						e.Graphics.FillRectangle(brush, rect);
					}
				}
			}

			if (currentTool == "Pen" && ToolBar.Visible) {
				using (SolidBrush brush = new SolidBrush(getSetting("Color"))) {
					Point cursorPos = Cursor.Position;
					int thickness = getSetting("Thickness");
					Rectangle rectangle = centerRectangle(new Rectangle(cursorPos, new Size(thickness, thickness)));
					e.Graphics.FillEllipse(brush, rectangle);
				}
			}

			int offsetX = regionRectangle.Width + 5;
			int offsetY = -2;
			if (regionRectangle.Y + ToolBar.Size.Height > Size.Height) {
				//offsetY = -ToolBar.Size.Height - 5;
				offsetY = regionRectangle.Height - ToolBar.Size.Height - 5;
				if (regionRectangle.Y + offsetY < 0) {
					offsetY = -regionRectangle.Y + 5;
				}
			}
			if (regionRectangle.Right + 34 > Size.Width) {
				offsetX = -34;
				if (regionRectangle.X - ToolBar.Width < 0) {
					//offsetY = regionRectangle.Height + 5;
					offsetX = -regionRectangle.X + Size.Width - ToolBar.Width - 5;
				}
			}
			ToolBar.Location = new Point(regionRectangle.X + offsetX, regionRectangle.Y + offsetY);
		}
		void onFormClosed(object? sender, FormClosedEventArgs e) {
			newImage.Dispose();
			resolutionDisplayFont.Dispose();
			if (settingsForm != null && !settingsForm.IsDisposed) {
				settingsForm.Dispose();
			}
		}

		void promptToSave() {
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			saveFileDialog.Filter = "PNG (*.png)|*.png|JPEG (*.jpg)|*.jpg|WebP (*.webp)|*.webp|GIF (*.gif)|*.gif|BMP (*.bmp)|*.bmp|TIFF (*.tiff)|*.tiff|All Files (*.*)|*.*";
			saveFileDialog.Title = "Save Screenshot";
			saveFileDialog.DefaultExt = "png";
			saveFileDialog.AddExtension = true;

			DialogResult result = saveFileDialog.ShowDialog();

			if (result == DialogResult.OK) {
				cropImage();
				newImage.Save(saveFileDialog.FileName);
				Close();
			}
		}

		void promptToSaveSender(object? sender, EventArgs e) {
			promptToSave();
		}

		void switchMode(string newMode) {
			if (currentTool == "Pen") {
				Invalidate();
			}
			if (currentTool == newMode) {
				currentTool = "None";
			} else {
				currentTool = newMode;
			}
			foreach (Button v in tools) {
				v.BackColor = (v.Name == (currentTool + "Tool")) ? Color.FromArgb(255, 224, 224, 255) : Color.FromArgb(255, 224, 224, 224);
			}
		}

		void copyToClipboardSender(object? sender, EventArgs e) {
			copyToClipboard();
		}

		void onTextChanged(object? sender, EventArgs e) {
			if (sender == null) { return; }
			edits[edits.Count - 1]["Text"] = ((System.Windows.Forms.TextBox)sender).Text;
			Invalidate();
		}

		void invalidateImage(object? sender, EventArgs e) {
			Invalidate();
		}

		void showSettings(object? sender, EventArgs e) {
			if (settingsForm == null || settingsForm.IsDisposed) {
				settingsForm = new SettingsForm();
				Rectangle crop = getCropRectangle();
				settingsForm.Location = new Point(crop.Right - settingsForm.Width, crop.Top);
				settingsForm.Show();
			}
		}

		void closeForm(object? sender, EventArgs e) {
			Close();
		}

		void disableTextbox(object? sender, EventArgs e) {
			InvisibleTextbox.Enabled = false;
		}

		string[] CursorIcons = new string[8] { "SizeNWSE", "SizeNESW", "SizeNESW", "SizeNWSE", "SizeNS", "SizeWE", "SizeNS", "SizeWE" };
		void GlobalMouseMove(object? sender = null, MouseEventArgs? e = null) {
			if (mouseDownTransparent && dragId == null) {
				Cursor.Current = Cursors.Default;
				return;
			}

			Rectangle[] cornerRects = GetCornerRectangles(1);
			for (int i = 0; i < cornerRects.Length; i++) {
				Rectangle rect = cornerRects[i];
				if (!rectangleContains(Cursor.Position, rect)) continue;
				switch (CursorIcons[i]) {
					case "SizeNWSE":
						Cursor.Current = Cursors.SizeNWSE;
						break;
					case "SizeNESW":
						Cursor.Current = Cursors.SizeNESW;
						break;
					case "SizeNS":
						Cursor.Current = Cursors.SizeNS;
						break;
					case "SizeWE":
						Cursor.Current = Cursors.SizeWE;
						break;
				}
				return;
			}

			if (currentTool == "None" && rectangleContains(Cursor.Position, getCropRectangle())) {
				Cursor.Current = Cursors.SizeAll;
				return;
			}
			Cursor.Current = Cursors.Default;
		}

		void setupQuickSettings() {
			QuickSettingsStrip.Opening += (_, _) => {
				// generate color preview
				Bitmap map = new Bitmap(16, 16);
				using Graphics g = Graphics.FromImage(map);
				using Pen pen = new Pen(Color.Black);
				g.Clear(getSetting("Color"));
				g.DrawRectangle(pen, new Rectangle(0, 0, 15, 15));
				QuickColorSetting.Image = map;
				QuickFillSetting.Checked = getSetting("FilledShape");
			};

			QuickColorSetting.Click += (_, _) => {
				if (settingsForm != null) {
					settingsForm.Dispose();
				};
				QuickSettingsStrip.Close();
				settingsForm = new SettingsForm();
				settingsForm.Location = new Point(-1000, -1000);
				settingsForm.Show();
				settingsForm.button1.PerformClick();
				settingsForm.Dispose();
				settingsForm = null;
			};

			QuickFillSetting.Click += (_, _) => {
				SettingsForm tempForm = new SettingsForm();
				tempForm.ShapeFillCheck.Checked = !tempForm.ShapeFillCheck.Checked;
				tempForm.Dispose();
			};

			QuickSettingsStrip.Items.Insert(1, new QuickSettingDropDown("Shape", "ShapeSelector", "Shape"));
			QuickSettingsStrip.Items.Add(new QuickSettingDropDown("Line shape", "LineShapeDropdown", "LineShape"));
		}
	}
}