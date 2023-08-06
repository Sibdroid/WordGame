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

namespace WordGame
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			var startWords = Tools.getStartWords("words-start.txt");
			Tools.setButtons(this, startWords.Item1, startWords.Item2);
			foreach (Button button in this.Controls.OfType<Button>())
			{
				button.Click += buttonClick;
			}
		}
		private void buttonClick(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			string letter = button.Text;
			Tools.addLetter(TextInput, letter);
		}
	}
	public class Tools
	{
		static Random random = new Random();
		public static System.Drawing.Color getColor (string code)
		{
			return System.Drawing.ColorTranslator.FromHtml (code);
		}
		public static string getRandomString (List <string> values)
		{
			int index = random.Next(0, values.Count);
			return values[index];
		}
		public static (string, string) getStartWords (string file)
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
			return (word1, word2);
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
			textbox.Text += letter;
			float height = textbox.Height * 0.99f;
			float width = textbox.Width * 0.99f;
			//textbox.SuspendLayout();
			//Font font = textbox.Font;
			//Size size = TextRenderer.MeasureText(textbox.Text, font);
			//float heightRatio = height / size.Height;
			//float widthRatio = width / size.Width;
			//font = new Font(font.FontFamily, font.Size * Math.Min(widthRatio, heightRatio), font.Style);
			//textbox.Font = font;
			//textbox.ResumeLayout();
		}
	}
}
