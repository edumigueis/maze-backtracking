using System;

namespace maze_backtracking.Classes
{

    class PilhaLista<Dado> : ListaSimples<Dado>, IStack<Dado>, IComparable<PilhaLista<Dado>>
                            where Dado : IComparable<Dado>
    {
        public Dado Desempilhar()
        {
            if (EstaVazia)
                throw new Exception("pilha vazia!");

            Dado valor = base.Primeiro.Info;

            NoLista<Dado> pri = base.Primeiro;
            NoLista<Dado> ant = null;
            base.RemoverNo(ref pri, ref ant);
            return valor;
        }

        public void Empilhar(Dado elemento)
        {
            base.InserirAntesDoInicio
                (
                    new NoLista<Dado>(elemento, null)
                );
        }

        new public bool EstaVazia
        {
            get => base.EstaVazia;
        }

        public Dado OTopo()
        {
            if (EstaVazia)
                throw new Exception("pilha vazia de cu!");

            return base.Primeiro.Info;
        }

        public int Tamanho { get => base.QuantosNos; }

        public int CompareTo(PilhaLista<Dado> other)
        {
            throw new NotImplementedException();
        }
    }
}
