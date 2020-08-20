using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze_backtracking.Classes
{
    public class NoLista<Dado> where Dado : IComparable<Dado>
    {
        private Dado info;
        private NoLista<Dado> prox;

        public Dado Info
        {
            get { return info; }

            set
            {
                if (value != null)
                    info = value;
            }
        }

        public NoLista<Dado> Prox
        {
            get
            {
                return prox;
            }

            set
            {
                prox = value;
            }
        }

        public NoLista(Dado novaInfo, NoLista<Dado> proximo)
        {
            Info = novaInfo;
            prox = proximo;
        }

    }
}
