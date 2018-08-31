using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUMPFutsalTournament.Domain.Implementations
{
    public static  class LiveMatchService
    {
        public static int CurrentActiveMatchMinute { get; set; } = 0;
        public static int CurrentActiveMatchSecond { get; set; } = 0;
    }
}
