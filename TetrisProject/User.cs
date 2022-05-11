using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    public struct User
    {
        public string nickname;
        public int score { get; }
        public User (string nick, int achievedScore) 
        {
            nickname = nick;
            score = achievedScore;
        }
    }
}
