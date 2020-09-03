using maze_backtracking.Classes;
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Linq;

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
            dgvRun.BackgroundColor = c;
            dgvLab.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvRun.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvLab.ColumnHeadersVisible = false;
            dgvLab.RowHeadersVisible = false;
            dgvLab.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLab.RowsDefaultCellStyle.SelectionBackColor = Color.Transparent;
            dgvRun.RowsDefaultCellStyle.SelectionBackColor = Color.Transparent;
            dgvRun.RowHeadersVisible = false;
            dgvRun.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            dgvRun.DefaultCellStyle.BackColor = c;
            dgvRun.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            dgvRun.RowsDefaultCellStyle.BackColor = c;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button1.Enabled = false;
            dgvRun.Rows.Clear();
            dgvRun.Refresh();    
            var lista = labirinto.GetResultado();
            int i3 = 1;
            if (lista.Count != 0)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    var cor = CalcularCor();
                    var result = lista[i].ToList();

                    dgvRun.Rows.Add();
                    dgvRun.Rows[dgvRun.Rows.Count - 1].Cells[0].Value = "Saindo de (" + (labirinto.Inicio[0, 0] - 1) + "," + (labirinto.Inicio[0, 1] - 1) + ")";
                    for (int i2 = result.Count - 1; i2 >= 0; i2--)
                    {
                        dgvLab.Rows[result[i2].Coordenada[0, 0]].Cells[result[i2].Coordenada[0, 1]].Style.BackColor = cor;
                        dgvRun.Rows.Add();
                        dgvRun.Rows[i3].Cells[0].Value = "Passando em (" + (result[i2].Coordenada[0, 0] - 1) + "," + (result[i2].Coordenada[0, 1] - 1) + ")";
                        i3++;
                        Application.DoEvents();
                        Thread.Sleep(200);
                    }
                    dgvRun.Rows.Add();
                    dgvRun.Rows[i3].Cells[0].Value = "Chegando em (" + (labirinto.Fim[0, 0] - 1) + "," + (labirinto.Fim[0, 1] - 1) + ")";
                    i3++;
                }
            }
            else
            {
                dgvRun.Rows.Add();
                dgvRun.Rows[0].Cells[0].Value = "O labirinto não tem solução!";
            }
            button1.Enabled = true;
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
                button2.Enabled = true;
                string nomeArq = dlgAbrirArquivo.FileName;
                LeitorDeArquivo leitor = new LeitorDeArquivo();
                try
                {
                    labirinto = new Labirinto(leitor.ReadFileAsCharTable(nomeArq));
                    for (int i = 0; i < labirinto.Matriz.GetLength(1); i++)
                    {
                        dgvLab.Columns.Add("Column" + i, "");
                        dgvLab.Columns[i].Width = 20;
                    }

                    for (int i = 0; i < labirinto.Matriz.GetLength(0); i++)
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

        private Color CalcularCor()
        {
            var rnd = new Random();
            Color randomColor = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
            return randomColor;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
