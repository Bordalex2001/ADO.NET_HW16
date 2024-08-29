using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ADO.NET_HW2
{
    public class InputPrompt
    {
        public static string ShowDialog(string text)
        {
            Window prompt = new Window()
            { 
                WindowStyle = WindowStyle.SingleBorderWindow,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Width = 500,
                Height = 175,
                ResizeMode = ResizeMode.NoResize
            };

            StackPanel stackPanel = new StackPanel();
            prompt.Content = stackPanel;

            Label textLabel = new Label()
            {
                Content = text,
                Margin = new Thickness(50, 20, 0, 0),
            };
            stackPanel.Children.Add(textLabel);

            TextBox inputBox = new TextBox()
            {
                Width = 400,
                Margin = new Thickness(50, 0, 50, 0)
            };
            stackPanel.Children.Add(inputBox);

            StackPanel buttonPanel = new StackPanel() 
            { 
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(50, 10, 0, 0)
            };
            stackPanel.Children.Add(buttonPanel);

            Button submitBtn = new Button()
            {
                Content = "ОК",
                Width = 100,
                Height = 20,
                Margin = new Thickness(0, 0, 10, 0),
            };
            submitBtn.Click += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(inputBox.Text))
                {
                    MessageBox.Show("Заповніть це поле, будь ласка", "Ой", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    prompt.DialogResult = true;
                    prompt.Close();
                }
            };
            buttonPanel.Children.Add(submitBtn);

            Button cancelBtn = new Button()
            {
                Content = "Скасувати",
                Width = 100,
                Height = 20,
                Margin = new Thickness(5, 0, 0, 0),
            };
            cancelBtn.Click += (sender, e) =>
            {
                prompt.DialogResult = false;
                prompt.Close();
            };
            buttonPanel.Children.Add(cancelBtn);

            return prompt.ShowDialog() == true ? inputBox.Text : null;
        }
    }
}