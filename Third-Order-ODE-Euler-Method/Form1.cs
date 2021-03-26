using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Third_Order_ODE_Euler_Method
{
    public partial class equationForm : Form
    {


        [DllImport("C:\\Users\\KHUSRAV\\source\\repos\\Third-Order-ODE-Euler-Method\\Debug\\numeric-method.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void fibonacci_init(Int64 a, Int64 b);

        [DllImport("C:\\Users\\KHUSRAV\\source\\repos\\Third-Order-ODE-Euler-Method\\Debug\\numeric-method.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 fibonacci_current();

        [DllImport("C:\\Users\\KHUSRAV\\source\\repos\\Third-Order-ODE-Euler-Method\\Debug\\numeric-method.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool fibonacci_next();

        [DllImport("C:\\Users\\KHUSRAV\\source\\repos\\Third-Order-ODE-Euler-Method\\Debug\\numeric-method.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void difference_equation_init(double yyyy, double yyy, double yy, double y);

        [DllImport("C:\\Users\\KHUSRAV\\source\\repos\\Third-Order-ODE-Euler-Method\\Debug\\numeric-method.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void default_value_init(double yyy0, double yy0, double y0);

        [DllImport("C:\\Users\\KHUSRAV\\source\\repos\\Third-Order-ODE-Euler-Method\\Debug\\numeric-method.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void step_init(double h, int length);

        [DllImport("C:\\Users\\KHUSRAV\\source\\repos\\Third-Order-ODE-Euler-Method\\Debug\\numeric-method.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool difference_equation_solve();

        [DllImport("C:\\Users\\KHUSRAV\\source\\repos\\Third-Order-ODE-Euler-Method\\Debug\\numeric-method.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double difference_equation_cell(int row, int column);

        double yyyy, yyy, yy, y, h, yyy0, yy0, y0;
        int n;

        public equationForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void solveEquation_Click(object sender, EventArgs e)
        {
            bool t = getNumbers();
            if (!t)
            {
                differenceEquation.Text = "Рақамҳоро нодуруст ворид кардед!";
                differenceEquation.ForeColor = Color.Red;
                return;
            }

            if (yyyy == 0.0 || yyy == 0.0 || yy == 0.0 || y == 0.0)
            {
                differenceEquation.Text = "Коэффисентҳо бояд ба 0 баробар нагаванд";
                differenceEquation.ForeColor = Color.Red;
                return;
            }
            generateEquation();
            difference_equation_init(yyyy: yyyy, yyy: yyy, yy: yy, y: y);
            default_value_init(yyy0: yyy0, yy0: yy0, y0: y0);
            step_init(h: h, length: n);

            t = difference_equation_solve();
            if (!t)
            {
                differenceEquation.Text = "Error";
                differenceEquation.ForeColor = Color.Red;
                return;
            }

            generateGrid();
            /*Console.WriteLine("Solve Prolem");
            // fibonacci_init(1, 5);
            if (fibonacci_next())
            {
                Console.WriteLine(fibonacci_current());
            }*/
        }

        private void generateGrid()
        {

            numberMethodEquation.ColumnHeadersDefaultCellStyle.Font =
                       new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            numberMethodEquation.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;
            numberMethodEquation.DefaultCellStyle.Font = new Font("Arial", 10);

            numberMethodEquation.ColumnCount = 5;
            numberMethodEquation.Columns[0].Name = "n";
            numberMethodEquation.Columns[0].Width = 50;
            numberMethodEquation.Columns[1].Name = "Xn";
            numberMethodEquation.Columns[2].Name = "Y1n";
            numberMethodEquation.Columns[3].Name = "Y2n";
            numberMethodEquation.Columns[4].Name = "Y3n";
            numberMethodEquation.Rows.Clear();
            for (int i = 0; i <= n; i ++)
            {
                string[] row =
                {
                    difference_equation_cell(i, 0).ToString("0"),
                    difference_equation_cell(i, 1).ToString("0.####"),
                    difference_equation_cell(i, 2).ToString("0.####"),
                    difference_equation_cell(i, 3).ToString("0.####"),
                    difference_equation_cell(i, 4).ToString("0.####"),
                };
                numberMethodEquation.Rows.Add(row);
            }
        }

        private void generateEquation()
        {
            string equation = "";
            if (yyyy < 0.0)
            {
                equation += "-";
            }
            equation += yyyy.ToString("0.##") + "y\'\'\'";
            equation += yyy < 0.0 ? " - " : " + ";
            equation += yyy.ToString("0.##") + "y\'\'";
            equation += yy < 0.0 ? " - " : " + ";
            equation += yy.ToString("0.##") + "y\'";
            equation += y < 0.0 ? " - " : " + ";
            equation += y.ToString("0.##") + "y";
            equation += "=0";

            differenceEquation.Text = equation;
            differenceEquation.ForeColor = Color.Black;
        }

        private bool getNumbers()
        {
            try
            {
                yyyy = Double.Parse(thirdDerivative.Text);
                yyy = Double.Parse(secondDerivative.Text);
                yy = Double.Parse(firstDerivative.Text);
                y = Double.Parse(zeroDerivative.Text);
                yyy0 = Double.Parse(defaultSecondD.Text);
                yy0 = Double.Parse(defaultFirstD.Text);
                y0 = Double.Parse(defaultZeroD.Text);
                h = Double.Parse(stepBox.Text);
                n = int.Parse(countBox.Text);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
