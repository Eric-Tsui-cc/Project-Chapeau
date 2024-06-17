using ChapeauModel;
using ChapeauService;
using System;
using System.Windows.Forms;

namespace ChapeauUI
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                CheckIfUCcorrect();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void OpenFormBasedOnTheRole(Employee employee)
        {
            // I'm passing the employee who logged in data to the home form.
            ChapeauUI newForm = new ChapeauUI(employee);
            OpenUI(newForm);
        }

        private void OpenUI(Form newForm)
        {
            // Define active form and hide it
            Form activeForm = ActiveForm;
            activeForm.Hide();

            // Show new form, which needs to be open
            newForm.ShowDialog();

            // Close previous form , so it's not running in the background
            activeForm.Close();
        }

        private void CheckIfUCcorrect()
        {
            IfCorrectChecker_Bogdan hashCheckAndGetEmployee = new IfCorrectChecker_Bogdan();
            string UCode = userCodeTextBox.Text;

            if (UCode == "")
            {
                wrongUCLabel.Visible = true;
                wrongUCLabel.Text = "No User Code Entered";
            }
            else if (hashCheckAndGetEmployee.IsCorrectUserCode(UCode) == false)
            {
                wrongUCLabel.Visible = true;
                wrongUCLabel.Text = "Wrong User Code";
            }
            else if (hashCheckAndGetEmployee.IsCorrectUserCode(UCode))
            {
                Employee employee = hashCheckAndGetEmployee.GetEmployeeByHashedUC(UCode);
                OpenFormBasedOnTheRole(employee);
            }
        }
    }
}
