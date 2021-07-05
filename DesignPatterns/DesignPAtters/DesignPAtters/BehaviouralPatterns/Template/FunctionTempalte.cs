using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns.BehaviouralPatterns.Template
{
    public static class GameTemplate
    {
        public static void Run(
          Action start,
          Action takeTurn,
          Func<bool> haveWinner,
          Func<int> winningPlayer)
        {
            start();
            while (!haveWinner())
                takeTurn();
            WriteLine($"Player {winningPlayer()} wins.");
        }
    }
}
