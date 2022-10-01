using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phisic
{
    public partial class InstructionForm : Form
    {
        public InstructionForm()
        {
            InitializeComponent();
            Properties.Settings.Default.isInstructionFromAddaed = true;


        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void InstructionForm_Deactivate(object sender, EventArgs e)
        {

        }

        private void InstructionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.isInstructionFromAddaed = false;
        }
    }
}
