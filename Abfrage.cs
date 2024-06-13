using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace _1305
{
    public class Abfrage
    {
        private static Random random = new Random();
        private int[] _mistakes = new int[6];
        private int _index = 0;
        public int score;
        private string? _sprache;
        private string? _abfrage;
        private string? _test;
        private string _ausgabe;

        public Abfrage(string sprache, string abfrage, string test)
        {
            _sprache = sprache;
            _abfrage = abfrage;
            _test = test;

            if(_test == "Deutsch")
            {
                _ausgabe = "deutsche";
            }else if(_test == "Französisch")
            {
                _ausgabe = "französische";
            }else if(_test == "Englisch")
            {
                _ausgabe = "englische";
            }

            Test();
            Mistakes();
        }

        public void Test()
        {
            for (int i = 0; i <= 5; i++)
            {
                string antwort;
                MongoClient dbClient = new MongoClient(("mongodb://myUserAdmin:123@localhost:27017/"));
                var database = dbClient.GetDatabase("1305");
                var collection = database.GetCollection<BsonDocument>(_sprache);
                int randomnumber = Randomnumber();
                var filter = Builders<BsonDocument>.Filter.Eq("Number", randomnumber);
                var document = collection.Find(filter).FirstOrDefault();
                string? ausgabe = (string?)document?[_abfrage];
                Console.WriteLine($"Was ist das " + _ausgabe + " Wort von " + ausgabe);
                antwort = Console.ReadLine();
                if (antwort == (string?)document?[_test])
                {
                    score++;
                    Console.WriteLine(score);
                }
                else
                {
                    _mistakes[_index] = randomnumber;
                    _index++;

                }


            }

        }

        public void Mistakes()
        {
            Console.WriteLine("Das sind die richtigen Antworten zu den Worten die du falsch hattest.");
            for (int i = 0; i <_mistakes.Length; i++)
            {
                MongoClient dbClient = new MongoClient(("mongodb://myUserAdmin:123@localhost:27017/"));
                var database = dbClient.GetDatabase("1305");
                var collection = database.GetCollection<BsonDocument>(_sprache);
                var filter = Builders<BsonDocument>.Filter.Eq("Number", _mistakes[i]);
                var document = collection.Find(filter).FirstOrDefault();
                string? fehler = (string?)document?[_abfrage];
                string? richtig = (string?)document?[_test];
                Console.WriteLine(fehler + ": " + richtig);
            }
        }
        public static int Randomnumber()
        {
            return random.Next(36);
        }
    }
}
