using maze_backtracking.Classes;
using System;
using System.IO;
using System.Windows.Forms;

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
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dlgAbrirArquivo.ShowDialog() == DialogResult.OK)
            {
                string nomeArq = dlgAbrirArquivo.FileName;
                LeitorDeArquivo leitor = new LeitorDeArquivo();
                try
                {
                    labirinto = new Labirinto(leitor.readFileAsCharTable(nomeArq));
                }
                catch (FileNotFoundException ex)
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
