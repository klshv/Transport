namespace Entities {

    public class Trolley : ElectricTransport {

        public override float GetPricePerKilometer() {
            return 6.0f;
        }

        public TypeElectricTransport TypeElectricTransport { get; set; }

    }
}