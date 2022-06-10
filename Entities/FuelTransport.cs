
namespace Entities {

    public abstract class FuelTransport : Transport {

        private int _numberOfSeats;

        public int NumberOfSeats {
            get => _numberOfSeats; 
            set => _numberOfSeats = value; 
        }
    }
}