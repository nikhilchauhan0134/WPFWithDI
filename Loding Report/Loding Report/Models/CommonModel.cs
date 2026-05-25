using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loding_Report.Models
{
    public class PropertyItem
    {
        public long PropertyId { get; set; }
        public string PropertyName { get; set; }

        // This controls what text actually displays inside the ComboBox
        public string DisplayText => $"{PropertyName} - {PropertyId}";
    }

    public class OutletItem
    {
        public long OutletCode { get; set; }
        public string OutletName { get; set; }
        public string DisplayText => OutletName;
    }
}
