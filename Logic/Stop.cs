using System;

namespace Logic {

    public class Stop {//}: IEquatable<Stop> {

        public string Name { private set; get; }
        public Stop(string name) => Name = name;

        public static bool operator !=(Stop a, Stop b) => a!.Name != b!.Name;
        public static bool operator ==(Stop a, Stop b) => a!.Name == b!.Name;

        public override bool Equals(object obj) {
            return obj is Stop stop &&
                   Name == stop.Name;
        }

        public override string ToString() {
            return Name;
        }
    }
}