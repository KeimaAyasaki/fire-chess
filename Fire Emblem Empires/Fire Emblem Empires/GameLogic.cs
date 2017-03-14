using Fire_Emblem_Empires.Board_Creation;
using Fire_Emblem_Empires.Unit_Creation;
using Fire_Emblem_Empires.Battle_Management;
using Fire_Emblem_Empires.Time_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires
{
    public class GameLogic
    {
        Player p1;
        Player p2;
        Player p3;
        static bool p1Turn = true;
        public Board m_board { get; private set; }
        public BattleManager m_battleManager { get; private set; }
        public int turnCount = 0;
        public GameLogic(ref Board board)
        {
            m_board = board;
            m_battleManager = new BattleManager();
            TimeManager.Start();
            ExamineBoardForTeams();
        }

        //Changes the turn bool to the opposite of what it is assigned as. 
        public void ChangeTurn()
        {
            p1Turn = !p1Turn;
        }

        public void ExamineBoardForTeams()
        {
            byte redCount = 0;
            List<Unit> red_roster = new List<Unit>();
            byte blueCount = 0;
            List<Unit> blue_roster = new List<Unit>();
            byte greenCount = 0;
            List<Unit> green_roster = new List<Unit>();

            for (byte j = 0; j < m_board.numRows; ++j)
            {
                for(byte k = 0; k < m_board.numColumns; ++k)
                {
                    if(m_board.GetSpace(j, k).m_unit != null)
                    {
                        if(m_board.GetSpace(j, k).m_unit.GetTeamColor() == Team.BLUE)
                        {
                            ++blueCount;
                            blue_roster.Add(m_board.GetSpace(j, k).m_unit);
                        }
                        else if (m_board.GetSpace(j, k).m_unit.GetTeamColor() == Team.RED)
                        {
                            ++redCount;
                            red_roster.Add(m_board.GetSpace(j, k).m_unit);
                        }
                        else if(m_board.GetSpace(j, k).m_unit.GetTeamColor() == Team.GREEN)
                        {
                            ++greenCount;
                            green_roster.Add(m_board.GetSpace(j, k).m_unit);
                        }
                    }
                }
            }
            p1 = new Player(Team.BLUE, blue_roster);
            p2 = new Player(Team.RED, red_roster);
            p3 = new Player(Team.GREEN, green_roster);
            if(p1.CanMoveUnits() && p2.CanMoveUnits() && p3.CanMoveUnits())
            {
                Console.WriteLine("Unimplemented mass multiplayer. Expect Crashing");
                Console.WriteLine("Expected only Red and Blue units");
            }
        }

        //Handles running the game
        public void Run()
        {
            bool gameIsOver = false;
            Player winner = null;
            //Keeps running so long as no winner is decided
            while (!gameIsOver)
            {
                //Checks if it's player 1 turn. 
                if (p1Turn)
                {
                    TakeTurn(p1);
                    if (p2.m_unitCount == 0)
                    {
                        gameIsOver = true;
                        winner = p1;
                    }
                }
                //Checks if it's player 2 turn.
                else
                {
                    TakeTurn(p2);
                    if (p2.m_unitCount == 0)
                    {
                        gameIsOver = true;
                        winner = p2;
                    }
                }
            }
            //Handles when P1 wins
            if(winner == null)
            {
                Console.WriteLine("Game finished before a winner was determined.");
            }
            else if(winner == p1)
            {
                Console.WriteLine("Player 1 won!");
            }
            else if(winner == p2)
            {
                Console.WriteLine("Plyaer 2 won!");
            }
            TimeManager.Stop();
            Console.WriteLine("Time Elapsed = {0}", TimeManager.TimeElapsedSinceStart());
            Console.WriteLine("Turns taken = {0}", turnCount);

        }

        //Takes in a player and lets the player move their units
        private void TakeTurn(Player p)
        {
            p.CanNowMoveAllUnits();
            while (p.CanMoveUnits())
            {
                Tile currTile = SelectTile();
                bool actionTaken = false;
                bool unitSelected = true;
                // while a tile has a unit and is selected
                while (currTile != null && currTile.m_unit != null && !actionTaken && unitSelected)
                {
                    Tile destTile = SelectTile();

                    // ensures no operation is carried out on a null destination tile
                    if(destTile == null)
                    {
                        unitSelected = false;
                        continue;
                    }

                    //Checks if the distance the player wishes to move is greater than the movement the unit has
                    if (m_board.CalculateDistance(currTile, destTile) > currTile.m_unit.m_MovementRange)
                    {
                        unitSelected = false;
                        continue;
                    }
                    //Checks to see if both tiles have a unit
                    if (BothTilesHaveAUnit(currTile, destTile))
                    {
                        //if units can interact
                        if (UnitsInteract(currTile, destTile))
                        {
                            currTile.m_unit.isNowUnableToMove();
                            ++turnCount;
                            actionTaken = true;
                            unitSelected = false;
                        }
                        continue;
                    }
                    //Moving unit from starting location to destination
                    else
                    {
                        m_board.MoveUnitFromSpaceToSpace(currTile, destTile);
                        currTile.m_unit.isNowUnableToMove();
                        ++turnCount;
                        unitSelected = false;
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
