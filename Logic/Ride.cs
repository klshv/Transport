using Logic;

namespace logic {

    public class Ride {

        public Stop Begin { private set; get; }
        public Stop End { private set; get; }
        public Route Route { private set; get; }


        public Ride(Route route, Stop begin, Stop end) {
            Begin = begin;
            End = end;
            Route = route;
        }

        public float CalculatePrice() {
            // 1 - найти расстояние между begin и end 
            var distance = Route.CalculateDistance(Begin, End);
            // 2 - определить стоимость для данного транспорта
            return distance * Route.Transport.GetPricePerKilometer();
        }
    }
}