using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    public class DatabaseManager
    {
        private List<User> usersScores { get; }
        private string path;
        public DatabaseManager()
        {
            path = Directory.GetCurrentDirectory();
            path += "\\database.txt";
            usersScores = new List<User>();

        }
        public User this[int index]
        {
            get => usersScores[index];
        }
        public void ReadFromDatabase()
        {
            if (!File.Exists(path))
            {
                StreamWriter sw = File.CreateText(path);
                SetPlaceholders();
            }
            else
            {
                ReadDatabase();
                usersScores.Sort(CompareUsersByScore);
                SetPlaceholders();
            }
        }
        public void UploadDatabase(string nick, int score)
        {
            File.AppendAllText(path, nick + " " + score + "\n");
            usersScores.Add(new User(nick, score));
            usersScores.Sort(CompareUsersByScore);
        }
        private void ReadDatabase()
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                string[] splittedStrings;
                string nick;
                int score;
                while ((line = sr.ReadLine()) != null)
                {
                    splittedStrings = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    nick = splittedStrings[0];
                    score = Convert.ToInt32(splittedStrings[1]);
                    usersScores.Add(new User(nick, score));
                }
            }
        }
        private void SetPlaceholders()
        {
            int index = usersScores.Count() - 1;
            for (int i = index; i < 5; i++)
            {
                usersScores.Add(new User("Not ranked yet", 0));
            }
        }
        private int CompareUsersByScore(User x, User y)
        {
            if (x.score < y.score)
                return 1;
            else if (x.score > y.score)
                return -1;
            else return 0;
        }
    }
}
