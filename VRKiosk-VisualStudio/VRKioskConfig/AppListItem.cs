using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace VRKioskConfig
{
    public class AppListItem : IComparable<AppListItem>
    {
        public Bitmap Icon { get; set; }
        public String Name { get; set; }
        public Bitmap Image { get; set; }
        public String ImageFilePath { get; set; }
        public String FilePath { get; set; }
        public String Parameters { get; set; }
        public String SendKeysForDialog { get; set; }
        public String SendKeysForAButton { get; set; }
        public String Notes { get; set; }
        public bool PhotoChanged { get; set; }
        public List<AppListItemCategory> Categories { get; set; }
        public DateTime AppDateTimeAdded { get; set; }

        // JCarewick
        // TODO
        // Test making the long write out getters and setters maybe that will not set them to be displayed by datagridview
        // 

        public AppListItem()
        {
            PhotoChanged = false;
            Categories = new List<AppListItemCategory>();
        }

        public int CompareTo(AppListItem other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                return this.Name.CompareTo(other.Name);
            }
        }
    }
}
