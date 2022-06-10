
namespace Entities {

    public class Tram : ElectricTransport {

        public override float GetPricePerKilometer() {
            return 5.0f;
        }

        public TypeElectricTransport TypeElectricTransport { get; set; }

    }

}
