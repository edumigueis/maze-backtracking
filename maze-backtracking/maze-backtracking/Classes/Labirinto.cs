using System;

namespace maze_backtracking.Classes
{
    class Labirinto
    {
        private char[,] matriz;

        public Labirinto(char[,] matriz)
        {
            this.matriz = matriz;
        }

        public char[,] Matriz { get => matriz; set => matriz = value; }

        public void EncontrarCaminhos()
        {
            
        }
    }
}
