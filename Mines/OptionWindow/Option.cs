using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mines.OptionWindow
{
    public partial class Option : Form
    {
        public Option(int row, int col, int nbBomb)
        {
            InitializeComponent();
            this.row.Value = row;
            this.col.Value = col;
            this.bomb.Value = nbBomb;
        }

        public int getRow()
        {
            return (int)this.row.Value;
        }

        public int getCol()
        {
            return (int)this.col.Value;
        }

        public int getBomb()
        {
            return (int)this.bomb.Value;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
