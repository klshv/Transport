
namespace Entities {

    public abstract class Transport {

        private string _name;

        public string Name {
            get => _name;
            set => _name = value; 
        }

        public abstract float GetPricePerKilometer();
    }
}
