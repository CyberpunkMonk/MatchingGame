using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace MatchingGame {
	public partial class Form1 : Form {
		int time = 0;
		int matchesToWin;
		int currentMatches;
		Random random = new Random();
		List<string> icons = new List<string>{"!","!","N","N",",",",","k","k","b","b","v","v","w","w","z","z"};
		Label firstClicked = null;
		Label secondClicked = null;
		SoundPlayer _soundplayer;
		public Form1() {
			InitializeComponent();
			assignIconsToSquares();
			_soundplayer = new SoundPlayer("chime.wav");
			matchesToWin = 8;
			currentMatches = 0;
		}
		private void assignIconsToSquares() {
			//Icon is pulled at random and assigned to a quare until each is assigned.
			foreach (Control control in tableLayoutPanel1.Controls) {
				Label iconlabel = control as Label;
				if (iconlabel != null) {
					int randomNum = random.Next(icons.Count);
					iconlabel.Text = icons[randomNum];
					iconlabel.ForeColor = iconlabel.BackColor;
					icons.RemoveAt(randomNum);
				}
			}
		}
		/// <summary>
		/// Every label's Click event is handled by this event handler
		/// </summary>
		/// <param name="sender">The label that was clicked</param>
		/// <param name="e"></param>
		private void label_Click(object sender, EventArgs e) {
			if (totalTimeTimer.Enabled == false) totalTimeTimer.Enabled = true;
			if (timer1.Enabled == true) return;
			Label clickedLabel = sender as Label;
			if (clickedLabel != null) {
				if (clickedLabel.ForeColor == Color.Black) return;
				if (firstClicked == null) {
					firstClicked = clickedLabel;
					firstClicked.ForeColor = Color.Black;
					return;
				}
				secondClicked = clickedLabel;
				secondClicked.ForeColor = Color.Black;
				if (firstClicked.Text == secondClicked.Text) {
					firstClicked = null;
					secondClicked = null;
					_soundplayer.Play();
					currentMatches++;
					checkForWinner();
					return;
				}
				timer1.Start();
			}
		}

		private void timer1_Tick(object sender, EventArgs e) {
			timer1.Stop();
			firstClicked.ForeColor = firstClicked.BackColor;
			secondClicked.ForeColor = secondClicked.BackColor;
			firstClicked = null;
			secondClicked = null;
		}

		private void checkForWinner() {
			Form1.ActiveForm.Text = currentMatches.ToString();
			if (matchesToWin == currentMatches) {
				totalTimeTimer.Enabled = false;
				MessageBox.Show("You matched all the icons!\nTime taken: "+time.ToString()+" seconds.","Congratulations!");
				Close();
			}
		}

		private void totalTimeTimer_Tick(object sender, EventArgs e) {
			time++;
		}
	}
}
