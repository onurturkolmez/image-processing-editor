using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Histogram
{
	public class HistogramCiz : System.Windows.Forms.UserControl
	{
		private System.ComponentModel.Container components = null;

        public HistogramCiz()
		{
			
			InitializeComponent();

			this.Paint += new PaintEventHandler(HistogramCiz_Paint);
			this.Resize+=new EventHandler(HistogramCiz_Resize);
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		
		private void InitializeComponent()
		{
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "Histogram Çiz";
			this.Size = new System.Drawing.Size(208, 176);
		}

        private void HistogramCiz_Paint(object sender, PaintEventArgs e)
		{
			if (myIsDrawing)
			{

				Graphics g = e.Graphics;
				Pen myPen = new Pen(new SolidBrush(myColor),myXUnit);
				for (int i=0;i<myValues.Length;i++)
				{
					g.DrawLine(myPen,
						new PointF(myOffset + (i*myXUnit), this.Height - myOffset), 
						new PointF(myOffset + (i*myXUnit), this.Height - myOffset - myValues[i] * myYUnit));

					if (myValues[i]==myMaxValue)
					{
						SizeF mySize = g.MeasureString(i.ToString(),myFont);

						g.DrawString(i.ToString(),myFont,new SolidBrush(myColor),
							new PointF(myOffset + (i*myXUnit) - (mySize.Width/2), this.Height - myFont.Height ),
							System.Drawing.StringFormat.GenericDefault);
					}
				}

				g.DrawString("0",myFont, new SolidBrush(myColor),new PointF(myOffset,this.Height - myFont.Height),System.Drawing.StringFormat.GenericDefault);
				g.DrawString((myValues.Length-1).ToString(),myFont, 
					new SolidBrush(myColor),
					new PointF(myOffset + (myValues.Length * myXUnit) - g.MeasureString((myValues.Length-1).ToString(),myFont).Width,
					this.Height - myFont.Height),
					System.Drawing.StringFormat.GenericDefault);

				g.DrawRectangle(new System.Drawing.Pen(new SolidBrush(Color.Black),1),0,0,this.Width-1,this.Height-1);
			}
		}

		long myMaxValue;
		private long[] myValues;
		private bool myIsDrawing;

		private float myYUnit;
		private float myXUnit;
		private int myOffset = 20;

		private Color myColor = Color.Black;
		private Font myFont = new Font("Tahoma",10);

		[Category("Histogram Options")]
		[Description ("The distance from the margins for the histogram")]
		public int Offset
		{
			set
			{
				if (value>0)
					myOffset= value;
			}
			get
			{
				return myOffset;
			}
		}

		[Category("Histogram Options")]
		[Description ("The color used within the control")]
		public Color DisplayColor
		{
			set
			{
				myColor = value;
			}
			get
			{
				return myColor;
			}
		}
        
		public void DrawHistogram(long[] Values)
		{
			myValues = new long[Values.Length];
			Values.CopyTo(myValues,0);

			myIsDrawing = true;
			myMaxValue = getMaxim(myValues);

			ComputeXYUnitValues();

			this.Refresh();
		}
        
		private long getMaxim(long[] Vals)
		{
			if (myIsDrawing)
			{
				long max = 0;
				for (int i=0;i<Vals.Length;i++)
				{
					if (Vals[i] > max)
						max = Vals[i];
				}
				return max;
			}
			return 1;
		}

        private void HistogramCiz_Resize(object sender, EventArgs e)
		{
			if (myIsDrawing)
			{
				ComputeXYUnitValues();
			}
			this.Refresh();
		}

		private void ComputeXYUnitValues()
		{
			myYUnit = (float) (this.Height - (2 * myOffset)) / myMaxValue;
			myXUnit = (float) (this.Width - (2 * myOffset)) / (myValues.Length-1);
		}
	}
}
