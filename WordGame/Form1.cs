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

namespace WordGame
{
	public partial class Form1 : Form
	{
		Tuple<string, string> startWords;
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
			
		}
		private void buttonClick(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			string letter = button.Text;
			Tools.addLetter(TextInput, letter);
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
				}
			}
		}

		private void EraseButton_Click(object sender, EventArgs e)
		{
			Tools.erase(TextInput);
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
	}
}
