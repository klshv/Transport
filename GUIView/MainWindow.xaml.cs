using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using Entities;
using logic;
using Logic;

namespace GUIView {

    public partial class MainWindow {

        public event PropertyChangedEventHandler PropertyChanged;

        public static ObservableCollection<string> stops = new ObservableCollection<string>
        { "Лагерный сад", "ул. Учебная", "ТЭМЗ", "Научная библиотека ТГУ", "ТГУ", "пл. Новособорная", "Краеведческий музей", "ул. Гоголя" };
        public static ObservableCollection<float> distance = new ObservableCollection<float> { 0.8f, 0.7f, 1.0f, 0.5f, 0.6f, 0.75f, 1.2f };
        public static int selectIndex;
        public ObservableCollection<string> transportType = new ObservableCollection<string> { "Автобус", "Трамвай", "Тролейбус"};
        public static ObservableCollection<float> prices;

        private string numberRoute = "33";

        public MainWindow() {
            InitializeComponent();
            stopList.SelectedIndex = 0;
            selectIndex = stopList.SelectedIndex;
            ComboBox.SelectedIndex = 0;
            stopList.ItemsSource = stops;
        }

        private void Сalculate(string end) {
            if (stopList.SelectedIndex > 1) {
                switch (ComboBox.SelectedIndex) {
                    case 0:
                        var bus = new Bus();
                        if (stopList.SelectedIndex == 1) {
                            resultBlock.Text = $"Стоимость проезда маршрутом {numberRoute}: \n от {stops[0]} до {stops[1]} составляет {distance[0]*bus.GetPricePerKilometer()} рублей";
                        }
                        var routeSegment =
                            new ObservableCollection<RouteSegment>() { new RouteSegment(new Stop(stops[0]), new Stop(stops[1]), distance[0]*bus.GetPricePerKilometer())};
                        prices = new ObservableCollection<float>();
                        foreach (var dist in distance) {
                            prices.Add(dist*bus.GetPricePerKilometer());
                        }
                        for (int i = 1; i < stops.Count - 1; i++) {
                            routeSegment.Add(new RouteSegment(new Stop(stops[i]), new Stop(stops[i + 1]), prices[i]));
                        }
                        var route33 = new Route(new InClassName(numberRoute, routeSegment, new Bus()));
                        var begin = new Stop(stopList.Items[0].ToString());
                        var stop = new Stop(end);

                        var ride = new Ride(route33, begin, stop);
                        WriteRidePrice(ride);
                        break;
                    case 1:
                        var tram = new Tram();
                        numberRoute = "16";
                        if (stopList.SelectedIndex == 1) {
                            resultBlock.Text = $"Стоимость проезда маршрутом {numberRoute}: \n от {stops[0]} до {stops[1]} составляет {distance[0]*tram.GetPricePerKilometer()} рублей";
                        }
                        routeSegment =
                            new ObservableCollection<RouteSegment>() { new RouteSegment(new Stop(stops[0]), new Stop(stops[1]), distance[0]*tram.GetPricePerKilometer())};
                        prices = new ObservableCollection<float>();
                        foreach (var dist in distance) {
                            prices.Add(dist*tram.GetPricePerKilometer());
                        }
                        for (int i = 1; i < stops.Count - 1; i++) {
                            routeSegment.Add(new RouteSegment(new Stop(stops[i]), new Stop(stops[i + 1]), prices[i]));
                        }
                        route33 = new Route(new InClassName(numberRoute, routeSegment, new Tram()));
                        begin = new Stop(stopList.Items[0].ToString());
                        stop = new Stop(end);

                        ride = new Ride(route33, begin, stop);
                        WriteRidePrice(ride);
                        break;
                    case 2:
                        var trolley = new Trolley();
                        numberRoute = "4";
                        if (stopList.SelectedIndex == 1) {
                            resultBlock.Text = $"Стоимость проезда маршрутом {numberRoute}: \n от {stops[0]} до {stops[1]} составляет {distance[0]*trolley.GetPricePerKilometer()} рублей";
                        }
                        routeSegment =
                            new ObservableCollection<RouteSegment>() { new RouteSegment(new Stop(stops[0]), new Stop(stops[1]), distance[0]*trolley.GetPricePerKilometer())};
                        prices = new ObservableCollection<float>();
                        foreach (var dist in distance) {
                            prices.Add(dist*trolley.GetPricePerKilometer());
                        }
                        for (int i = 1; i < stops.Count - 1; i++) {
                            routeSegment.Add(new RouteSegment(new Stop(stops[i]), new Stop(stops[i + 1]), prices[i]));
                        }
                        route33 = new Route(new InClassName(numberRoute, routeSegment, new Trolley()));
                        begin = new Stop(stopList.Items[0].ToString());
                        stop = new Stop(end);

                        ride = new Ride(route33, begin, stop);
                        WriteRidePrice(ride);
                        break;
                }
            } else {
                resultBlock.Text = $"Стоимость проезда маршрутом {33}: \n от {stops[0]} до {stops[0]} составляет {0} рублей";
            }
        }

        private void WriteRidePrice(Ride ride) {
            resultBlock.Text = $"Стоимость проезда маршрутом {ride.Route.Number}: \n от {ride.Begin.Name} до {ride.End.Name} составляет {ride.CalculatePrice()} рублей";
        }

        private void AddButtonClick(object sender, RoutedEventArgs e) {
            selectIndex = stopList.SelectedIndex;
            AddWindow AddWd = new AddWindow();
            AddWd.Owner = this;
            AddWd.Show();
            
            stopList.ItemsSource = stops;
            stopList.SelectedIndex = 0;
        }

        private void DeleteButtonClick(object sender, EventArgs e) {
            distance.RemoveAt(stopList.SelectedIndex);
            stops.RemoveAt(stopList.SelectedIndex);
            stopList.ItemsSource = stops;
            stopList.SelectedIndex = 0;
        }

        private void ButtonClick(object sender, RoutedEventArgs e) {
            Сalculate(stopList.SelectedItems[0].ToString());
        }

        private void TypeButtonClick(object sender, RoutedEventArgs e) {
            throw new NotImplementedException();
        }

    }
}