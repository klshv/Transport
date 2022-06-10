namespace Logic {

    public class RouteSegment {

        public float Distance { private set; get; }
        public Stop Begin { private set; get; }
        public Stop End { private set; get; }

        public RouteSegment(Stop begin, Stop end, float distance) {
            Begin = begin;
            End = end;
            Distance = distance;
        }
    }
}
