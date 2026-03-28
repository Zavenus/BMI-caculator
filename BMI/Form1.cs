using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            double weightKg;
            double heightCm;
            if (!double.TryParse(txtWeight.Text, out weightKg) || weightKg <= 0)
            {
                MessageBox.Show("請輸入有效的體重 (kg)", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtHeight.Text, out heightCm) || heightCm <= 0)
            {
                MessageBox.Show("請輸入有效的身高 (cm)", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double heightM = heightCm / 100.0;
            double bmi = weightKg / (heightM * heightM);

            string category;
            // 根據 BMI 決定分類與背景顏色
            if (bmi < 18.5)
            {
                category = "體重過輕";
                lblResult.BackColor = Color.LightSkyBlue;
                lblResult.ForeColor = Color.Black;
            }
            else if (bmi < 25)
            {
                category = "正常範圍";
                lblResult.BackColor = Color.LightGreen;
                lblResult.ForeColor = Color.Black;
            }
            else if (bmi < 30)
            {
                category = "過重";
                lblResult.BackColor = Color.Khaki;
                lblResult.ForeColor = Color.Black;
            }
            else
            {
                category = "肥胖";
                lblResult.BackColor = Color.IndianRed;
                lblResult.ForeColor = Color.White;
            }

            lblResult.Text = $"BMI: {bmi:F2} — {category}";
            lblResult.Visible = true;
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
