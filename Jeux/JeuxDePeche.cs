using JeuxDeCartes.Joueurs;
using JeuxDeCartes.Modèles;
using JeuxDeCartes.Observateurs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JeuxDeCartes.Jeux
{
    public class JeuxDePêche : Sujet // Hérite de Sujet
    {
        public List<Joueur> Joueurs { get; set; } 
        public PileDePioche PileDePioche { get; private set; }
        public PileDeDepot PileDeDepot { get; private set; }
        private Random random = new Random();

        public JeuxDePêche(List<Joueur> joueurs, PaireDeCartes jeuDeCartes)
        {
            Joueurs = joueurs; // Initialise Joueurs
            PileDePioche = new PileDePioche(jeuDeCartes.Cartes);
            PileDeDepot = new PileDeDepot(new List<Carte>());
        }

        public async Task DistribuerCartes()
        {
            int nombreCartesParJoueur = random.Next(5, 9); // Choisit aléatoirement entre 5 et 8 cartes

            foreach (var joueur in Joueurs)
            {
                for (int i = 0; i < nombreCartesParJoueur; i++)
                {
                    Carte? carte = PileDePioche.Piocher(); // Pioche une carte (nullable)

                    if (carte != null)
                    {
                        joueur.AjouterCarte((Carte)carte); // Convertit carte en Carte ici
                    }
                    else
                    {
                        Console.WriteLine("La pile de pioche est vide, impossible de distribuer plus de cartes.");
                        break;
                    }
                }
            }

            Console.WriteLine($"Distribution terminée. Chaque joueur a reçu {nombreCartesParJoueur} cartes.");
        }

        public async Task Jouer()
        {
            // Mélanger la pile de pioche
            PileDePioche.Melanger();

            // Vérifier si on a au moins 2 joueurs
            if (Joueurs == null || Joueurs.Count < 2)
            {
                Console.WriteLine("Aucun joueur n'est disponible pour jouer.");
                return;
            }

            // Choisir un joueur de départ aléatoirement
            int indexJoueur = random.Next(Joueurs.Count);
            Joueur joueurActuel = Joueurs[indexJoueur];

            Console.WriteLine($"Le joueur {joueurActuel.Nom} commence la partie.");

            // Ajouter chaque joueur comme observateur
            foreach (var joueur in Joueurs)
            {
                AjouterObservateur(joueur);
            }

            bool partieTerminee = false;

            while (!partieTerminee)
            {
                // Le joueur actuel joue une carte
                Carte? carteJouee = joueurActuel.JouerCarte(PileDeDepot.CarteSommet());

                if (carteJouee != null)
                {
                    PileDeDepot.AjouterCarte((Carte)carteJouee);
                    Console.WriteLine($"{joueurActuel.Nom} joue {carteJouee}");

                    // Notifier les observateurs de l'action du joueur
                    NotifierObservateurs($"{joueurActuel.Nom} a joué {carteJouee} !");

                    // **Nouvelle vérification : notifier si un joueur n'a plus qu'une carte**
                    NotifierJoueurs();

                    // Gérer les cartes spéciales
                    if (carteJouee.HasValue && carteJouee.Value.EstSpeciale())
                    {
                        Console.WriteLine($"{joueurActuel.Nom} a joué une carte spéciale : {carteJouee.Value} !");

                        // Appliquer l'effet des cartes spéciales
                        switch (carteJouee.Value.Valeur)
                        {
                            case Valeur.Valet:
                                // Logique pour changer de couleur
                                break;
                            case Valeur.As:
                                // Sauter le tour du joueur suivant
                                indexJoueur = (indexJoueur + 1) % Joueurs.Count;
                                break;
                            case Valeur.Dix:
                                // Inverser l'ordre du jeu
                                Joueurs.Reverse();
                                break;
                            case Valeur.Sept:
                                // Forcer le joueur suivant à piocher 2 cartes
                                Joueur joueurSuivant = Joueurs[(indexJoueur + 1) % Joueurs.Count];
                                joueurSuivant.Piocher(PileDePioche, 2);
                                break;
                        }
                    }
                    else
                    {
                        Carte? cartePiochee = PileDePioche.Piocher();
                        if (cartePiochee != null)
                        {
                            joueurActuel.AjouterCarte((Carte)cartePiochee);
                            Console.WriteLine($"{joueurActuel.Nom} ne peut pas jouer, il pioche une carte.");
                        }
                        else
                        {
                            Console.WriteLine("La pile de pioche est vide, impossible de piocher une carte.");
                            // Mélanger la pile de dépôt pour en faire la nouvelle pile de pioche
                            PileDeDepot.Melanger();
                            PileDePioche = new PileDePioche(PileDeDepot.Cartes);
                            PileDeDepot.Vider();
                        }
                    }


                    // Vérifier la condition de fin de partie (quand un joueur n'a plus de cartes)
                    if (joueurActuel.Main.Count == 0)
                    {
                        partieTerminee = true;
                        Console.WriteLine($"{joueurActuel.Nom} a gagné la partie !");
                        break;
                    }

                    // Passer au joueur suivant
                    indexJoueur = (indexJoueur + 1) % Joueurs.Count;
                    joueurActuel = Joueurs[indexJoueur];

                    // Délai pour simuler un jeu plus réaliste
                    await Task.Delay(1000);
                }
            }
        }

        // Méthode pour calculer et afficher les points des joueurs
        public void AfficherScoresFinals()
        {
            Console.WriteLine("Fin de la partie ! Voici les scores finaux :");
            foreach (var joueur in Joueurs)
            {
                int points = joueur.CalculerPoints();
                Console.WriteLine($"{joueur.Nom} a {points} points.");
            }

            // Déterminer le gagnant (celui avec le moins de points)
            var gagnant = Joueurs.OrderBy(j => j.CalculerPoints()).First();
            Console.WriteLine($"{gagnant.Nom} remporte la partie avec {gagnant.CalculerPoints()} points !");
        }

        public void MinimiserPoints()
        {
            foreach (var joueur in Joueurs)
            {
                Carte? carteSpeciale = joueur.Main.FirstOrDefault(c => c.EstSpeciale()); // Utiliser Carte? pour permettre la nullabilité(le français c'est compliqué)
                if (carteSpeciale != null) // Vérifiez que la carte spéciale existe
                {
                    joueur.AjouterCarteAuDepot(carteSpeciale.Value); // Utilisez .Value pour accéder à la carte
                    Console.WriteLine($"{joueur.Nom} joue une carte spéciale : {carteSpeciale.Value.Nom}");
                }
                else
                {
                    // Si aucune carte spéciale, jouer la carte avec la plus haute valeur
                    Carte? carteLaPlusHaute = joueur.Main.OrderByDescending(c => c.Valeur).FirstOrDefault(); // Utiliser Carte?
                    if (carteLaPlusHaute != null) // Vérifiez que la carte existe
                    {
                        joueur.AjouterCarteAuDepot(carteLaPlusHaute.Value); // Utilisez .Value pour accéder à la carte
                        Console.WriteLine($"{joueur.Nom} joue sa carte la plus haute : {carteLaPlusHaute.Value.Nom}");
                    }
                }
            }
        }

        public void AfficherBilan()
        {
            foreach (var joueur in Joueurs) // Utiliser 'Joueurs' ici
            {
                Console.WriteLine($"{joueur.Nom} a {joueur.CalculerPoints()} points.");
            }
        }

        // Méthode pour notifier les joueurs
        public void NotifierJoueurs()
        {
            foreach (var joueur in Joueurs)
            {
                if (joueur.Main.Count == 1)
                {
                    Console.WriteLine($"Attention, {joueur.Nom} n'a plus qu'une carte !");
                }
            }
        }

        // Méthode pour notifier les observateurs
        public void NotifierObservateurs(string message)
        {
            AvertirObservateurs(message); 
        }
    }
}

