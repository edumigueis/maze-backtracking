using System;
using System.Collections.Generic;

namespace maze_backtracking.Classes
{
    class ListaSimples<Dado> where Dado : IComparable<Dado>
    {
        NoLista<Dado> primeiro, ultimo, atual, anterior;
        int quantosNos;
        bool primeiroAcessoDoPercurso;

        public Dado valorDe(int i)
        {
            return default(Dado);
        }
        public ListaSimples()
        {
            primeiro = ultimo = anterior = atual = null;
            quantosNos = 0;
            primeiroAcessoDoPercurso = false;
        }

        public void PercorrerLista()
        {
            atual = primeiro;
            while (atual != null)
            {
                Console.WriteLine(atual.Info);
                atual = atual.Prox;
            }
        }

        public void InserirAntesDoInicio(NoLista<Dado> novoNo)
        {
            if (EstaVazia)       // se a lista está vazia, estamos
                ultimo = novoNo; // incluindo o 1o e o último nós!

            novoNo.Prox = primeiro; // faz o novo nó apontar para o nó
            primeiro = novoNo;      // atualmente no início da lista
            quantosNos++;           // (que pode ser null)
        }

        public void InserirAposFim(NoLista<Dado> novoNo)
        {
            if (EstaVazia)
                primeiro = novoNo;
            else
                ultimo.Prox = novoNo;

            quantosNos++;
            ultimo = novoNo;
            ultimo.Prox = null; // garantimos final lógico da lista
        }

        public bool ExisteDado(Dado outroProcurado)
        {
            anterior = null;
            atual = primeiro;

            if (EstaVazia)
                return false;

            if (outroProcurado.CompareTo(primeiro.Info) < 0)
                return false;

            if (outroProcurado.CompareTo(ultimo.Info) > 0)
            {
                anterior = ultimo;
                atual = null;
                return false;
            }

            bool achou = false;
            bool fim = false;

            while (!achou && !fim)

                if (atual == null)
                    fim = true;
                else
                  if (outroProcurado.CompareTo(atual.Info) == 0)
                    achou = true;
                else
                    if (atual.Info.CompareTo(outroProcurado) > 0)
                    fim = true;
                else
                {
                    anterior = atual;
                    atual = atual.Prox;
                }
            return achou;  
        }           

        public void InserirEmOrdem(Dado dados)
        {
            if (!ExisteDado(dados)) // existeChave configura anterior e atual
            {                       // aqui temos certeza de que a chave não existe
                                    // guarda dados no novo nó
                var novo = new NoLista<Dado>(dados, null);
                if (EstaVazia)      // se a lista está vazia, então o 	
                    InserirAntesDoInicio(novo);  // novo nó é o primeiro da lista
                else
                    // testa se nova chave < primeira chave
                    if (anterior == null && atual != null)
                    InserirAntesDoInicio(novo); // liga novo antes do primeiro
                else
                        // testa se nova chave > última chave
                        if (anterior != null && atual == null)
                    InserirAposFim(novo);
                else
                    InserirNoMeio(novo);  // insere entre os nós anterior e atual
            }
        }

        private void InserirNoMeio(NoLista<Dado> novo)
        {
            // existeDado() encontrou intervalo de inclusão do novo nó

            anterior.Prox = novo;   // liga anterior ao novo
            novo.Prox = atual;      // e novo no atual

            if (anterior == ultimo)  // se incluiu ao final da lista,
                ultimo = novo;       // atualiza o apontador ultimo
            quantosNos++;            // incrementa número de nós da lista     	}								 
        }
        public bool removerDado(Dado dados)
        {
            if (ExisteDado(dados))
            {
                // existeDado posicionou atual e anterior
                RemoverNo(ref atual, ref anterior);
                return true;
            }

            return false;
        }

        public void RemoverNo(ref NoLista<Dado> atual, ref NoLista<Dado> anterior)
        {
            if (!EstaVazia)
            {
                if (atual == primeiro)
                {
                    primeiro = primeiro.Prox;
                    if (EstaVazia)
                        ultimo = null;
                }
                else
                    if (atual == ultimo)
                    {
                        ultimo = anterior;
                        ultimo.Prox = null;
                    }
                    else
                        anterior.Prox = atual.Prox;

                quantosNos--;
            }
        }

        private void IniciarPercursoSequencial()
        {
            primeiroAcessoDoPercurso = true;
            atual = primeiro;
            anterior = null;
        }

