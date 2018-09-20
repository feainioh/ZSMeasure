using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace HWLibs.Windows.Forms
{
    /// <summary>
    /// 扩展的 DataGridView
    /// </summary>
    public class DataGridViewEx : DataGridView
    {
        bool showRowHeaderNumbers;

        /// <summary>
        /// 是否显示行号
        /// </summary>
        [Description("是否显示行号"), DefaultValue(true)]
        public bool ShowRowHeaderNumbers
        {
            get { return showRowHeaderNumbers; }
            set 
            {
                if (showRowHeaderNumbers != value)
                    Invalidate();
                showRowHeaderNumbers = value; 
            }
        }

        public DataGridViewEx()
        {
            showRowHeaderNumbers = true;
            RowPostPaint += new DataGridViewRowPostPaintEventHandler(DataGridViewEx_RowPostPaint);
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            DataGridViewCell cell = this[e.ColumnIndex, e.RowIndex];
            if (e.RowIndex == 1 && e.ColumnIndex == 2)
            {
                e.Handled = true;
                return;
            }
            if (cell is DataGridViewCombineCell)
            {
                DataGridViewCombineCell combineCell = cell as DataGridViewCombineCell;
                Rectangle rect = GetCombinedRect(combineCell);

                DrawCombinCellBackground(combineCell, e.Graphics, rect);
                if (combineCell.Selected)
                {
                    ControlPaint.DrawFocusRectangle(e.Graphics, rect);
                }
                DrawCombinCellText(combineCell, e.Graphics, rect);
                e.Handled = true;
                return;
            }
            base.OnCellPainting(e);
        }

        private void DrawCombinCellText(DataGridViewCombineCell combineCell, Graphics graphics, Rectangle rect)
        {
            if (combineCell.Value != null)
            {
                StringFormat sf = new StringFormat();
                DataGridViewContentAlignment alignment = combineCell.Style.Alignment == DataGridViewContentAlignment.NotSet
                    ? DefaultCellStyle.Alignment : combineCell.Style.Alignment;
                switch (alignment)
                {
                    case DataGridViewContentAlignment.BottomCenter:
                        break;
                    case DataGridViewContentAlignment.BottomLeft:
                        break;
                    case DataGridViewContentAlignment.BottomRight:
                        break;
                    case DataGridViewContentAlignment.MiddleCenter:
                        break;
                    case DataGridViewContentAlignment.MiddleLeft:
                        break;
                    case DataGridViewContentAlignment.MiddleRight:
                        break;
                    case DataGridViewContentAlignment.NotSet:
                        break;
                    case DataGridViewContentAlignment.TopCenter:
                        break;
                    case DataGridViewContentAlignment.TopLeft:
                        break;
                    case DataGridViewContentAlignment.TopRight:
                        break;
                    default:
                        break;
                }
                Font font = combineCell.Style.Font == null ? DefaultCellStyle.Font : combineCell.Style.Font;
                Brush bruForeColor = new SolidBrush(combineCell.Style.ForeColor.IsEmpty ? DefaultCellStyle.ForeColor : combineCell.Style.ForeColor);

                graphics.DrawString(combineCell.Value.ToString(), font, bruForeColor, rect, sf);

                bruForeColor.Dispose();
            }
        }

        private void DrawCombinCellBackground(DataGridViewCombineCell combineCell, Graphics graphics, Rectangle rect)
        {
            Brush bruCellBackColor = GetCellBackColorBrush(combineCell, combineCell.Selected);
            Pen penCellBorder = GetCellBorderPen(combineCell);

            graphics.FillRectangle(bruCellBackColor, rect);
            graphics.DrawLine(penCellBorder, new Point(rect.Left, rect.Bottom - 1), new Point(rect.Right, rect.Bottom - 1));
            graphics.DrawLine(penCellBorder, new Point(rect.Right - 1, rect.Top), new Point(rect.Right - 1, rect.Bottom - 1));

            bruCellBackColor.Dispose();
            penCellBorder.Dispose();
        }

        private Pen GetCellBorderPen(DataGridViewCombineCell combineCell)
        {
            return new Pen(this.GridColor);
        }

        private Brush GetCellBackColorBrush(DataGridViewCombineCell combineCell, bool selected)
        {
            if (selected)
            {
                if (!combineCell.Style.SelectionBackColor.IsEmpty)
                    return new SolidBrush(combineCell.Style.SelectionBackColor);
                return new SolidBrush(DefaultCellStyle.SelectionBackColor);
            }
            else
            {
                if (!combineCell.Style.BackColor.IsEmpty)
                    return new SolidBrush(combineCell.Style.BackColor);
                return new SolidBrush(DefaultCellStyle.BackColor);
            }
        }

        private Rectangle GetCombinedRect(DataGridViewCombineCell combineCell)
        {
            Rectangle rect = GetCellDisplayRectangle(combineCell.ColumnIndex, combineCell.RowIndex, false);
            for (int i = 0; i < combineCell.ColSpan - 1; i++)
            {
                rect.Width += this.Columns[combineCell.ColumnIndex + i + 1].Width;
            }
            for (int i = 0; i < combineCell.RowSpan - 1; i++)
            {
                rect.Height += this.Rows[combineCell.RowIndex + i + 1].Height;
            }
            return rect;
        }

        void DataGridViewEx_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (showRowHeaderNumbers)
            {
                string title = (e.RowIndex + 1).ToString();
                Brush bru = Brushes.Black;
                e.Graphics.DrawString(title, DefaultCellStyle.Font,
                    bru, e.RowBounds.Location.X + RowHeadersWidth / 2 - 4, e.RowBounds.Location.Y + 4);
            }
        }
    }
}
