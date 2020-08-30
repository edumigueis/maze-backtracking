using maze_backtracking.Classes;
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Threading;

namespace maze_backtracking
{
    public partial class Form1 : Form
    {
        private Labirinto labirinto;

        public Form1()
        {
            InitializeComponent();
            Color c = new Color();
            c = Color.FromArgb(50, 50, 50);
            dgvLab.BackgroundColor = c;
            dgvLab.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvLab.ColumnHeadersVisible = false;
            dgvLab.RowHeadersVisible = false;
            dgvLab.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLab.RowsDefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*do
            {
                int[,] atual = labirinto.EncontrarProximaPosicao();

                if (atual[0, 0] == -1)
                    labirinto.Voltar();
                else 
                {
                    Color cor = new Color();
                    cor = Color.FromArgb(80, 0, 200);
                    dgvLab.Rows[atual[0,0]].Cells[atual[0,1]].Style.BackColor = cor;
                    Thread.Sleep(500);
                }
            }
            while (!labirinto.EstaNoFim);*/

            List<PilhaLista<Movimento>> lista = new List<PilhaLista<Movimento>>();
            lista = labirinto.Resolver();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < dgvLab.Columns.Count; i++)
            {
                try
                {
                    dgvLab.Columns.Clear();
                    dgvLab.Columns.RemoveAt(i);
                }
                catch (Exception) { }
            }

            if (dlgAbrirArquivo.ShowDialog() == DialogResult.OK)
            {
                string nomeArq = dlgAbrirArquivo.FileName;
                LeitorDeArquivo leitor = new LeitorDeArquivo();
                try
                {
                    labirinto = new Labirinto(leitor.ReadFileAsCharTable(nomeArq));
                    for(int i = 0; i<labirinto.Matriz.GetLength(1); i++)
                    {
                        dgvLab.Columns.Add("Column" + i, "");
                        dgvLab.Columns[i].Width = 20;
                    }

                    for(int i = 0; i < labirinto.Matriz.GetLength(0); i++)
                    {
                        dgvLab.Rows.Add();
                        for (int j = 0; j < labirinto.Matriz.GetLength(1); j++)
                        {
                            if (labirinto.Matriz[i, j].Equals('#'))
                            {
                                Color c = new Color();
                                c = Color.FromArgb(50, 50, 50);
                                dgvLab.Rows[i].Cells[j].Style.BackColor = c;
                            }
                            else if (labirinto.Matriz[i, j].Equals('I'))
                            {
                                Color c = new Color();
                                c = Color.FromArgb(204, 25, 34);
                                dgvLab.Rows[i].Cells[j].Style.BackColor = c;
                            }
                            else if (labirinto.Matriz[i, j].Equals('S'))
                            {
                                Color c = new Color();
                                c = Color.FromArgb(17, 197, 74);
                                dgvLab.Rows[i].Cells[j].Style.BackColor = c;
                            }
                            else
                            {
                                Color c = new Color();
                                c = Color.FromArgb(220, 220, 220);
                                dgvLab.Rows[i].Cells[j].Style.BackColor = c;
                            }
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Escolha um arquivo, por favor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("O arquivo não é de texto (.txt)!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ops! Algum erro inesperado aconteceu. Tente novamente mais tarde.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
