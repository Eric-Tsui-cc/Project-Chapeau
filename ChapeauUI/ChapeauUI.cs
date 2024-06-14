using ChapeauModel;

namespace ChapeauUI
{
    public partial class ChapeauUI : Form
    {
        public ChapeauUI(Employee employee)
        {
            InitializeComponent();
        }
        public ChapeauUI()
        {
            InitializeComponent();
        }

        private void OpenUI(Form newForm)
        {
            // define active form (LoginUI) and hide it
            Form activeForm = ActiveForm;
            activeForm.Hide();

            // show new form, which needs to be open
            newForm.ShowDialog();

            // close previous form (LoginPage), so it's not running in the background
            activeForm.Close();
        }


        private void roundedButton4_Click(object sender, EventArgs e)
        {
            Orders newForm = new Orders();

            OpenUI(newForm);
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            LoginPage newForm = new LoginPage();
            OpenUI(newForm);


        }

        private void roundedButton3_Click(object sender, EventArgs e)
        {
            Tables newForm = new Tables();
            OpenUI(newForm);

        }


    }
}