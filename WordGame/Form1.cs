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

namespace WordGame
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			List<string> startWords = File.ReadAllLines("words-start.txt").ToList();
			string first = Tools.getRandomString(startWords);
			List<string> startWordsSecond = new List<string>();
			int index = 0;
			foreach (string word in startWords)
			{
				bool fits = true;
				foreach (char character in  word)
				{
					if (first.Contains(character))
					{
						fits = false;
					}
				}
				if (fits)
				{
					startWordsSecond.Add(word);
					index++;
				}
			}
			string second = Tools.getRandomString(startWordsSecond);
			foreach (Button button in this.Controls.OfType<Button>())
			{
				button.BackColor = Tools.getColor("#DFDFDF");
				button.ForeColor = Tools.getColor("#000000");
				button.FlatStyle = FlatStyle.Flat;
				button.FlatAppearance.BorderColor = Tools.getColor("#DFDFDF");
				button.FlatAppearance.BorderSize = 1;
				if (button.Name.Contains("Word0"))
				{
					button.Text = first[button.Name[button.Name.Length - 1] - '0'].ToString().ToUpper();
				}
				else if (button.Name.Contains("Word1"))
				{
					button.Text = second[button.Name[button.Name.Length - 1] - '0'].ToString().ToUpper();
				}
			}
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
	}
}
