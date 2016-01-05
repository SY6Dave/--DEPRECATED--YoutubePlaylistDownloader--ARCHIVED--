using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubePlaylistDownloader
{
    class CustomCheckList : CheckedListBox
    {
        public CustomCheckList()
        {
            DoubleBuffered = true;
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }

        object listlock = new object();

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            double percent = 0;

            if(Form1.pd.videos != null)
            {
                lock(listlock)
                {
                    try
                    {
                        foreach (Downloadable d in Form1.pd.videos)
                        {
                            if (d.Index == e.Index)
                            {
                                percent = d.PercentCompletion / 100;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        return;
                    }
                    
                }
                
            }

            Rectangle rect = e.Bounds;
            rect.X += 18;
            rect.Width -= 18;
            rect.Y += 1;
            rect.Height -= 2;
            rect.Width = (int)((float)rect.Width * percent);

            Color rectCol = Color.FromArgb(80, Color.Green);
            e.Graphics.FillRectangle(new SolidBrush(rectCol), rect);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Region iRegion = new Region(e.ClipRectangle);
            e.Graphics.FillRegion(new SolidBrush(this.BackColor), iRegion);
            if (this.Items.Count > 0)
            {
                for (int i = 0; i < this.Items.Count; ++i)
                {
                    System.Drawing.Rectangle irect = this.GetItemRectangle(i);
                    if (e.ClipRectangle.IntersectsWith(irect))
                    {
                        if ((this.SelectionMode == SelectionMode.One && this.SelectedIndex == i)
                        || (this.SelectionMode == SelectionMode.MultiSimple && this.SelectedIndices.Contains(i))
                        || (this.SelectionMode == SelectionMode.MultiExtended && this.SelectedIndices.Contains(i)))
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
                                irect, i,
                                DrawItemState.Selected, this.ForeColor,
                                this.BackColor));
                        }
                        else
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
                                irect, i,
                                DrawItemState.Default, this.ForeColor,
                                this.BackColor));
                        }
                        iRegion.Complement(irect);
                    }
                }
            }
            base.OnPaint(e);
        }
    }
}
