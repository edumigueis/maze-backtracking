using maze_backtracking.Classes;
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace maze_backtracking
{
    public partial class Form1 : Form
    {
        private Labirinto labirinto;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            labirinto.EncontrarCaminhos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dlgAbrirArquivo.ShowDialog() == DialogResult.OK)
            {
                string nomeArq = dlgAbrirArquivo.FileName;
                LeitorDeArquivo leitor = new LeitorDeArquivo();
                try
                {
                    labirinto = new Labirinto(leitor.ReadFileAsCharTable(nomeArq));
                    for(int i = 0; i < labirinto.Matriz.GetLength(0); i++)
                    {
                        dgvLab.Rows.Add();
                        for (int j = 0; j < labirinto.Matriz.GetLength(1); j++)
                        {
                            if(labirinto.Matriz[i, j].Equals('#'))
                            {
                                Color c = new Color();
                                c = Color.FromArgb(0, 0, 0);
                                dgvLab.Rows[i].Cells[j].Style.BackColor = c;
                            }
                            
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Escolha um arquivo, por favor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("O arquivo não é de texto (.txt)!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
