using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelCreating
{
    public partial class LevelSize : Form
    {

        private int levelWidth = 10;
        private int levelHeight = 10;
        private bool saveLevelSize = false;
        public LevelSize()
        {
            InitializeComponent();

        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            saveLevelSize = true;
            int.TryParse(LevelWidth.Text, out levelWidth);
            int.TryParse(LevelHeight.Text, out levelHeight);
            Close();
        }
        

        public void Transfer(int width, int height)
        {
            levelWidth = width;
            levelHeight = height;
        }

        public void TransferBack(out int width, out int height, out bool saveLevelSize)
        {
            width = levelWidth;
            height = levelHeight;
            saveLevelSize = this.saveLevelSize;
        }

    }
}
