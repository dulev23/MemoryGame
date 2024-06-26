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
    public partial class MemoryGame : Form
    {
        public MemoryGame()
        {
            InitializeComponent();
        }

        private void btnEasy_Click(object sender, EventArgs e)
        {
            EasyMode easyMode = new EasyMode();
            easyMode.Show();
        }

        private void btnMedium_Click(object sender, EventArgs e)
        {
            MediumMode mediumMode = new MediumMode();
            mediumMode.Show();
        }

        private void btnHard_Click(object sender, EventArgs e)
        {
            HardMode hardMode = new HardMode();
            hardMode.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to exit the game?","Exit Game", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
