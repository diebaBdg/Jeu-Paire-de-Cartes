using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JeuxDeCartes.Modèles
{
    public struct Carte
    {
        public string Nom { get; set; }
        public Valeur Valeur { get; set; }
        public Couleur Couleur { get; set; }

        public Carte(string nom, Valeur valeur, Couleur couleur)
        {
            Nom = nom;
            Valeur = valeur;
            Couleur = couleur;
        }

        // Méthode pour vérifier si la carte est un Valet, As, 10 ou 7
        public bool EstSpeciale()
        {
            return Valeur == Valeur.As || Valeur == Valeur.Sept || Valeur == Valeur.Dix || Valeur == Valeur.Valet;
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}
