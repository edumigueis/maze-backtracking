using System;
using System.Collections.Generic;

namespace maze_backtracking.Classes
{
    class Labirinto
    {
        public int[,] Inicio { get; set; }

        public int[,] Fim { get; set; }

        public char[,] Matriz { get; set; }

        private char[,] MatrizOriginal { get; set; }

        private bool todosOsCaminhosEncontrados = false;

        private int[,] Direcoes { get; } = { { 0, -1, 0 },
                                            { 1, -1, 1 },
                                            { 2, 0, 1 },
                                            { 3, 1, 1 },
                                            { 4, 1, 0 },
                                            { 5, 1, -1 },
                                            { 6, 0, -1 },
                                            { 7, -1, -1 } };

        private PilhaLista<Movimento> Movimentos { get; set; }
        private List<PilhaLista<Movimento>> CaminhosEncontrados { get; set; }

        public Labirinto(char[,] matriz)
        {
            AdicionarParedes(matriz);
            EncontrarInicio();
            EncontrarFim();
            MatrizOriginal = (char[,])Matriz.Clone();
            CaminhosEncontrados = new List<PilhaLista<Movimento>>();
        }

        private void Resolver()
        {
            Movimentos = new PilhaLista<Movimento>();
            int novoI;
            int novoJ;
            int atualJ = 0;
            int atualI = 0;
            bool moveuSe = false;

            for (int d = 0; d < Direcoes.GetLength(0); d++)
            {
                moveuSe = false;

                if (!Movimentos.EstaVazia)
                {
                    atualI = Movimentos.OTopo().Coordenada[0, 0];
                    atualJ = Movimentos.OTopo().Coordenada[0, 1];
                    novoI = atualI + Direcoes[d, 1];
                    novoJ = atualJ + Direcoes[d, 2];
                }
                else
                {
                    novoI = Inicio[0, 0] + Direcoes[d, 1];
                    novoJ = Inicio[0, 1] + Direcoes[d, 2];
                }

                if (Matriz[novoI, novoJ].ToString().Equals(" "))
                {
                    if (!Movimentos.EstaVazia)
                    {
                        Mover(novoI, novoJ, ref atualI, ref atualJ, d);
                        moveuSe = true;
                        d = -1;
                    }
                    else
                    {
                        atualI = Inicio[0, 0];
                        atualJ = Inicio[0, 1];
                        Mover(novoI, novoJ, ref atualI, ref atualJ, d);
                        moveuSe = true;
                        d = -1;
                    }
                }
                else
                    if (Matriz[novoI, novoJ].ToString().Equals("S"))
                {
                    SalvarCaminho(novoI, novoJ);
                    break;
                }
                else if (d == 7 && moveuSe == false)
                {
                    if (Movimentos.EstaVazia)
                    {
                        todosOsCaminhosEncontrados = true;
                        return;
                    }

                    Movimento atual = Movimentos.Desempilhar();

                    if (!Movimentos.EstaVazia)
                    {
                        Movimento ant = Movimentos.OTopo();
                        atualI = ant.Coordenada[0, 0];
                        atualJ = ant.Coordenada[0, 1];
                        d = -1;
                    }

                }
            }
        }

        private void FecharCaminhoEncontrado()
        {
            if (Movimentos.EstaVazia)
            {
                todosOsCaminhosEncontrados = true;
                return;
            }

            int i = Movimentos.OTopo().Coordenada[0, 0];
            int j = Movimentos.OTopo().Coordenada[0, 1];
            List<int[,]> lista = new List<int[,]>();

            for (int i2 = 0; i2 < Matriz.GetLength(0); i2++)
            {
                for (int j2 = 0; j2 < Matriz.GetLength(1); j2++)
                {
                    if (Matriz[i2, j2].ToString().Equals("O"))
                    {
                        int[,] arr = new int[,] { { i2, j2 } };
                        lista.Add(arr);
                    }
                }
            }

            Matriz = (char[,])MatrizOriginal.Clone();
            Matriz[i, j] = 'O';

            for (int i3 = 0; i3 < lista.Count; i3++)
            {
                Matriz[lista[i3][0, 0], lista[i3][0, 1]] = 'O';
            }

        }

        public List<PilhaLista<Movimento>> GetResultado()
        {
            do
            {
                Resolver();
                FecharCaminhoEncontrado();
            }
            while (!todosOsCaminhosEncontrados);

            return CaminhosEncontrados;
        }

        private void Mover(int linhaProx, int colProx, ref int linha, ref int col, int direcao)
        {
            linha = linhaProx;
            col = colProx;
            var movimento = new Movimento(direcao, new int[,] { { linha, col } });
            Movimentos.Empilhar(movimento);
            Matriz[linha, col] = 'X';
        }

        private void SalvarCaminho(int linha, int col)
        {
            Movimentos.Empilhar(new Movimento(0, new int[,] { { linha, col } }));
            CaminhosEncontrados.Add(Movimentos);
            Movimentos.Desempilhar();
        }
        private void EncontrarInicio()
        {
            for (int i = 0; i < Matriz.GetLength(0); i++)
            {
                for (int j = 0; j < Matriz.GetLength(1); j++)
                {
                    if (Matriz[i, j].ToString().ToUpper().Equals("I"))
                        Inicio = new int[,] { { i, j } };
                }
            }
        }

        private void EncontrarFim()
        {
            for (int i = 0; i < Matriz.GetLength(0); i++)
            {
                for (int j = 0; j < Matriz.GetLength(1); j++)
                {
                    if (Matriz[i, j].ToString().ToUpper().Equals("S"))
                        Fim = new int[,] { { i, j } };
                }
            }
        }

        private void AdicionarParedes(char[,] matriz)
        {
            char[,] novaMatriz = new char[matriz.GetLength(0) + 2, matriz.GetLength(1) + 2];

            for (int i = 0; i < novaMatriz.GetLength(0); i++)
            {
                for (int j = 0; j < novaMatriz.GetLength(1); j++)
                {
                    if (i == 0 || i == novaMatriz.GetLength(0) - 1 ||
                        j == 0 || j == novaMatriz.GetLength(1) - 1)
                    {
                        novaMatriz[i, j] = '#';
                    }
                    else
                    {
                        novaMatriz[i, j] = matriz[i - 1, j - 1];
                    }
                }
            }

            Matriz = novaMatriz;
        }
    }
}
