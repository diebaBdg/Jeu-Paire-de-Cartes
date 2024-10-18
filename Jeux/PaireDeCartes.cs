using JeuxDeCartes.Modèles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JeuxDeCartes.Jeux
{
    public class PaireDeCartes
    {
        public List<Carte> Cartes { get; private set; } // Collection contenant les 52 cartes

        public PaireDeCartes()
        {
            Cartes = new List<Carte>();
            Initialiser(); // Appel de la méthode d'initialisation pour générer les cartes
        }

        // Méthode pour générer les 52 cartes (toutes les combinaisons de valeurs et couleurs)
        public void Initialiser()
        {
            // Créer les 52 cartes
            foreach (Couleur couleur in Enum.GetValues(typeof(Couleur)))
            {
                foreach (Valeur valeur in Enum.GetValues(typeof(Valeur)))
                {
                    // Créer un nom basé sur la valeur et la couleur
                    string nomCarte = $"{valeur} de {couleur}"; // Par exemple: "As de Coeur"

                    Cartes.Add(new Carte(nomCarte, valeur, couleur));
                }
            }
        }

        // Mélanger les cartes
        public void Melanger()
        {
            Random rng = new Random();
            Cartes = Cartes.OrderBy(c => rng.Next()).ToList();
        }
    }
}
