using System;
using System.Windows;


namespace GUIView {

    public partial class AddWindow : Window {

        public AddWindow() {
            InitializeComponent();
        }
        
        private void SaveButtonClick(object sender, RoutedEventArgs e) {
            if (!stopTextBox.Text.Equals(String.Empty) && !priceTextBox.Text.Equals(String.Empty)) {
                MainWindow.stops.Insert(MainWindow.selectIndex, stopTextBox.Text);
                MainWindow.prices.Insert(MainWindow.selectIndex, float.Parse(priceTextBox.Text));
            }
            Close();
        }

        private void RevertButtonClick(object sender, RoutedEventArgs e) {
            Close();
        }

    }

}