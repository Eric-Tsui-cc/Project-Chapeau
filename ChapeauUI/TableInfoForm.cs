using ChapeauModel;
using ChapeauService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChapeauUI
{
    public partial class TableInfoForm : Form 
    {
        private OverviewService overviewService;
        private Table table;
        private ChapeauUI chapeauUI;
        public TableInfoForm(Table table)
        {
            InitializeComponent();
            overviewService = new OverviewService();
            chapeauUI = new ChapeauUI();
            this.table = table;
        }
        public TableInfoForm()
        {
            InitializeComponent();
            overviewService = new OverviewService();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message;
            string title;

            if (table.Status == StatusOfTable.Occupied)
            {
                message = "Are you sure you want to change the table status to Free?";
                title = "Confirm Status Change";
            }
            else
            {
                message = "Are you sure you want to change the table status to Occupied?";
                title = "Confirm Status Change";
            }

            
            DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            
            if (result == DialogResult.Yes)
            {
                if (table.Status == StatusOfTable.Occupied)
                {
                    overviewService.ChangeTableStatusToFree(table.TableId);
                    MessageBox.Show("Status Changed！", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else
                {
                    overviewService.ChangeTableStatusToOccupied(table.TableId);
                    MessageBox.Show("Status Changed！", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
