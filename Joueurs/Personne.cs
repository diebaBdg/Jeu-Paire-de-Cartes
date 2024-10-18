using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxDeCartes.Joueurs
{
    // Classe de base représentant une personne, avec un nom et un prénom
    public class Personne
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Identifiant { get; set; }

        public Personne(string nom, string prenom, int identifiant)
        {
            Nom = nom;
            Prenom = prenom;
            Identifiant = identifiant;
        }

        public override string ToString()
        {
            return $"{Prenom} {Nom} (ID: {Identifiant})";
        }
    }

}
