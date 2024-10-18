using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxDeCartes.Observateurs
{
    public interface IObservateur
    {
        void MiseAJour(string message);
    }
}
