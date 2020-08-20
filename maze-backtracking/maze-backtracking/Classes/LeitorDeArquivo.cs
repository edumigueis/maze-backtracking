using System;
using System.IO;

namespace maze_backtracking.Classes
{
    class LeitorDeArquivo
    {
        private char[,] matriz;

        public LeitorDeArquivo()
        {}

        public char[,] ReadFileAsCharTable(string nomeArquivo)
        {
            if (nomeArquivo.Equals(""))
                throw new FileNotFoundException("O nome do arquivo não foi fornecido.");
            if(!Path.GetExtension(nomeArquivo).Equals(".txt"))
                throw new Exception("O arquivo fornecido não é .txt!");

            StreamReader sr = new StreamReader(nomeArquivo);
            
            int colunas = int.Parse(sr.ReadLine());
            int linhas = int.Parse(sr.ReadLine());

            matriz = new char[linhas, colunas];

            for (int i = 0; i < linhas; i++)
            {
                string linhaArquivo = sr.ReadLine();
                for (int j = 0; j < colunas; j++)
                {

                    matriz[i, j] = linhaArquivo[j];
                }
            }
            return matriz;
        }
    }
}
