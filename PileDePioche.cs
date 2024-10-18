using JeuxDeCartes.Modèles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxDeCartes
{
    public class PileDePioche
    {
        public Stack<Carte> Cartes { get; private set; }

        // Initialisation de la pile de pioche avec une liste de cartes
        public PileDePioche(List<Carte> cartes)
        {
            Cartes = new Stack<Carte>(cartes);
        }

        public Carte? Piocher()
        {
            return Cartes.Count > 0 ? Cartes.Pop() : (Carte?)null; // Renvoie null si la pile est vide
        }


        public int CartesRestantes()
        {
            return Cartes.Count;
        }

        public void Melanger()
        {
            var cartesMelangees = Cartes.ToList();
            Random rng = new Random();
            cartesMelangees = cartesMelangees.OrderBy(c => rng.Next()).ToList();
            Cartes = new Stack<Carte>(cartesMelangees);
        }

    }

}
