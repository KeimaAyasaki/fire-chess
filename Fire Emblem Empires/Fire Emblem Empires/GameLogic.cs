using Fire_Emblem_Empires.Board_Creation;
using Fire_Emblem_Empires.Unit_Creation;
using Fire_Emblem_Empires.Battle_Management;
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
        public Board m_board { get; private set; }
        public BattleManager m_battleManager { get; private set; }

        public GameLogic(ref Board board)
        {
            m_board = board;
            m_battleManager = new BattleManager();
        }

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
            p.canNowMoveAllUnits();
            while (p.CanMoveUnits())
            {
                Tile currTile = selectTile();
                // while boolean over here for unit opreation
                Tile destTile = selectTile();
                //Checks if the distance the player wishes to move is greater than the movement the unit has
                if(m_board.CalculateDistance(currTile, destTile) > currTile.m_unit.m_MovementRange)
                {
                    continue;
                }
                if (BothTilesHaveAUnit(currTile, destTile))
                {
                    //if units can interact
                    UnitsInteract(currTile, destTile);aaaaaaaa
                        // then continue and unit operation == true
                     // continue back to unit operation boolean set earlier as a while
                }
                //Moving unit from starting location to destination
                else
                {
                    m_board.MoveUnitFromSpaceToSpace(currTile, destTile);
                }
            }
        }

        // TODO:: Refactor
        private bool UnitsInteract(Tile currTile, Tile destTile)
        {
            bool unitsHaveInteracted = false;
            byte interactionDistance = m_board.CalculateDistance(currTile, destTile);
            if (interactionDistance == 1)
            {
                if (currTile.m_unit.GetJob() == Job.MAGE)
                {
                    byte damageDealt = 0;
                    if (m_battleManager.calculateDamage(currTile.m_unit, destTile.m_unit, out damageDealt))
                    {
                        destTile.m_unit.ModifyCurrentHealth(damageDealt, false);
                        unitsHaveInteracted = true;
                    }
                }
                if (currTile.m_unit.GetJob() == Job.HEALER)
                {
                    byte amountHealed = 0;
                    if (m_battleManager.calculateHealing(currTile.m_unit, destTile.m_unit, out amountHealed))
                    {
                        destTile.m_unit.ModifyCurrentHealth(amountHealed, true);
                        unitsHaveInteracted = true;
                    }
                }
                //test  all melee
                if (currTile.m_unit.GetJob() == Job.FIGHTER || currTile.m_unit.GetJob() == Job.SOLDIER || currTile.m_unit.GetJob() == Job.MERCENARY)
                {
                    byte damageDealt = 0;
                    if (m_battleManager.calculateDamage(currTile.m_unit, destTile.m_unit, out damageDealt))
                    {
                        destTile.m_unit.ModifyCurrentHealth(damageDealt, false);
                        unitsHaveInteracted = true;
                    }
                }
            }
            //else if mages
            else if (interactionDistance == 2)
            {
                if(currTile.m_unit.GetJob == Job.MAGE)
                {
                    byte damageDealt = 0;
                    if(m_battleManager.calculateDamage(currTile.m_unit, destTile.m_unit, out damageDealt))
                    {
                        destTile.m_unit.ModifyCurrentHealth(damageDealt, false);
                        unitsHaveInteracted = true;
                    }
                }
            }
            //esle units did not interact false
            // if units interacted say the unit moved
            return unitsHaveInteracted;
        }

        //selectTile must validate Tile
    }
}
