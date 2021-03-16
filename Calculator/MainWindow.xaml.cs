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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for <c>MainWindow.xaml</c>
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string decimalSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public MainWindow()
        {
            InitializeComponent();
            btnDecimal.Content = decimalSeparator;
        }

        private void NumberClick(object sender, RoutedEventArgs e)
        {
            txtCurrent.Text += (sender as Button).Content.ToString();
        }

        private void DecimalSeparatorClick(object sender, RoutedEventArgs e)
        {
            if (!txtCurrent.Text.Contains(decimalSeparator))
            {
                txtCurrent.Text += decimalSeparator;
            }
        }

        private void OperatorClick(object sender, RoutedEventArgs e)
        {
            lblOperator.Content = (e.Source as Button).Content.ToString();
            lblOperand1.Content = txtCurrent.Text;
            txtCurrent.Text = "0";
        }

        private void CalculateClick(object sender, RoutedEventArgs e)
        {
            lblOperand2.Content = txtCurrent.Text;

            try
            {
                double op1 = double.Parse(lblOperand1.Content.ToString());
                double op2 = double.Parse(lblOperand2.Content.ToString());
                switch (lblOperator.Content)
                {
                    case "+": lblResult.Content = op1 + op2; break;
                    case "-": lblResult.Content = op1 - op2; break;
                    case "*": lblResult.Content = op1 * op2; break;
                    case "/": lblResult.Content = op1 / op2; break;
                }
                txtCurrent.Text = lblResult.Content.ToString();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (OverflowException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //e.Handled = !char.IsDigit(e.Text[0]);
        }

        private void TextBoxPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key is (< Key.D0 or > Key.D9) and (< Key.NumPad0 or > Key.NumPad9);
        }

        private void AlterSignClick(object sender, RoutedEventArgs e)
        {
            txtCurrent.Text = "-" + txtCurrent.Text.TrimStart('0');
        }
    }
}
