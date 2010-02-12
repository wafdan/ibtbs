using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TubesAI.Model
{
    public interface AgentInterface
    {
        bool firstTurn { set; }
        List<ElemenAksi> Execute(Team myTeam, Team enTeam);
    }
}
