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
    public partial class SaveForm : Form
    {
        string saveseperator = "";
        string savelineSeperator = "\\n";
        bool save = false;
        public SaveForm()
        {
            InitializeComponent();
        }

        private void SaveForm_Load(object sender, EventArgs e)
        {
            seperator.Text = saveseperator;
            lineSeperator.Text = savelineSeperator;
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            save = true;
            SetSeperator(seperator.Text);
            SetLineSeperator(lineSeperator.Text);
            this.Close();
        }

        public void SetSeperator(string seperator)
        {
            this.saveseperator = seperator;
        }

        public void SetLineSeperator(string lineSeperator)
        {
            this.savelineSeperator = lineSeperator;
        }

        public string GetSeperator()
        {
            return saveseperator;
        }

        public string GetLineSeperator()
        {
            return savelineSeperator;
        }
        public bool GetSaveState() 
        {
            return save;
        }
    }
}
