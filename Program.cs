using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JeuxDeCartes.Modèles;
using JeuxDeCartes.Joueurs;
using JeuxDeCartes.Jeux;

namespace JeuxDeCartes
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Initialiser le jeu de cartes
            PaireDeCartes jeuDeCartes = new PaireDeCartes();
            jeuDeCartes.Initialiser();

            // Créer le jeu de pêche (il doit être créé avant les joueurs)
            JeuxDePêche jeuDePêche = new JeuxDePêche(new List<Joueur>(), jeuDeCartes);

            // Initialisation des joueurs en passant le jeu comme sujet
            List<Joueur> joueurs = new List<Joueur>
            {
                new Joueur("Bodiang", "Diena", 1, jeuDePêche),
                new Joueur("Sanoussi", "Fily", 2, jeuDePêche),
                new Joueur("Dienaba", "Fily", 3, jeuDePêche)
            };

            // Assigner les joueurs au jeu
            jeuDePêche.Joueurs = joueurs;

            Console.WriteLine(jeuDePêche != null ? "jeuDePêche est initialisé." : "jeuDePêche est null !");

            // Distribuer les cartes
            await jeuDePêche.DistribuerCartes();

            // Démarrer le jeu
            await jeuDePêche.Jouer();

            // Afficher le bilan des points
            jeuDePêche.AfficherBilan();

            // Apres sortir ... like c'est terminer!
        }
    }
}
