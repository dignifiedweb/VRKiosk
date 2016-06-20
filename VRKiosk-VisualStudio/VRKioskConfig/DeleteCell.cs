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
    public class DeleteCell : DataGridViewButtonCell
    {
        // Genius idea to override default Paint method to display an image: http://stackoverflow.com/questions/6645699/datagridview-image-button-column
        Bitmap del = new Bitmap(VRKioskConfig.Properties.Resources.deleteIcon);

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            graphics.DrawImage(del, cellBounds);
        }
    }
}
