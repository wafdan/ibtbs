﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TubesAI.Model;

namespace Algoritma
{
    public class BFS : AgentInterface
    {
        public BFS()
        {
        }

        public bool firstTurn
        {
            get
            {
                return firstTurn;
            }

            set
            {
                firstTurn = value;
            }
        }

        public List<ElemenAksi> Execute(Team myTeam, Team enTeam)
        {
            List<ElemenAksi> output = new List<ElemenAksi>(11);
            foreach (var unit in myTeam.listUnit)
            {
                Unit at_unit = enTeam.listUnit.First();
                foreach (var enemy_unit in enTeam.listUnit)
                {
                    if (at_unit.isDead())
                        at_unit = enemy_unit;
                    if (at_unit.getCurrentHP() > enemy_unit.getCurrentHP() && !enemy_unit.isDead())
                        at_unit = enemy_unit;
                }
                output.Add(unit.Attack(at_unit.index, enTeam));
                //else output.Add(unit.useItem(2, myTeam, Item.potion));
            }
            return output;
        }
    }

    public class DFS : AgentInterface
    {
        public DFS()
        {
        }

        public bool firstTurn
        {
            get
            {
                return firstTurn;
            }

            set
            {
                firstTurn = value;
            }
        }

        public List<ElemenAksi> Execute(Team myTeam, Team enTeam)
        {
            List<ElemenAksi> output = new List<ElemenAksi>(11);
            return output;
        }
    }

    public class UCS : AgentInterface
    {
        public UCS()
        {
        }

        public bool firstTurn
        {
            get
            {
                return firstTurn;
            }

            set
            {
                firstTurn = value;
            }
        }

        public List<ElemenAksi> Execute(Team myTeam, Team enTeam)
        {
            List<ElemenAksi> output = new List<ElemenAksi>(11);
            return output;
        }
    }

    public class Greedy : AgentInterface
    {
        public Greedy()
        {
        }

        public bool firstTurn
        {
            get
            {
                return firstTurn;
            }

            set
            {
                firstTurn = value;
            }
        }

        public List<ElemenAksi> Execute(Team myTeam, Team enTeam)
        {
            List<ElemenAksi> output = new List<ElemenAksi>(11);
            return output;
        }
    }

    public class Astar : AgentInterface
    {
        public Astar()
        { 
        }

        public bool firstTurn
        {
            get
            {
                return firstTurn;
            }

            set
            {
                firstTurn = value;
            }
        }

        public List<ElemenAksi> Execute(Team myTeam, Team enTeam)
        {
            List<ElemenAksi> output = new List<ElemenAksi>(11);
            return output;
        }
    }

    public class CSP : AgentInterface
    {
        public CSP()
        {
        }

        public bool firstTurn
        {
            get
            {
                return firstTurn;
            }
            set
            {
                firstTurn = value;
            }
        }
                
        public List<ElemenAksi> Execute(Team myTeam, Team enTeam)
        {
            List<ElemenAksi> output = new List<ElemenAksi>(11);
            return output;
        }
    }

}
