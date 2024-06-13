using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace _1305
{
    public class Spiel
    {
        private int score;
        private string sprache;
        private string sprachkürzel;
        private string abfrageS;
        private string test;
        public Spiel() 
        {
            Spieler spieler = new Spieler();

            do
            {
                EntscheidFremdsprache();
            } while (NeueRunde());

            spieler.score = score;
            spieler.SpielerSpeichern();
            spieler.Highscore();
            
        }

        public void EntscheidFremdsprache()
        {
            Console.WriteLine("Welche Sprache wollen testen? Für Englisch geben Sie e ein und für Franzözisch geben Sie f ein");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "e":
                    sprache = "Englisch";
                    sprachkürzel = "e";
                    EntscheidAbfrageSprache();
                    break;
                case "f":
                    sprache = "Französisch";
                    sprachkürzel = "f";
                    EntscheidAbfrageSprache();
                    break;

            }
        }

        public void EntscheidAbfrageSprache()
        {
            Console.WriteLine($"Welche Sprache wollen Sie abgefragt werden? Für Deutsch geben Sie d ein und für " + sprache + " geben Sie " + sprachkürzel + " ein");
            string choice = Console.ReadLine();

            if (choice == "d")
            {
                abfrageS = "Deutsch";
                test = sprache;
                Abfrage abfrage = new Abfrage(sprache, abfrageS, test);
                score = abfrage.score;

            }
            else if (choice == sprachkürzel)
            {
                    abfrageS = sprache;
                    test = "Deutsch";
                    Abfrage abfrage = new Abfrage(sprache, abfrageS, test);
                    score = abfrage.score;

            }
        }

        public bool NeueRunde()
        {
            bool test = true;
            string answer;
            Console.WriteLine("Wollen Sie noch eine Runde spielen? Für nein geben Sie n ein und für ja j ein.");
            answer = Console.ReadLine();

            if(answer == "n")
            {
                test = false;
            }
            return test;
        }
    }
}
