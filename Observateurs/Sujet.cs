using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JeuxDeCartes.Observateurs
{
    public class Sujet : ISujet
    {
        private List<IObservateur> observateurs = new List<IObservateur>();

        public void AjouterObservateur(IObservateur observateur)
        {
            observateurs.Add(observateur);
        }

        public void SupprimerObservateur(IObservateur observateur)
        {
            observateurs.Remove(observateur);
        }

        public void AvertirObservateurs(string message)
        {
            foreach (var observateur in observateurs)
            {
                observateur.MiseAJour(message);
            }
        }
    }
}
