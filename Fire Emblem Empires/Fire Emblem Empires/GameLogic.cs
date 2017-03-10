using Fire_Emblem_Empires.Unit_Creation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires
{
    public class GameLogic
    {
        public static Unit[] p1_roster = new Unit[] { new Fighter(Team.BLUE), new Healer(Team.BLUE), new Mage(Team.BLUE), new Mercenary(Team.BLUE), new Soldier(Team.BLUE) };
        public static Unit[] p2_roster = new Unit[] { new Fighter(Team.RED), new Healer(Team.RED), new Mage(Team.RED), new Mercenary(Team.RED), new Soldier(Team.RED) };
        static bool p1Turn = true;

        Player p1 = new Player(p1_roster, 5, Team.BLUE);
        Player p2 = new Player(p2_roster, 5, Team.RED);

        public void changeTurn()
        {
            p1Turn = !p1Turn;
        }

        public void runGame()
        {
            bool noWinner = true;
            bool p1Wins = false;
            while (noWinner)
            {
                if (p1Turn)
                {
                    takeTurn(p1);
                    if (p2.m_unitCount == 0)
                    {
                        noWinner = false;
                        p1Wins = true;
                    }
                }
                else
                {
                    takeTurn(p2);
                    if (p2.m_unitCount == 0)
                    {
                        noWinner = false;
                    }
                }
            }
            if (p1Wins)
            {
                Console.WriteLine("Player 1 Wins");
            }
            else
            {
                Console.WriteLine("Player 2 Wins");
            }
        }

        private void takeTurn(Player p)
        {
            throw new NotImplementedException();
        }
    }
}
