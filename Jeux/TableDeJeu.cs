using JeuxDeCartes.Joueurs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxDeCartes.Jeux
{
    public class TableDeJeu
    {
        public List<Joueur> Joueurs { get; private set; }
        public PileDePioche Pioche { get; private set; }
        public PileDeDepot Depot { get; private set; }

        public TableDeJeu(List<Joueur> joueurs, PileDePioche pioche, PileDeDepot depot)
        {
            Joueurs = joueurs;
            Pioche = pioche;
            Depot = depot;
        }

        //// Démarre la partie, logge les actions des joueurs
        //public void LancerPartie()
        //{
        //    // Logique de démarrage de la partie
        //    Console.WriteLine("Partie lancée !");
        //}
    }

}
