using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze_backtracking.Classes
{
    public class Movimento : IComparable<Movimento>
    {
            public int Direcao { get; set; }

            public int[,] Coordenada { get; set; }

            public Movimento(int direcao, int[,] coordenada)
            {
                Direcao = direcao;
                Coordenada = coordenada;
            }

            public int CompareTo(Movimento m)
            {
                return 0;
            }
    }
}
