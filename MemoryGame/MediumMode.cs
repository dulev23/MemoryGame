using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class MediumMode : Form
    {
        Random random = new Random();
        public Label firstClicked, secondClicked;
        public int attemptsCount;
        public int timeUntilSecondTurn;

        public List<string> cards = new List<string>()
        {
            "!","!","%","%","Y","Y","o","o",
            "$","$","+","+","v","v","b","b"
        };

        public MediumMode()
        {
            InitializeComponent();
            attemptsCount = 0;
            AssignCards();
            timeUntilSecondTurn = 5;
        }

        private void AssignCards()
        {
            foreach (Control control in tlpCards.Controls)
            {
                if (control is Label label)
                {
                    int randomNum = random.Next(cards.Count);
                    label.Text = cards[randomNum];
                    cards.RemoveAt(randomNum);
                }
            }
        }

        private void lblAttemptsCount_Paint(object sender, PaintEventArgs e)
        {
            lblAttemptsCount.Text = string.Format("Attempts: {0}", attemptsCount.ToString());
        }

        private void UpdateAttempts()
        {
            lblAttemptsCount.Text = attemptsCount.ToString();
        }

        private void UpdateTimeLeft()
        {
            lblTimeLimit.Text = timeUntilSecondTurn.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;
        }

        private void lblCards_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;
            if (firstClicked != null && secondClicked != null)
                return;

            if (clickedLabel == null)
                return;

            if (clickedLabel.ForeColor == Color.Black)
                return;

            if (firstClicked == null)
            {
                timeUntilSecondTurn = 5;
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                timeLimit.Start();
                UpdateTimeLeft();
                return;
            }
            
            if (secondClicked == null)
            {
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;
                attemptsCount++;
                UpdateAttempts();
                timeUntilSecondTurn = 5;
                UpdateTimeLeft();
                timeLimit.Stop();
                WinCondition();
            }
            
            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
                timeLimit.Stop();
                timeUntilSecondTurn = 5;
                UpdateTimeLeft();
            }
            else
            {
                timer1.Start();
            }
        }

        private void WinCondition()
        {
            Label label;
            for (int i = 0; i < tlpCards.Controls.Count; i++)
            {
                label = tlpCards.Controls[i] as Label;
                if (label != null && label.ForeColor == label.BackColor)
                    return;
            }

            string message = "Congratulations! You matched all the cards.\n"
                + "It took you " + attemptsCount + " Attempts";

            string title = "You Won!";

            if (MessageBox.Show(message, title, MessageBoxButtons.OK) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void timeLimit_Tick(object sender, EventArgs e)
        {
            if(timeUntilSecondTurn > 0 && firstClicked!=null)
            {
                timeUntilSecondTurn--;
                UpdateTimeLeft();
            }
            
            if(timeUntilSecondTurn <= 0)
            {
                timeUntilSecondTurn = 5;
                UpdateTimeLeft();
                timeLimit.Stop();
                if (firstClicked != null)
                {
                    firstClicked.ForeColor = firstClicked.BackColor;
                    firstClicked = null;
                }

                if(secondClicked != null)
                {
                    secondClicked.ForeColor = secondClicked.BackColor;
                    secondClicked = null;
                }
                attemptsCount++;
                UpdateAttempts();
            }
        }

        private void lblTimeLimit_Paint(object sender, PaintEventArgs e)
        {
            lblTimeLimit.Text = string.Format("Time left: {0}",timeUntilSecondTurn);
        }
    }
}
