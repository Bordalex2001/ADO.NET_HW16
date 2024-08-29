using ADO.NET_HW2;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADO.NET_HW16
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

        private void showAllBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Repository repos = new())
                {
                    string query = $@"Select * from List";

                    var list = repos.connection?.Query<List>(query);
                    dataGridView.ItemsSource = list?.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void showNamesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Repository repos = new())
                {
                    string query = $@"Select Name as N'Назва' from List";

                    var namesList = repos.connection?.Query(query);
                    dataGridView.ItemsSource = namesList?.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void showColorsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Repository repos = new())
                {
                    string query = $@"Select distinct Color as N'Колір' from List";

                    var colorList = repos.connection?.Query(query);
                    dataGridView.ItemsSource = colorList?.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void minCaloriesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Repository repos = new())
                {
                    string query = $@"Select min(CaloricContent) from List";

                    var calories = repos.connection?.QuerySingle<int>(query);
                    MessageBox.Show($"Найменша кількість калорій: {calories}", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void maxCaloriesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Repository repos = new())
                {
                    string query = $@"Select max(CaloricContent) from List";

                    var calories = repos.connection?.QuerySingle<int>(query);
                    MessageBox.Show($"Найбільша кількість калорій: {calories}", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void avgCaloriesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Repository repos = new())
                {
                    string query = $@"Select avg(CaloricContent) from List";

                    var calories = repos.connection?.QuerySingle<int>(query);
                    MessageBox.Show($"Середня кількість калорій: {calories}", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void countVegetablesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Repository repos = new())
                {
                    string query = $@"Select count(*) from List where Type = N'Овочі'";

                    var vegetables = repos.connection?.QuerySingle<int>(query);
                    MessageBox.Show($"Кількість овочів: {vegetables}", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void countFruitsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Repository repos = new())
                {
                    string query = $@"Select count(*) from List where Type = N'Фрукти' or Type = N'Ягоди'";

                    var fruits = repos.connection?.QuerySingle<int>(query);
                    MessageBox.Show($"Кількість фруктів: {fruits}", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void countByChosenColorBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string? colorInput = InputPrompt.ShowDialog("Уведіть колір:");
                if (!string.IsNullOrWhiteSpace(colorInput))
                {
                    using (Repository repos = new())
                    {
                        string query = $@"Select count(*) from List where Color = @color";

                        var quantity = repos.connection?.QuerySingle<int>(query, new { color = colorInput});
                        MessageBox.Show($"Кількість овочів і фруктів кольору \"{colorInput}\": {quantity}", "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void countEachColorBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Repository repos = new())
                {
                    string query = $@"Select Color as N'Колір', count(*) as N'Кількість' from List group by Color";
                    
                    var eachColor = repos.connection?.Query(query);
                    dataGridView.ItemsSource = eachColor?.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void caloriesBelowValueBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string? caloriesInput = InputPrompt.ShowDialog("Уведіть кількість калорій:");
                if (!string.IsNullOrWhiteSpace(caloriesInput) && int.TryParse(caloriesInput, out int calories))
                {
                    using (Repository repos = new())
                    {
                        string query = $@"Select * from List where CaloricContent < @calories";

                        var belowValue = repos.connection?.Query<List>(query, new { calories = caloriesInput });
                        dataGridView.ItemsSource = belowValue?.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void caloriesAboveValueBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string? caloriesInput = InputPrompt.ShowDialog("Уведіть кількість калорій:");
                if (!string.IsNullOrWhiteSpace(caloriesInput) && int.TryParse(caloriesInput, out int calories))
                {
                    using (Repository repos = new())
                    {
                        string query = $@"Select * from List where CaloricContent > @calories";

                        var aboveValue = repos.connection?.Query<List>(query, new { calories = caloriesInput });
                        dataGridView.ItemsSource = aboveValue?.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void caloriesInRangeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string? minCaloriesInput = InputPrompt.ShowDialog("Уведіть найменшу кількість калорій:");
                string? maxCaloriesInput = InputPrompt.ShowDialog("Уведіть найбільшу кількість калорій:");
                if ((!string.IsNullOrWhiteSpace(minCaloriesInput) && int.TryParse(minCaloriesInput, out int minCalories))
                    && (!string.IsNullOrWhiteSpace(maxCaloriesInput) && int.TryParse(maxCaloriesInput, out int maxCalories)))
                {
                    if (minCalories > maxCalories || maxCalories < minCalories)
                    {
                        MessageBox.Show("Найменше значення більше за найбільше. Спробуйте заповнити поля ще раз", "Ой", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    using (Repository repos = new())
                    {
                        string query = $@"Select * from List where CaloricContent between @minCalories and @maxCalories";

                        var range = repos.connection?.Query<List>(query, 
                            new { 
                                mincalories = minCaloriesInput,
                                maxCalories = maxCaloriesInput
                            });
                        dataGridView.ItemsSource = range?.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void yellowOrRedColorBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Repository repos = new Repository())
                {
                    string query = "Select * from List where Color = N'Жовтий' or Color = N'Червоний'";

                    var yellowOrRed = repos.connection?.Query<List>(query);
                    dataGridView.ItemsSource = yellowOrRed?.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}