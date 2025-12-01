using System.Windows;
using System.Windows.Controls;
using LogCore.Logging;
using LogCore.Models;
using CoreLogManager = LogCore.Logging.LogManager; // алиас, за да няма объркване с името на проекта

namespace LogManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LevelComboBox.SelectedIndex = 0; // Info по подразбиране
        }

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageTextBox.Text;

            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Моля, въведете съобщение.");
                return;
            }

            LogLevel level = GetSelectedLevel();

            // Използваме Singleton-а от LogCore
            CoreLogManager.Instance.Log(level, message);

            MessageTextBox.Clear();
            LoadAllLogs();
        }

        private LogLevel GetSelectedLevel()
        {
            var selectedItem = LevelComboBox.SelectedItem as ComboBoxItem;
            string content = selectedItem?.Content?.ToString() ?? "Info";

            return content switch
            {
                "Info" => LogLevel.Info,
                "Warning" => LogLevel.Warning,
                "Error" => LogLevel.Error,
                _ => LogLevel.Info
            };
        }

        private void LoadAllButton_Click(object sender, RoutedEventArgs e)
        {
            LoadAllLogs();
        }

        private void LoadInfoButton_Click(object sender, RoutedEventArgs e)
        {
            LoadLogsByLevel(LogLevel.Info);
        }

        private void LoadWarningButton_Click(object sender, RoutedEventArgs e)
        {
            LoadLogsByLevel(LogLevel.Warning);
        }

        private void LoadErrorButton_Click(object sender, RoutedEventArgs e)
        {
            LoadLogsByLevel(LogLevel.Error);
        }

        private void LoadAllLogs()
        {
            var logs = CoreLogManager.Instance.ReadAll();
            LogsListBox.ItemsSource = logs;
        }

        private void LoadLogsByLevel(LogLevel level)
        {
            var logs = CoreLogManager.Instance.ReadByLevel(level);
            LogsListBox.ItemsSource = logs;
        }
    }
}
