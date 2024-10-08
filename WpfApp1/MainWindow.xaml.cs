using System;
using System.Windows;

namespace FigureApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик смены фигуры в ComboBox
        private void FigureComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selectedFigure = ((System.Windows.Controls.ComboBoxItem)figureComboBox.SelectedItem).Content.ToString();

            // Для круга отображаем только один параметр
            if (selectedFigure == "Круг")
            {
                param2TextBox.Visibility = Visibility.Collapsed;
                param2Label.Visibility = Visibility.Collapsed;
            }
            else
            {
                param2TextBox.Visibility = Visibility.Visible;
                param2Label.Visibility = Visibility.Visible;
            }
        }

        private void OnCalculateClick(object sender, RoutedEventArgs e)
        {
            string selectedFigure = ((System.Windows.Controls.ComboBoxItem)figureComboBox.SelectedItem).Content.ToString();
            double param1, param2;
            Figure figure = null;

            // Проверяем, удалось ли преобразовать первый параметр
            if (!double.TryParse(param1TextBox.Text, out param1))
            {
                MessageBox.Show("Некорректный ввод параметра 1");
                return;
            }

            switch (selectedFigure)
            {
                case "Круг":
                    figure = new Circle(param1);
                    break;

                case "Прямоугольник":
                    if (!double.TryParse(param2TextBox.Text, out param2))
                    {
                        MessageBox.Show("Некорректный ввод параметра 2");
                        return;
                    }
                    figure = new Rectangle(param1, param2);
                    break;

                case "Треугольник":
                    if (!double.TryParse(param2TextBox.Text, out param2))
                    {
                        MessageBox.Show("Некорректный ввод параметра 2");
                        return;
                    }
                    figure = new Triangle(param1, param2);
                    break;
            }

            // Используем делегат для вызова метода CalculateArea
            if (figure != null)
            {
                CalculateAreaDelegate calculateAreaDelegate = figure.CalculateArea;
                double area = calculateAreaDelegate();
                resultTextBlock.Text = $"Площадь: {area:F2}";
            }
        }
    }
}
