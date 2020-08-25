using System;

namespace maze_backtracking.Classes
{
    class Labirinto
    {
        class Movimento : IComparable<Movimento>
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

        public char[,] Matriz { get; set; }

        public bool EstaNoFim { get; set; }

        private int[,] Inicio { get; set; }

        private int[,] Fim { get; set; }

        private int[,] Direcoes { get; } = { { 0, -1, 0 },
                                            { 1, -1, 1 },
                                            { 2, 0, 1 },
                                            { 3, 1, 1 },
                                            { 4, 1, 0 },
                                            { 5, 1, -1 },
                                            { 6, 0, -1 },
                                            { 7, -1, -1 } };

        private PilhaLista<Movimento> Movimentos { get; set; }

        public Labirinto(char[,] matriz)
        {
            AdicionarParedes(matriz);
            EncontrarInicio();
            EncontrarFim();
            Movimentos = new PilhaLista<Movimento>();
        }

        public int[,] EncontrarProximaPosicao()
        {
            int novoI = -1;
            int novoJ = -1;

            for (int d = 0; d < Direcoes.GetLength(0); d++)
            {
                if (!Movimentos.EstaVazia)
                {
                    novoI = Movimentos.OTopo().Coordenada[0, 0] + Direcoes[d, 1];
                    novoJ = Movimentos.OTopo().Coordenada[0, 1] + Direcoes[d, 2];
                }
                else 
                {
                    novoI = Inicio[0,0] + Direcoes[d, 1];
                    novoJ = Inicio[0, 1] + Direcoes[d, 2];
                }

                if (Matriz[novoI, novoJ].ToString().Equals(""))
                {
                    var movimento = new Movimento(d, new int[,] {{Movimentos.OTopo().Coordenada[0, 0],
                                                             Movimentos.OTopo().Coordenada[0, 1] }});
                    Movimentos.Empilhar(movimento);
                    break;
                }                 
            }

            return new int[,] { { novoI, novoJ } };
        }

        public void Voltar()
        {
            Matriz[Movimentos.OTopo().Coordenada[0, 0],Movimentos.OTopo().Coordenada[0, 1]] = 'X';
            var movimento = Movimentos.Desempilhar();
        }

        private void EncontrarInicio()
        {
            for (int i = 0; i < Matriz.GetLength(0); i++)
            {
                for (int j = 0; j < Matriz.GetLength(1); j++)
                {
                    if (Matriz[i, j].ToString().ToUpper().Equals("I"))
                        Inicio = new int[,] { { i , j } };
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
