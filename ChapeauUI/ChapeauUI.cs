using ChapeauDAL;
using ChapeauModel;
using ChapeauService;
namespace ChapeauUI

{
    public partial class ChapeauUI : Form
    {

        public ChapeauUI()
        {   

            InitializeComponent();
            Bill bill = GetBillbyId(9);
            label1.Text = bill.Amount.ToString();
            label2.Text = bill.Tip.ToString();

        }
        private Bill GetBillbyId(int id)
        {
            BillDao billDao = new BillDao();
            Bill bill = billDao.GetBillById(id);
            return bill;
        }
    }
}