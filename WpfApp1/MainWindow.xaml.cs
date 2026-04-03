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

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        // Почасовые ставки
        private const double AssistantRate = 150;
        private const double DocentRate = 250;
        private const double ProfessorRate = 350;
        private const double TaxRate = 0.13;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            // Сброс полей
            AccruedTextBlock.Text = string.Empty;
            TaxAmountTextBlock.Text = string.Empty;

            // Проверка ввода часов
            if (!double.TryParse(HoursTextBox.Text, out double hours) || hours < 0)
            {
                MessageBox.Show("Введите корректное количество часов (неотрицательное число).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Определение ставки
            double rate = GetHourlyRate();
            double accrued = hours * rate;
            double tax = 0;

            // Расчёт налога
            if (TaxCheckBox.IsChecked == true)
            {
                tax = accrued * TaxRate;
            }

            double netSalary = accrued - tax;

            // Отображение результата (округление до 2 знаков)
            AccruedTextBlock.Text = $"{accrued:F2} руб";
            TaxAmountTextBlock.Text = $"{tax:F2} руб";
        }

        private double GetHourlyRate()
        {
            if (AssistantRadio.IsChecked == true)
                return AssistantRate;
            if (DocentRadio.IsChecked == true)
                return DocentRate;
            if (ProfessorRadio.IsChecked == true)
                return ProfessorRate;
            return AssistantRate; 
        }
    }
}
