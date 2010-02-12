using System;
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
            return output;
        }
    }

    public class DFS : AgentInterface
    {

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
