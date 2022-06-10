using System;
using System.Collections.Generic;

using Entities;
using logic;
using Logic;

namespace GUI {
    class Program {
        static void Main(string[] args) {
            var route33 = new Route(new InClassName("33", new List<RouteSegment>() {
                new RouteSegment(new Stop("Лагерный сад"), new Stop("ул. Учебная"), 0.8f),
                new RouteSegment(new Stop("ул. Учебная"), new Stop("ТЭМЗ"), 0.7f),
                new RouteSegment(new Stop("ТЭМЗ"), new Stop("Научная библиотека ТГУ"), 1.0f),
                new RouteSegment(new Stop("Научная библиотека ТГУ"), new Stop("ТГУ"), 0.5f),
                new RouteSegment(new Stop("ТГУ"), new Stop("пл. Новособорная"), 0.6f),
                new RouteSegment(new Stop("пл. Новособорная"), new Stop("Краеведческий музей"), 0.75f),
                new RouteSegment(new Stop("Краеведческий музей"), new Stop("ул. Гоголя"), 1.2f),
            }, new Bus()));

            var begin = new Stop("ТЭМЗ");
            var end = new Stop("пл. Новособорная");

            var ride = new Ride(route33, begin, end);
            WriteRidePrice(ride);
        }

        private static void WriteRidePrice(Ride ride) {
            Result = $"Стоимость проезда маршрутом {ride.Route.Number} от {ride.Begin.Name} до {ride.End.Name} составляет {ride.CalculatePrice()}";
        }
        
        private static string _result;

        public static string Result
        {
            get => _result;
            set => _result = value;
        }
    }
}