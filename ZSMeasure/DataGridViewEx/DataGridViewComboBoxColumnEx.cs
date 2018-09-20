using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace ZSMeasure
{
    /// <summary>
    /// 可修改 DropDownStyle 的 DataGridViewComboBoxColumn
    /// </summary>
    public class DataGridViewComboBoxColumnEx : DataGridViewComboBoxColumn
    {
        ComboBoxStyle dropDownStyle;

        /// <summary>
        /// 控制组合框的外观和功能
        /// </summary>
        [Description("控制组合框的外观和功能"), DefaultValue(ComboBoxStyle.DropDownList)]
        public ComboBoxStyle DropDownStyle
        {
            get { return dropDownStyle; }
            set { dropDownStyle = value; }
        }

        public DataGridViewComboBoxColumnEx()
        {
            dropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
