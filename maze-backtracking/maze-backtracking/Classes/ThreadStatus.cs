using System.Threading;
using System.Windows.Forms;

namespace maze_backtracking.Classes
{
    public class ThreadStatus
    {
        public static void ExibirStatus(DataGridView dgv)
        {
            for (; ; )
            {
                dgv.Columns[0].HeaderText = "Status (.)";
                Thread.Sleep(300);
                dgv.Columns[0].HeaderText = "Status (..)";
                Thread.Sleep(300);
                dgv.Columns[0].HeaderText = "Status (...)";
                Thread.Sleep(300);
                dgv.Columns[0].HeaderText = "Status (....)";
                Thread.Sleep(300);
            }
        }
    }
}
