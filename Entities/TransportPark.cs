using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TransportPark
    {
        private List<Transport> _transports;

        public void AddTransport(Transport transport)
        {
            if (_transports == null)
            {
                _transports = new List<Transport>();
            }

            _transports.Add(transport);
        }

        public List<Transport> Transports
        {
            get { return _transports; }
        } 
    }
}
