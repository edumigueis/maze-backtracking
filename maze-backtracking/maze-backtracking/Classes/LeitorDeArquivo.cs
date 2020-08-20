using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze_backtracking.Classes
{
    class LeitorDeArquivo
    {
        private char[,] matriz;
        private char[,] readFileAsCharTable(string nomeArquivo)
        {
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
