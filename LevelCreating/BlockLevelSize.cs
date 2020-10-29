using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelCreating
{
    public partial class BlockLevelSize : Form
    {
        int blockWidth = 10;
        int blockHeight = 10;
        public BlockLevelSize()
        {
            InitializeComponent();
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            int.TryParse(BlockX.Text, out blockWidth);
            int.TryParse(BlockY.Text, out blockHeight);
            Close();
        }

        public void Transfer(int width, int height)
        {
            blockWidth = width;
            blockHeight = height;
        }

        public void TransferBack(out int width, out int height)
        {
            width = blockWidth;
            height = blockHeight;
        }
    }
}
