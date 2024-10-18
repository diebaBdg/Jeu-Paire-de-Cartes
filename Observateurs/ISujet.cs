using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxDeCartes.Observateurs
{
    public interface ISujet
    {
        void AjouterObservateur(IObservateur observateur);
        void SupprimerObservateur(IObservateur observateur);
        void AvertirObservateurs(string message);
    }
}
