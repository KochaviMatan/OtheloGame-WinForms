using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05_Othelo
{
    public enum eTeam
    {
        White = 'O',
        Black = 'X',
    }

    public enum eWinner
    {
        Black,
        White,
        Tie,
    }

    public enum eNumberOfRounds
    {
        Round = 3,
        WinsNeededToWin = 2
    }
}
