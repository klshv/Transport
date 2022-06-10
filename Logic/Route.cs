using System;
using System.Collections.Generic;
using System.Diagnostics;
using Entities;

namespace Logic {

    public class InClassName {

        public InClassName(string number, IEnumerable<RouteSegment> roads, Transport transport) {
            Number = number;
            Roads = roads;
            Transport = transport;
        }

        public string Number { get; private set; }

        public IEnumerable<RouteSegment> Roads { get; private set; }

        public Transport Transport { get; private set; }

    }

    public class Route {

        public string Number { private set; get; }
        public Transport Transport { private set; get; }
        private IEnumerable<RouteSegment> Roads { set; get; }

        public Route(InClassName inClassName) {
            Number = inClassName.Number;
            Roads = inClassName.Roads;
            Transport = inClassName.Transport;
        }

        public float CalculateDistance(Stop begin, Stop end) {
            var segmentEnumerator = Roads.GetEnumerator();
            RewindEnumeratorToStop(segmentEnumerator, begin);
            return GetDistanceToStop(segmentEnumerator, end);
        }

        private void RewindEnumeratorToStop(IEnumerator<RouteSegment> enumerator, Stop stop) {
            bool someSegmentsLeft;
            while (someSegmentsLeft = enumerator.MoveNext()) {
                if (enumerator.Current != null && enumerator.Current.Begin == stop) break;
            }

            if (!someSegmentsLeft) {
                throw new Exception($"Остановка {stop.Name} отсутствует в маршруте {Number}");
            }
        }

        private float GetDistanceToStop(IEnumerator<RouteSegment> enumerator, Stop stop) {
            bool someSegmentsLeft;
            Debug.Assert(enumerator.Current != null, "enumerator.Current != null");
            var distance = enumerator.Current.Distance;
            while ((someSegmentsLeft = enumerator.MoveNext()) && enumerator.Current?.End != stop) {
                if (enumerator.Current != null)
                    distance += enumerator.Current.Distance;
            }

            if (!someSegmentsLeft) {
                throw new Exception(
                    $"Конечная остановка {stop.Name} отсутствует в маршруте {Number} или находится раньше исходной");
            } else {
                if (enumerator.Current != null) 
                    distance += enumerator.Current.Distance;
            }

            return distance;
        }
    }
}