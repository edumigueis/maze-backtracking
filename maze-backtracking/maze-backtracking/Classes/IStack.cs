using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze_backtracking.Classes
{

    interface IStack<Dado>
    {
        int Tamanho { get; }
        bool EstaVazia { get; }

        void Empilhar(Dado elemento);

        Dado Desempilhar();

        Dado OTopo();

    }
}
