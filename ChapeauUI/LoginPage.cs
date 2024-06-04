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
            else
            {
                throw new Exception("Employee role is not defined! Please contact the manager!");
            }
        }




        private void OpenUI(Form newForm)
        {
            // define active form (LoginUI) and hide it
            Form activeForm = ActiveForm;
            activeForm.Hide();

            // show new form, which needs to be open
            newForm.ShowDialog();

            // close previous form (LoginUI), so it's not running in the background
            activeForm.Close();
        }

        private void CheckIfUCcorrect()
        {
            IfCorrectChecker hashCheckAndGetEmployee = new IfCorrectChecker();
            string UCode = userCodeTextBox.Text;

            if (UCode == null)
            {
                wrongUCLabel.Visible = true;
                wrongUCLabel.Text = "No User Code Written";
            }
            else if (hashCheckAndGetEmployee.IsCorrectPassword(UCode) == false)
            {
                wrongUCLabel.Visible = true;
                wrongUCLabel.Text = "Wrong Password";

            }
            else if (hashCheckAndGetEmployee.IsCorrectPassword(UCode))
            {
                OpenFormBasedOnTheRole(hashCheckAndGetEmployee.GetEmployeeByHashedUC(UCode));
            }
        }

        private void wrongUCLabel_Click(object sender, EventArgs e)
        {
            string UCode = userCodeTextBox.Text;

            if (UCode == "1111")
            {
                wrongUCLabel.Visible = true;
                wrongUCLabel.Text = "No User Code Written";
            }
            else
            {

            }
        }

        private void wrongUCLabel_Click_1(object sender, EventArgs e)
        {

        }
    }
}
