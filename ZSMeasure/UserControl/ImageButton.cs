using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZSMeasure
{
    public class ImageButton:System.Windows.Forms.Button
    {
        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }
                
    }
}
