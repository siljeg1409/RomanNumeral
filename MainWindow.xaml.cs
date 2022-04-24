using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace RomanNumeral
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNumber.Text.Trim() == "")
            {
                txtRoman.Text = "";
                return;
            }
            try { txtRoman.Text = convertNumberToRoman(Int32.Parse(txtNumber.Text)); }
            catch (Exception ex) { string msg = ex.Message; }
        }

        private string convertNumberToRoman(int number)
        {
            string roman = "";
            int i = 10;

            while (number >= i / 10)
            {
                int temp = number % i - number % (i / 10);
                int multiplier = i / 10;
                int firstDigit = (int)(temp / Math.Pow(10, (int)Math.Floor(Math.Log10(temp))));
                string prefix = multiplier > 1000 ? "_" : "";
                string sufix = multiplier > 1000 ? "_" : "";
                if (firstDigit > 5 && firstDigit < 9)
                    prefix += getRomanDigit(5, multiplier);


                switch (firstDigit)
                {

                    case 1:
                    case 6:
                        roman = $"{new string(getRomanDigit(1, multiplier), 1)}{roman}";
                        break;

                    case 2:
                    case 7:
                        roman = $"{new string(getRomanDigit(1, multiplier), 2)}{roman}";
                        break;

                    case 3:
                    case 8:
                        roman = $"{new string(getRomanDigit(1, multiplier), 3)}{roman}";
                        break;

                    case 4:
                    case 9:
                        roman = $"{getRomanDigit(1, multiplier)}{getRomanDigit(firstDigit == 4 ? 5 : 1, firstDigit == 4 ? multiplier : i)}{roman}";
                        break;

                    case 5:
                        roman = $"{getRomanDigit(5, multiplier)}{roman}";
                        break;

                }
                roman = $"{prefix}{roman}";
                i *= 10;
            }


            return roman.Trim();
        }

        private char getRomanDigit(int firstDigit, int multiplier)
        {
            if (firstDigit * multiplier > 1000)
                multiplier = multiplier / 1000;
            try
            {
                char ret = Enum.GetName(typeof(Roman), firstDigit * multiplier)[0];
                return ret;
            }
            catch (Exception)
            {
                return ' ';
            }

        }

        enum Roman
        {
            I = 1,
            V = 5,
            X = 10,
            L = 50,
            C = 100,
            D = 500,
            M = 1000
            //_I = 1000,
            //_V = 5000,
            //_X = 10000,
            //_L = 50000,
            //_C = 100000,
            //_D = 500000,
            //_M = 1000000
        }
    }
}
