using JeuxDeCartes.Modèles;
using System;
using System.Collections.Generic;
using JeuxDeCartes.Observateurs;
using JeuxDeCartes.Jeux;

namespace JeuxDeCartes.Joueurs
{
    // Classe représentant un joueur dans le jeu, hérite de Personne
    public class Joueur : Personne, IObservateur
    {
        public List<Carte> Main { get; private set; }
        private ISujet sujet;

        public Joueur(string nom, string prenom, int identifiant, ISujet sujet)
    : base(nom, prenom, identifiant)
        {
            Main = new List<Carte>();
            this.sujet = sujet;
            if (this.sujet != null)
            {
                this.sujet.AjouterObservateur(this); // S'inscrire comme observateur seulement si sujet n'est pas null
            }
            else
            {
                Console.WriteLine("Sujet est null, observateur non ajouté.");
            }
        }


        // Ajoute une carte à la main du joueur, s'il y a moins de 8 cartes
        public void AjouterCarte(Carte carte)
        {
            if (Main.Count < 8)
                Main.Add(carte);
            else
                Console.WriteLine($"{Nom} ne peut pas avoir plus de 8 cartes.");
        }

        // Méthode pour jouer une carte de la main
        public Carte? JouerCarte(Carte? carteAuSommet)
        {
            if (Main.Count > 0)
            {
                Carte carteJouee = Main[0]; // Choisir une carte en fonction des règles
                Main.RemoveAt(0); // Retire la carte de la main

                // Vérification du nombre de cartes restantes
                if (Main.Count == 1)
                {
                    Console.WriteLine($"Attention : {Nom} n'a plus qu'une carte !");
                    sujet.AvertirObservateurs($"Attention : {Nom} n'a plus qu'une carte !");
                }

                return carteJouee; // Retourne la carte jouée
            }

            return null;
        }


        public void AjouterCarteAuDepot(Carte carte)
        {
            if (Main.Contains(carte))
            {
                Main.Remove(carte);
                Console.WriteLine($"{Nom} a ajouté {carte} à la pile de dépôt.");
            }
            else
            {
                Console.WriteLine("Cette carte n'est pas dans la main du joueur.");
            }
        }

        public void Piocher(PileDePioche pile, int nombreDeCartes)
        {
            if (pile == null)
            {
                Console.WriteLine("La pile ne peut pas être nulle.");
                return;
            }

            for (int i = 0; i < nombreDeCartes; i++)
            {
                Carte? cartePiochee = pile.Piocher();
                if (cartePiochee != null)
                {
                    AjouterCarte((Carte)cartePiochee);
                }
            }
        }


        public int CalculerPoints()
        {
            int totalPoints = 0;
            foreach (var carte in Main)
            {
                totalPoints += (int)carte.Valeur; // Tous les cas sont déjà gérés par l'énumération
            }
            return totalPoints;
        }

        public void AssocierJeu(JeuxDePêche jeu)
        {
            this.sujet = jeu; // Remplace l'ancienne référence par la nouvelle
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Cartes en main: {Main.Count}, Points: {CalculerPoints()}";
        }

        public void MiseAJour(string message)
        {
            Console.WriteLine($"Notification pour {Nom}: {message}");
        }
    }
}
