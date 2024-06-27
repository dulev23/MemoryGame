using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class EasyMode : Form
    {
        Random random = new Random();
        public Label firstClicked, secondClicked;
        public int attemptsCount;


        List<string> cards = new List<string>()
        {
            "L","L","N","N","S",
            "S",",",",","j","j"
        };

        public EasyMode()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            AssignCards();
            attemptsCount = 0;
        }

        private void AssignCards()
        {
            foreach (Control control in tlpImages.Controls)
            {
                if (control is Label label)
                {
                    int randomNum = random.Next(cards.Count);
                    label.Text = cards[randomNum];
                    cards.RemoveAt(randomNum);
                }
            }
        }

        private void lblImages_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            if (firstClicked != null && secondClicked != null)
                return;

            if (clickedLabel == null)
                return;

            if (clickedLabel.ForeColor == Color.Black)
                return;

            if(firstClicked == null)
            {
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                return;
            } else if (secondClicked == null)
            {
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;
                UpdateAttempts();
                WinCondition();
            }
            ++attemptsCount;
            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
            }
            else
            {
                timer1.Start();
            }
        }

        private void WinCondition()
        {
            Label label;
            for(int i = 0; i < tlpImages.Controls.Count; i++)
            {
                label = tlpImages.Controls[i] as Label;
                if (label != null && label.ForeColor == label.BackColor)
                    return;
            }

            attemptsCount += 1;
            string message = "Congratulations! You matched all the cards.\n"
                + "It took you " + attemptsCount + " Attempts";

            string title = "You Won!";

            if (MessageBox.Show(message, title, MessageBoxButtons.OK) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;
        }

        private void UpdateAttempts()
        {
            lblAttempts.Text = attemptsCount.ToString();
        }

        private void lblAttempts_Paint(object sender, PaintEventArgs e)
        {
            lblAttempts.Text = string.Format("Attempts: {0}", attemptsCount.ToString());
        }
    }
}
