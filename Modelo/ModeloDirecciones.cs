using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ModeloDirecciones
    {
        public int ID_DIR { get; set; }
        public int ID_CLIENTE { get; set; }
        public string CALLE_PRINCIPAL_CLI { get; set; }
        public string CALLE_SECUNDARIA_CLI { get; set; }
    }
}
