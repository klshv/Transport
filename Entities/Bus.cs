
namespace Entities {

    public class Bus : FuelTransport {

        public new int NumberOfSeats { get; set; }

        public override float GetPricePerKilometer() {
            return 7.5f;
        }
    }
}