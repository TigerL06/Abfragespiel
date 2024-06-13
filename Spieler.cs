using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace _1305
{
    public class Spieler
    {
        public int score;
        public string name;
        string scores = "Score";
        public Spieler() 
        {
            Console.WriteLine("Was ist ihr Benutzername ?");
            name = Console.ReadLine();
        }

        public void SpielerSpeichern()
        {
            MongoClient dbClient = new MongoClient(("mongodb://myUserAdmin:123@localhost:27017/"));
            var database = dbClient.GetDatabase("1305");
            var collection = database.GetCollection<BsonDocument>("Score");
            var spieler = new BsonDocument {{"Name", name},  {"Score", score}};
            collection.InsertOne(spieler);
        }

        public void Highscore()
        {
            MongoClient dbClient = new MongoClient(("mongodb://myUserAdmin:123@localhost:27017/"));
            var database = dbClient.GetDatabase("1305");
            var collection = database.GetCollection<BsonDocument>("Score");

            var sortDefinition = Builders<BsonDocument>.Sort.Descending("Score");
            var sortedDocuments = collection.Find(new BsonDocument()).Sort(sortDefinition).ToList();

            Console.WriteLine("Das ist die Highscoreliste:");
            foreach (var document in sortedDocuments)
            {
                string? name = (string?)document?["Name"];
                int? score = (int?)document?["Score"];
                Console.WriteLine(name + ": " + score);
            }

            SpielerScore();
        }

        public void SpielerScore()
        {
            Console.WriteLine($"Das ist ihr Score: " + name + " " + score);
            Console.ReadLine();
        }
    }
}
