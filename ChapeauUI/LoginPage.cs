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


        // opens UI based on the role of employee which is logging in

        private void OpenFormBasedOnTheRole(Employee employee)
        {
            if (employee.Role == EmployeeRole.Waiter)
            {
                // passing employee who logged in data to the next form.
                ChapeauUI newForm = new ChapeauUI(employee);
                OpenUI(newForm);
            }
            else if (employee.Role == EmployeeRole.Chef || employee.Role == EmployeeRole.Bartender)
            {

            }
            else if (employee.Role == EmployeeRole.Chef)
            {

            }

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
                wrongUCLabel.Text = "Wrong Password";

            }
            else if (hashCheckAndGetEmployee.IsCorrectUserCode(UCode))
            {
                OpenFormBasedOnTheRole(hashCheckAndGetEmployee.GetEmployeeByHashedUC(UCode));
            }
        }

    }
}
