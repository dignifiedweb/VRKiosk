using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;

namespace VRKioskConfig
{
    public class DeleteColumn : DataGridViewButtonColumn
    {
        public DeleteColumn()
        {
            this.CellTemplate = new DeleteCell();
            this.Width = VrKioskMain.ICON_WIDTH;
            this.Name = "Delete";
            this.FlatStyle = FlatStyle.Flat;
            this.DefaultCellStyle.ForeColor = Color.White;
            this.DefaultCellStyle.BackColor = Color.White;
        }
    }
}
