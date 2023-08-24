using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.CompilerServices;
using System.Reflection.Emit;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace WordGame
{
	public partial class Form1 : Form
	{
		Tuple<string, string> startWords;
		List<string> allWords = File.ReadAllLines("words.txt").ToList();
		List<string> foundWords = new List<string>();
		List<string> foundWordsCurrent = new List<string>();
		List<string> foundWordsUp = new List<string>();
		List<string> foundWordsDown = new List<string>();
		List<TextBox> found = new List<TextBox>();
		Point defaultScrollPosition;
		Size defaultScrollSize;
		int total;
		int wordsFound = 0;
		int scrolled = 0;
		public Form1()
		{
			InitializeComponent();
			this.KeyPreview = true;
			this.KeyDown += new KeyEventHandler(Form1_KeyDown);
			startWords = Tools.getStartWords("words-start.txt");
			Tools.setButtons(this, startWords.Item1, startWords.Item2);
			foreach (Button button in this.Controls.OfType<Button>())
			{
				if (!button.Name.Contains("Button"))
				{
					button.Click += buttonClick;
				}
			}
			TextInput.Enabled = false;
			TextInput.BackColor = Color.White;
			TextInput.ForeColor = Color.Black;
			EraseButton.MouseEnter += EraseButton_MouseEnter;
			EraseButton.MouseLeave += EraseButton_MouseLeave;
			ConfirmButton.MouseEnter += ConfirmButton_MouseEnter;
			ConfirmButton.MouseLeave += ConfirmButton_MouseLeave;
			ConfirmButton.Click += ConfirmButton_Click;
			ConfirmButton.Select();
			this.MouseWheel += ScrollWords;
			total = Tools.calculateTotal("words-start.txt", startWords.Item1, startWords.Item2);
			found = Tools.setFound(this);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			int outerScrollX = 430;
			int outerScrollY = 5;
			int outerScrollWidth = 40;
			int outerScrollHeight = 370;
			int innerScrollX = outerScrollX + 5;
			int innerScrollY = outerScrollY + 5;
			int innerScrollWidth = outerScrollWidth - 10;
			int innerScrollHeight = outerScrollHeight - 10;
			int innerScrollYEnd = innerScrollY + innerScrollHeight;
			int heightMeasurement;
			
			using (Graphics g = e.Graphics)
			{
				g.Clear(Color.White);
				using (Pen pen = new Pen(Color.Black, 1))
				{
					g.DrawRectangle(pen, outerScrollX, outerScrollY, outerScrollWidth, outerScrollHeight);
				}
				using (Brush brush = new SolidBrush(Color.DarkGray))
				{
					if (foundWords.Count > 10)
					{
						innerScrollHeight = (int)Math.Round((double)innerScrollHeight * (double)10 / (double)foundWords.Count, 0);
						innerScrollY = innerScrollYEnd - innerScrollHeight;
						if (scrolled < 0)
						{
							innerScrollY += outerScrollY + 5 - innerScrollY;
						}
					}
					g.FillRectangle(brush, innerScrollX, innerScrollY, innerScrollWidth, innerScrollHeight);
				}
				Console.WriteLine(scrolled);
			}
		}
		private void ConfirmWord()
		{
			scrolled = 0;
			string word = TextInput.Text.ToLower();
			string word1 = startWords.Item1;
			string word2 = startWords.Item2;
			if (word.Length < 4)
			{
				Messages.Text = "TOO SHORT";
				Messages.ForeColor = Tools.getColor("#E2C044");
				Tools.wipe(TextInput);
				return;
			}
			if (foundWords.Contains(word))
			{
				Messages.Text = "ALREADY FOUND";
				Messages.ForeColor = Tools.getColor("#E2C044");
				Tools.wipe(TextInput);
				return;
			}
			if (!allWords.Contains(word))
			{
				Messages.Text = "NO SUCH WORD";
				Messages.ForeColor = Tools.getColor("#E2C044");
				Tools.wipe(TextInput);
				return;
			}
			bool anyWord1 = (word1.Any(x => word.Any(y => y == x)));
			bool anyWord2 = (word2.Any(x => word.Any(y => y == x)));
			if (!anyWord1)
			{
				Messages.Text = "ADD A PINK ONE";
				Messages.ForeColor = Tools.getColor("#E2C044");
				Tools.wipe(TextInput);
				return;
			}
			if (!anyWord2)
			{
				Messages.Text = "ADD A BLUE ONE";
				Messages.ForeColor = Tools.getColor("#E2C044");
				Tools.wipe(TextInput);
				return;
			}
			foundWords.Add(word);
			if (wordsFound < 10)
			{
				foundWordsCurrent.Add(word);
			}
			else
			{
				if (scrolled == 0)
				{
					foundWordsUp.Add(foundWordsCurrent[0]);
					foundWordsCurrent.RemoveAt(0);
					foundWordsCurrent.Add(word);
				}
				else
				{
					foundWordsDown.Add(word);
				}
			}
			wordsFound++;
			for (int i = 0; i < 10; i++)
			{
				if (foundWordsCurrent.ElementAtOrDefault(i) != null)
				{
					found[i].Text = foundWordsCurrent[i].ToUpper();
					Tools.resizeText(found[i], TextRenderer.MeasureText(found[i].Text, found[i].Font));
				}
			}
			Tools.wipe(TextInput);
			Messages.Text = $"{word.ToUpper()}";
			Messages.ForeColor = Tools.getColor("#21A179");
			Score.Text = $"{int.Parse(Score.Text) + Tools.calculateValue(word)}";
			var thresholds = new [] { 0, 1, 5, 10, 20, 40, 80 };
			var titles = new [] { "Basic", "Novice", "Learner", "Scholar", "Adept", "Expert", "Genius"};
			var thresholdsAndTitles = thresholds.Zip(titles, (i, j) => new { Threshold = i, Word = j });
			string title = "";
			int nextThreshold = 0;
			foreach (var pair in thresholdsAndTitles)
			{
				if (int.Parse(Score.Text) >= (int)(total * pair.Threshold / 100))
				{
					title = pair.Word.ToUpper();
					if (pair.Threshold == 0)
					{
						nextThreshold++;
					}
					else if (pair.Threshold == 1)
					{
						nextThreshold = 5;
					}
					else if (pair.Threshold < 80)
					{
						nextThreshold = pair.Threshold * 2;
					}
				}
				else
				{
					break;
				}
			}
			nextThreshold = (int)(total * nextThreshold / 100);
			Title.Text = title;
			LeftUntilNext.Text = $"({nextThreshold - int.Parse(Score.Text)} to next)";
			this.Refresh();
		}
		private void ConfirmButton_Click(object sender, EventArgs e)
		{
			ConfirmWord();
		}
		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			string text = e.KeyCode.ToString();
			string letters = startWords.Item1 + startWords.Item2;
			if (letters.Contains(text.ToLower()) && text.Length == 1)
			{
				Tools.addLetter(TextInput, text);
			}
			else
			{
				switch (text)
				{
					case "Back":
						Tools.erase(TextInput);
						break;
					case "Enter":
						ConfirmWord();
						break;
				}
			}
		}
		private void buttonClick(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			string letter = button.Text;
			Tools.addLetter(TextInput, letter);
		}

		private void EraseButton_Click(object sender, EventArgs e)
		{
			Tools.erase(TextInput);
		}
		private void EraseButton_MouseEnter(object sender, EventArgs e)
		{
			EraseButton.FlatAppearance.BorderColor = Color.Black;
			EraseButton.FlatAppearance.BorderSize = 1;
		}
		private void EraseButton_MouseLeave(object sender, EventArgs e)
		{
			EraseButton.FlatAppearance.BorderColor = SystemColors.Control;
			EraseButton.FlatAppearance.BorderSize = 0;
		}
		private void ConfirmButton_MouseEnter(object sender, EventArgs e)
		{
			ConfirmButton.FlatAppearance.BorderColor = Color.Black;
			ConfirmButton.FlatAppearance.BorderSize = 1;
		}
		private void ConfirmButton_MouseLeave(object sender, EventArgs e)
		{
			ConfirmButton.FlatAppearance.BorderColor = SystemColors.Control;
			ConfirmButton.FlatAppearance.BorderSize = 0;
		}
		private void ScrollWords(object sender, MouseEventArgs e)
		{
			if (e.Delta < 0)
			{
				// up!
				if (foundWordsDown.Count == 0)
				{
					return;
				}
				else
				{
					Tools.scroll(foundWordsUp, foundWordsCurrent, foundWordsDown, found, false);
					for (int i = 0; i < 10; i++)
					{
						if (foundWordsCurrent.ElementAtOrDefault(i) != null)
						{
							found[i].Text = foundWordsCurrent[i].ToUpper();
							Tools.resizeText(found[i], TextRenderer.MeasureText(found[i].Text, found[i].Font));
						}
					}
					scrolled++;
				}
			}
			if (e.Delta > 0)
			{
				// down!
				if (foundWordsUp.Count == 0)
				{
					return;
				}
				else
				{
					Tools.scroll(foundWordsUp, foundWordsCurrent, foundWordsDown, found);
					for (int i = 0; i < 10; i++)
					{
						if (foundWordsCurrent.ElementAtOrDefault(i) != null)
						{
							found[i].Text = foundWordsCurrent[i].ToUpper();
							Tools.resizeText(found[i], TextRenderer.MeasureText(found[i].Text, found[i].Font));
						}
					}
					scrolled--;
				}
			}
			this.Refresh();
		}
		private void ConfirmButton_Click_1(object sender, EventArgs e)
		{

		}
	}
	public class Tools
	{
		static Random random = new Random();
		static string defaultText = "Enter...";
		static int maxLength = 16;
		static readonly int defaultTextSize = 30;
		public static System.Drawing.Color getColor (string code)
		{
			return System.Drawing.ColorTranslator.FromHtml (code);
		}
		public static string getRandomString (List <string> values)
		{
			int index = random.Next(0, values.Count);
			return values[index];
		}
		public static Tuple<string, string> getStartWords (string file)
		{
			List<string> startWords1 = File.ReadAllLines(file).ToList();
			string word1 = Tools.getRandomString(startWords1);
			List<string> startWords2 = new List<string>();
			int index = 0;
			foreach (string word in startWords1)
			{
				bool fits = true;
				foreach (char character in word)
				{
					if (word1.Contains(character))
					{
						fits = false;
					}
				}
				if (fits)
				{
					startWords2.Add(word);
					index++;
				}
			}
			string word2 = Tools.getRandomString(startWords2);
			return Tuple.Create(word1, word2);
		}
		public static void setButtons (Form form, string word1, string word2)
		{
			foreach (Button button in form.Controls.OfType<Button>())
			{
				button.ForeColor = Tools.getColor("#000000");
				button.FlatStyle = FlatStyle.Flat;
				button.FlatAppearance.BorderSize = 1;
				if (button.Name.Contains("Button"))
				{
					button.FlatAppearance.BorderSize = 0;
				}
				if (button.Name.Contains("Word0"))
				{
					button.Text = word1[button.Name[button.Name.Length - 1] - '0'].ToString().ToUpper();
					button.BackColor = Tools.getColor("#F49FBC");
				}
				else if (button.Name.Contains("Word1"))
				{
					button.Text = word2[button.Name[button.Name.Length - 1] - '0'].ToString().ToUpper();
					button.BackColor = Tools.getColor("#92BCEA");
				}
			}
		}
		public static void addLetter (TextBox textbox, string letter)
		{
			if (textbox.Text.Length == maxLength)
			{
				return;
			}
			if (textbox.Text == defaultText) 
			{
				textbox.Text = "";
				textbox.ForeColor = Tools.getColor("#AAAAAA");
			}
			textbox.Text += letter;
			textbox.ForeColor = Color.Black;
			resizeText(textbox, TextRenderer.MeasureText(textbox.Text, textbox.Font));

		}
		public static void erase (TextBox textbox)
		{
			if (textbox.Text == defaultText)
			{
				return;
			}
			textbox.Text = textbox.Text.Remove(textbox.Text.Length - 1);
			if (textbox.Text.Length == 0)
			{
				textbox.Text = defaultText;
				textbox.ForeColor = Tools.getColor("#AAAAAA");
			}
			resizeText(textbox, TextRenderer.MeasureText(textbox.Text, textbox.Font), false);
		}
		public static void wipe (TextBox textbox)
		{
			textbox.Text = defaultText;
			textbox.ForeColor = Tools.getColor("#AAAAAA");
			resizeText(textbox, TextRenderer.MeasureText(textbox.Text, textbox.Font), false);
		}
		public static void resizeText (TextBox textbox, Size standard_size, bool decrease=true)
		{
			SizeF textSize = default(SizeF);
			if (decrease)
			{
				do
				{
					using (Font font = new Font(textbox.Font.Name, textbox.Font.SizeInPoints))
					{
						textSize = TextRenderer.MeasureText(textbox.Text, font);
						if (textSize.Width > textbox.Width)
						{
							textbox.Font = new Font(font.Name, font.SizeInPoints - 1f);
						}
					}
				} while (textSize.Width > textbox.Width);
			}
			else
			{
				do
				{
					using (Font font = new Font(textbox.Font.Name, textbox.Font.SizeInPoints))
					{
						textSize = TextRenderer.MeasureText(textbox.Text, font);
						if (textSize.Width < textbox.Width)
						{
							Font newFont = new Font(font.Name, font.SizeInPoints + 1f);
							if (newFont.Size <= defaultTextSize)
							{
								textbox.Font = newFont;
							}
							else
							{
								return;
							}
						}
					}
				} while (textSize.Width < textbox.Width);
			}
		}
		public static int calculateValue(string word)
		{
			int value = word.Length;
			if (4 < word.Length && word.Length < 7)
			{
				value++;
			}
			if (7 < word.Length && word.Length < 10)
			{
				value++;
			}
			if (10 < word.Length)
			{
				value++;
			}
			if (word.Distinct().Count() == 8)
			{
				value *= 2;
			}
			return value;

		}
		public static int calculateTotal(string file, string word1, string word2)
		{
			int total = 0;
			List<string> words = File.ReadAllLines(file).ToList();
			foreach (string word in words)
			{
				if (word1.Any(x => word.Any(y => y == x)) 
					&& word2.Any(x => word.Any(y => y == x))
					&& word != word1
					&& word != word2)
				{
					total += calculateValue(word);
				}
			}
			return total;
		}
		public static List<TextBox> setFound(Form form)
		{
			List <TextBox> found = new List<TextBox>();
			Size textBoxSize = new Size(184, 33);
			Point startingLocation = new Point(242, 3);
			for (int i=0; i <= 10; i++)
			{
				TextBox newTextBox = new TextBox();
				newTextBox.Location = startingLocation;
				newTextBox.Size = textBoxSize;
				newTextBox.Enabled = false;
				// The buttons appear invisible because their color matches the background's
				newTextBox.BackColor = Color.White;
				newTextBox.Font = new System.Drawing.Font(newTextBox.Font.Name, 20);
				newTextBox.BorderStyle = BorderStyle.None;
				newTextBox.TextAlign = HorizontalAlignment.Center;
				startingLocation.Y += 39;
				found.Add(newTextBox);
				form.Controls.Add(newTextBox);
			}
			return found;
		}
		public static void scroll(List <string> up, List <string> current, List <string> down, 
			                      List<TextBox> found, bool scrollDown = true)
		{
			if (scrollDown)
			{
				down.Insert(0, current[current.Count - 1]);
				current.RemoveAt(current.Count - 1);
				current.Insert(0, up[up.Count - 1]);
				up.RemoveAt(up.Count - 1);
				for (int i = 0; i < 10; i++)
				{
					if (current.ElementAtOrDefault(i) != null)
					{
						found[i].Text = current[i].ToUpper();
						Tools.resizeText(found[i], TextRenderer.MeasureText(found[i].Text, found[i].Font));
					}
				}
			}
			else
			{
				up.Add(current[0]);
				current.RemoveAt(0);
				current.Add(down[0]);
				down.RemoveAt(0);
				for (int i = 0; i < 10; i++)
				{
					if (current.ElementAtOrDefault(i) != null)
					{
						found[i].Text = current[i].ToUpper();
						Tools.resizeText(found[i], TextRenderer.MeasureText(found[i].Text, found[i].Font));
					}
				}
			}
		}
	}
}