        private bool PodePercorrer()
        {
            if (!primeiroAcessoDoPercurso)
            {
                anterior = atual;
                atual = atual.Prox;
            }
            else
                primeiroAcessoDoPercurso = false;

            return atual != null;
        }
        private void ProcurarMenorDado
            (ref NoLista<Dado> menorAteAgora,
                ref NoLista<Dado> anteriorAoMenor)
        {
            menorAteAgora = primeiro;
            anteriorAoMenor = null;

            IniciarPercursoSequencial();
            while (PodePercorrer())
            {
                if (atual.Info.CompareTo(menorAteAgora.Info) < 0)
                {
                    anteriorAoMenor = anterior;
                    menorAteAgora = atual;
                }
            }
        }
        public void Ordenar()
        {
            ListaSimples<Dado> ordenada = new ListaSimples<Dado>();
            NoLista<Dado> menorDeTodos = null,
                        antesDoMenor = null;

            while (!this.EstaVazia)
            {
                ProcurarMenorDado(ref menorDeTodos, ref antesDoMenor);

                NoLista<Dado> novoNo = menorDeTodos;
                this.RemoverNo(ref menorDeTodos, ref antesDoMenor);

                ordenada.InserirAposFim(novoNo);

            }

        }
        public bool EstaVazia
        {
            get
            {
                return (primeiro == null);
            }
        }

        public NoLista<Dado> Primeiro
        {
            get
            {
                return primeiro;
            }
            set
            {
                primeiro = value;
            }
        }

        public NoLista<Dado> Atual
        {
            get
            {
                return atual;
            }
        }
        public NoLista<Dado> Ultimo
        {
            get
            {
                return ultimo;
            }
        }

        public int QuantosNos
        {
            get
            {
                return quantosNos;
            }
        }

        // exercício 1
        public int ContagemDeNos()
        {
            int quantos = 0;
            atual = primeiro;
            while (atual != null)
            {
                quantos++;
                atual = atual.Prox;
            }
            return quantos;
        }

        // exercicio 3

        public void CasamentoCom(ListaSimples<Dado> outra,
                                    ref ListaSimples<Dado> nova)
        {
            nova = new ListaSimples<Dado>();
            NoLista<Dado> a = null,
                        b = null;
            while (!this.EstaVazia && !outra.EstaVazia)
            {
                a = this.primeiro;
                b = outra.primeiro;

                if (a.Info.CompareTo(b.Info) < 0)
                {
                    this.quantosNos--;
                    this.primeiro = this.primeiro.Prox; // avança na lista 1
                    nova.InserirAposFim(a);
                }
                else
                    if (b.Info.CompareTo(a.Info) < 0)
                {
                    outra.quantosNos--;
                    outra.primeiro = outra.primeiro.Prox; // avança na lista 2
                    nova.InserirAposFim(b);
                }
                else
                {
                    this.quantosNos--;
                    outra.quantosNos--;
                    this.primeiro = this.primeiro.Prox; // avança na lista 1
                    outra.primeiro = outra.primeiro.Prox; // avança na lista 2
                    nova.InserirAposFim(a);
                }
            }
            if (!this.EstaVazia)  // não acabou a lista 1
            {
                nova.ultimo.Prox = this.primeiro;
                nova.ultimo = this.ultimo;
                nova.quantosNos += this.quantosNos;
            }

            if (!outra.EstaVazia)
            {
                nova.ultimo.Prox = outra.primeiro;
                nova.ultimo = outra.ultimo;
                nova.quantosNos += outra.quantosNos;
            }

            this.primeiro = this.ultimo = null;
            this.quantosNos = 0;

            outra = new ListaSimples<Dado>();
        }

        // exercício 4
        public void Inverter()
        {
            if (quantosNos > 1)
            {
                NoLista<Dado> um = primeiro;
                ultimo = primeiro;
                NoLista<Dado> dois = primeiro.Prox;
                NoLista<Dado> tres = null;
                while (dois != null)
                {
                    tres = dois.Prox;
                    dois.Prox = um;
                    um = dois;
                    dois = tres;
                }
                primeiro = um;
                ultimo.Prox = null;
            }
        }

        public List<Dado> ToList()
        {
            atual = primeiro;
            List<Dado> lista = new List<Dado>();
            while (atual != null)
            {
                lista.Add(atual.Info);
                atual = atual.Prox;
            }
            return lista;
        }
    }
}
