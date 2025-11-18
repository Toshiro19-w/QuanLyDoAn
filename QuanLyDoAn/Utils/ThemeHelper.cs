using System.Drawing;
using System.Windows.Forms;

namespace QuanLyDoAn.Utils
{
    public static class ThemeHelper
    {
        public static void ApplyTheme(Control control)
        {
            control.BackColor = Constants.Colors.Background;
            
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Constants.Colors.Primary;
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;
                }
                else if (ctrl is TextBox txt)
                {
                    txt.BackColor = Color.White;
                    txt.ForeColor = Constants.Colors.TextPrimary;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (ctrl is ComboBox cmb)
                {
                    cmb.BackColor = Color.White;
                    cmb.ForeColor = Constants.Colors.TextPrimary;
                }
                else if (ctrl is Label lbl)
                {
                    lbl.ForeColor = Constants.Colors.TextDark;
                }
                else if (ctrl is DataGridView dgv)
                {
                    ApplyDataGridViewTheme(dgv);
                }
                else if (ctrl is Panel || ctrl is GroupBox)
                {
                    ApplyTheme(ctrl);
                }
            }
        }
        
        public static void ApplyDataGridViewTheme(DataGridView dgv)
        {
            dgv.BackgroundColor = Constants.Colors.Background;
            dgv.GridColor = Constants.Colors.Border;
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Constants.Colors.TextPrimary;
            dgv.DefaultCellStyle.SelectionBackColor = Constants.Colors.Hover;
            dgv.DefaultCellStyle.SelectionForeColor = Constants.Colors.TextPrimary;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Constants.Colors.HeaderBackground;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Constants.Colors.TextDark;
            dgv.EnableHeadersVisualStyles = false;
        }
    }
}
