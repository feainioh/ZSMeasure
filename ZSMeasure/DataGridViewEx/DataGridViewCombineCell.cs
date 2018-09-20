using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HWLibs.Windows.Forms
{
    public class DataGridViewCombineCell : DataGridViewTextBoxCell
    {
        public DataGridViewCombineCell()
        {
        }

        int rowSpan;

        /// <summary>
        /// 合并行数
        /// </summary>
        public int RowSpan
        {
            get { return rowSpan; }
            set { rowSpan = value; }
        }
        int colSpan;
        /// <summary>
        /// 和并列数
        /// </summary>
        public int ColSpan
        {
            get { return colSpan; }
            set { colSpan = value; }
        }
    }
}
