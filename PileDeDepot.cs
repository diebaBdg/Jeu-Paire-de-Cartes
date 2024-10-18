using JeuxDeCartes.Modèles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxDeCartes
{
    public class PileDeDepot
    {
        public List<Carte> Cartes { get; private set; }

        // Constructeur qui prend une liste de cartes
        public PileDeDepot(List<Carte> cartes)
        {
            Cartes = cartes;
        }
        public PileDeDepot()
        {
            Cartes = new List<Carte>();
        }

        public void AjouterCarte(Carte carte)
        {
            Cartes.Add(carte);
        }

        // Retourne la carte au sommet
        public Carte? CarteSommet()
        {
            return Cartes.Count > 0 ? Cartes[Cartes.Count - 1] : (Carte?)null; //J'ai utilisé Carte? pour lever l'erreur de type non-nullable
        }


        // Mélange les cartes du dépôt pour être réutilisées comme pile de pioche
        public void Melanger()
        {
            Random rng = new Random();
            int n = Cartes.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                Carte value = Cartes[k];
                Cartes[k] = Cartes[n];
                Cartes[n] = value;
            }
        }

        public void Vider()
        {
            Cartes.Clear();
        }

    }
}
