using ChapeauDAL;
using ChapeauModel;
using ChapeauService;
namespace ChapeauUI

{
    public partial class ChapeauUI : Form
    {
        private OverviewService overviewService;
        private Dictionary<Button, int> buttonTableIdMap;
        protected Table table;
        public ChapeauUI()
        {

            InitializeComponent();
            overviewService = new OverviewService();
            table = new Table();
            buttonTableIdMap = new Dictionary<Button, int>
            {
                {Table1, 1},
                {Table2, 2},
                {Table3, 3},
                {Table4, 4},
                {Table5, 5},
                {Table6, 6},
                {Table7, 7},
                {Table8, 8},
                {Table9, 9},
                {Table10, 10},

            };
            UpdateButtonStatus();



        }
        public void UpdateButtonStatus()
        {
            foreach (var pair in buttonTableIdMap)
            {
                Button button = pair.Key;
                int tableId = pair.Value;
                StatusOfTable status = overviewService.GetStatusByTableId(tableId);
                button.BackColor = GetButtonColor(status);
            }
        }

        private Color GetButtonColor(StatusOfTable status)
        {
            switch (status)
            {
                case StatusOfTable.Free:
                    return Color.Green;
                case StatusOfTable.Occupied:
                    return Color.Red;

                default:
                    return Color.Gray;
            }
        }

        private void Table1_Click(object sender, EventArgs e)
        {

            Button button = sender as Button;
            if (button != null)
            {

                int tableId = buttonTableIdMap[button];
                table = overviewService.GetTableById(tableId);


                TableInfoForm newWindow = new TableInfoForm(table);
                newWindow.ShowDialog();
            }
        }

        private void Table2_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {

                int tableId = buttonTableIdMap[button];
                table = overviewService.GetTableById(tableId);


                TableInfoForm newWindow = new TableInfoForm(table);
                newWindow.ShowDialog();
            }
        }

        private void Table3_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {

                int tableId = buttonTableIdMap[button];
                table = overviewService.GetTableById(tableId);


                TableInfoForm newWindow = new TableInfoForm(table);
                newWindow.ShowDialog();
            }
        }

        private void Table4_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {

                int tableId = buttonTableIdMap[button];
                table = overviewService.GetTableById(tableId);


                TableInfoForm newWindow = new TableInfoForm(table);
                newWindow.ShowDialog();
            }
        }

        private void Table5_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {

                int tableId = buttonTableIdMap[button];
                table = overviewService.GetTableById(tableId);


                TableInfoForm newWindow = new TableInfoForm(table);
                newWindow.ShowDialog();
            }
        }

        private void Table6_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {

                int tableId = buttonTableIdMap[button];
                table = overviewService.GetTableById(tableId);


                TableInfoForm newWindow = new TableInfoForm(table);
                newWindow.ShowDialog();
            }
        }

        private void Table7_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {

                int tableId = buttonTableIdMap[button];
                table = overviewService.GetTableById(tableId);


                TableInfoForm newWindow = new TableInfoForm(table);
                newWindow.ShowDialog();
            }
        }

        private void Table8_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {

                int tableId = buttonTableIdMap[button];
                table = overviewService.GetTableById(tableId);


                TableInfoForm newWindow = new TableInfoForm(table);
                newWindow.ShowDialog();
            }
        }

        private void Table9_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {

                int tableId = buttonTableIdMap[button];
                table = overviewService.GetTableById(tableId);


                TableInfoForm newWindow = new TableInfoForm(table);
                newWindow.ShowDialog();
            }
        }

        private void Table10_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {

                int tableId = buttonTableIdMap[button];
                table = overviewService.GetTableById(tableId);


                TableInfoForm newWindow = new TableInfoForm(table);
                newWindow.ShowDialog();
            }
        }

        private void buttonFresh_Click(object sender, EventArgs e)
        {
            UpdateButtonStatus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MakeOrderPage orderPage = new MakeOrderPage();
            orderPage.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BillForm billForm = new BillForm();
            billForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KitchenViewPage kitchenViewPage = new KitchenViewPage();
            kitchenViewPage.ShowDialog();
        }
    }
}