using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BrandEntity
    {
        private string _name;
        public int? Id { get; private set; }
        public string Name 
        { 
            get => _name; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Nombre no puede ser vacio", nameof(value));   
                }

                _name = value;
            }

        }

        public BrandEntity()
        { 

        }
    }
}
