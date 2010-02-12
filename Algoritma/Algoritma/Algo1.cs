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

            output.Add(ActionManager.Attack(0, 0, 1));

            Console.WriteLine("ini BFS");
            return new List<ElemenAksi>();
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
            Console.WriteLine("ini DFS");
            return new List<ElemenAksi>();
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
            Console.WriteLine("ini UCS");
            return new List<ElemenAksi>();
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
            Console.WriteLine("ini Greedy");
            return new List<ElemenAksi>();
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
            Console.WriteLine("ini Astar");
            return null;
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
            Console.WriteLine("ini CSP");
            return new List<ElemenAksi>();
        }
    }

}
