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

        //Changes the turn bool to the opposite of what it is assigned as. 
        public void changeTurn()
        {
            p1Turn = !p1Turn;
        }

        //Handles running the game
        public void runGame()
        {
            bool noWinner = true;
            bool p1Wins = false;
            //Keeps running so long as no winner is decided
            while (noWinner)
            {
                //Checks if it's player 1 turn. 
                if (p1Turn)
                {
                    TakeTurn(p1);
                    if (p2.m_unitCount == 0)
                    {
                        noWinner = false;
                        p1Wins = true;
                    }
                }
                //Checks if it's player 2 turn.
                else
                {
                    TakeTurn(p2);
                    if (p2.m_unitCount == 0)
                    {
                        noWinner = false;
                    }
                }
            }
            //Handles when P1 wins
            if (p1Wins)
            {
                Console.WriteLine("Player 1 Wins");
            }
            //Handles when P2 wins
            else
            {
                Console.WriteLine("Player 2 Wins");
            }
        }

        //Takes in a player and lets the player move their units
        private void TakeTurn(Player p)
        {
            p.CanNowMoveAllUnits();
            while (p.CanMoveUnits())
            {
                Tile currTile = SelectTile();
                bool unitOperation = false;
                // while a tile has a unit and is selected
                while (currTile != null && currTile.m_unit != null && !unitOperation)
                {
                    Tile destTile = SelectTile();

                    // ensures no operation is carried out on a null destination tile
                    if(destTile == null)
                    {
                        continue;
                    }

                    //Checks if the distance the player wishes to move is greater than the movement the unit has
                    if (m_board.CalculateDistance(currTile, destTile) > currTile.m_unit.m_MovementRange)
                    {
                        continue;
                    }
                    //Checks to see if both tiles have a unit
                    if (BothTilesHaveAUnit(currTile, destTile))
                    {
                        //if units can interact
                        if (UnitsInteract(currTile, destTile))
                        {
                            currTile.m_unit.isNowUnableToMove();
                            unitOperation = true;
                        }
                        continue;
                    }
                    //Moving unit from starting location to destination
                    else
                    {
                        m_board.MoveUnitFromSpaceToSpace(currTile, destTile);
                    }
                }
            }
        }

        //Takes in two tiles and checks to see if the units in the tile can interact
        // TODO:: Refactor
        private bool UnitsInteract(Tile currTile, Tile destTile)
        {
            bool unitsHaveInteracted = false;
            byte interactionDistance = m_board.CalculateDistance(currTile, destTile);
            //Checks Distance between Units
            if(interactionDistance == 0)
            {
                unitsHaveInteracted = true;
            }
            else if (interactionDistance == 1)
            {
                //Calculate Healing
                if (currTile.m_unit.GetJob() == Job.HEALER)
                {
                    byte amountHealed = 0;
                    if (m_battleManager.calculateHealing(currTile.m_unit, destTile.m_unit, out amountHealed))
                    {
                        destTile.m_unit.ModifyCurrentHealth(amountHealed, true);
                        unitsHaveInteracted = true;
                    }
                }
                //Calculate Damage
                if (currTile.m_unit.GetJob() == Job.FIGHTER || currTile.m_unit.GetJob() == Job.SOLDIER || currTile.m_unit.GetJob() == Job.MERCENARY || currTile.m_unit.GetJob() == Job.MAGE)
                {
                    byte damageDealt = 0;
                    if (m_battleManager.calculateDamage(currTile.m_unit, destTile.m_unit, out damageDealt))
                    {
                        destTile.m_unit.ModifyCurrentHealth(damageDealt, false);
                        unitsHaveInteracted = true;
                    }
                    // Counterattack
                    if(destTile.m_unit.GetJob() != Job.HEALER)
                    {
                        byte damageTaken = 0;
                        if(m_battleManager.calculateDamage(destTile.m_unit, currTile.m_unit, out damageTaken))
                        {
                            currTile.m_unit.ModifyCurrentHealth(damageTaken, false);
                            unitsHaveInteracted = true;
                        }    
                    }
                }
            }
            //Checks Distance Between Units
            else if (interactionDistance == 2)
            {
                //Calculate Damage
                if(currTile.m_unit.GetJob() == Job.MAGE)
                {
                    byte damageDealt = 0;
                    if(m_battleManager.calculateDamage(currTile.m_unit, destTile.m_unit, out damageDealt))
                    {
                        destTile.m_unit.ModifyCurrentHealth(damageDealt, false);
                        unitsHaveInteracted = true;
                    }
                    //Counterattack
                    if(destTile.m_unit.GetJob() == Job.MAGE)
                    {
                        byte damageTaken = 0;
                        if (m_battleManager.calculateDamage(destTile.m_unit, currTile.m_unit, out damageTaken))
                        {
                            currTile.m_unit.ModifyCurrentHealth(damageTaken, false);
                            unitsHaveInteracted = true;
                        }
                    }
                }
            }
            return unitsHaveInteracted;
        }

        //selectTile must validate Tile
        private Tile SelectTile()
        {
            // some sort of click event should be run here to get the tile
            Tile tile = new Tile();
            if (m_board.LocationIsAValidLocation(tile.m_Location))
            {
                return tile;
            }
            else
            {
                return null;
            }
        }

        //Checks if the two tiles in question have an acceptible unit
        private bool BothTilesHaveAUnit(Tile currTile, Tile destTile)
        {
            return !(currTile.m_unit == null && destTile.m_unit == null) && !(currTile.m_unit.isADefaultUnit() && destTile.m_unit.isADefaultUnit());
        }
    }
}
