using ChapeauModel;

namespace ChapeauUI
{
    public partial class ChapeauUI : Form
    {
        private Employee currentEmployee; // Add this field 

        public ChapeauUI(Employee employee)
        {
            InitializeComponent();
            currentEmployee = employee; // Set the current employee


            // Hide all role-specific buttons initially
            buttonWaiter.Visible = false;
            buttonChef.Visible = false;
            buttonManager.Visible = false;

            // Show the button based on the employee's role
            if (employee.Role == EmployeeRole.Waiter)
            {
                buttonWaiter.Visible = true;
            }
            else if (employee.Role == EmployeeRole.Chef || employee.Role == EmployeeRole.Bartender)
            {
                buttonChef.Visible = true;
            }
            else if (employee.Role == EmployeeRole.Manager)
            {
                buttonManager.Visible = true;
            }
        }

        private void OpenUI(Form newForm)
        {
            // Define active form (LoginUI) and hide it
            Form activeForm = ActiveForm;
            activeForm.Hide();

            // Show new form, which needs to be open
            newForm.ShowDialog();

            // Close previous form (LoginPage), so it's not running in the background
            activeForm.Close();
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            LoginPage newForm = new LoginPage();
            OpenUI(newForm);
        }

        private void roundedButton5_Click_1(object sender, EventArgs e)
        {
            Orders newForm = new Orders(currentEmployee);
            OpenUI(newForm);
        }

        private void roundedButton6_Click_1(object sender, EventArgs e)
        {
            Tables newForm = new Tables(currentEmployee);
            OpenUI(newForm);
        }

        private void roundedButton4_Click(object sender, EventArgs e)
        {
            Tables newForm = new Tables(currentEmployee);
            OpenUI(newForm);
        }

        private void roundedButton3_Click(object sender, EventArgs e)
        {
            Orders newForm = new Orders(currentEmployee);
            OpenUI(newForm);
        }
    }
}
