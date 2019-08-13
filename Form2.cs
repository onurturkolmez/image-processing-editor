using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessingEditor
{
    public partial class Form2 : Form
    {
        private Histogram.HistogramCiz Histogram;
        private Graphics gr;
        private Form1 form1;
        private string mode = "";
        public Form2(Form callingForm)
        {
            this.Histogram = new Histogram.HistogramCiz();
            this.histogramaDesenat1 = new Histogram.HistogramCiz();

            //form 1 değişkenlerine ulaşmak için form2'de form class'ı olarak çağırıldı
            form1 = (Form1)callingForm;
            mode = form1.mode;
            InitializeComponent();

            //form2'de histogramı gösteren componentler gizlenir.
            this.Histogram.Visible = false;
            this.histogramaDesenat1.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
        }

        //form 1 'de oluşturulan resimlerin form 2 'de çizilmesi eventi
        protected override void OnPaint(PaintEventArgs e)
        {
            gr = e.Graphics;
            int height = 375;
            int width = 530;
            int widthOffset = 20;
            int heightOffset = 40;

            if (mode == "scalling")
            {
                this.Text = "Scalling " + form1.width + "x" + form1.height;
                Image selectedImage = Image.FromFile(form1.fileName);
                gr.DrawImage(selectedImage, 0, 0, form1.width, form1.height);
                this.Width = form1.width + widthOffset;
                this.Height = form1.height + heightOffset;
            }

            else if (mode == "Histogram")
            {
                this.Histogram.Visible = true;
                this.histogramaDesenat1.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                this.Text = "Histogram";

                Image selectedImage = Image.FromFile(form1.fileName);
                long[] values = GetHistogram(new Bitmap(selectedImage));
                Histogram.DrawHistogram(values);

                Image grayImage = (Image)form1.GrayScale;
                long[] values_ = GetHistogram_Gray(new Bitmap(grayImage));
                histogramaDesenat1.DrawHistogram(values_);
            }

            else if (mode == "RGBScale")
            {
                this.Text = "Red,Green,Blue Scale";
                
                gr.DrawImage(form1.RedScale, 0, 0, width, height);
                gr.DrawImage(form1.GreenScale, width + 10, 0, width, height);
                gr.DrawImage(form1.BlueScale, 2 * width + 20, 0, width, height);
                this.Width = 3 * width + 20 + widthOffset;
                this.Height = height + heightOffset;
            }

            else if (mode == "GrayScaling")
            {
                this.Text = "Gray Scale";
                gr.DrawImage(form1.GrayScale, 0, 0, width, height);
                this.Width = width + widthOffset;
                this.Height = height + heightOffset;
            }

            else if (mode == "NegativeImage")
            {
                this.Text = "Negative Image";
                gr.DrawImage(form1.NegativeImage, 0, 0, width, height);
                this.Width = width + widthOffset;
                this.Height = height + heightOffset;
            }

            else if (mode == "Mirroring")
            {
                this.Text = "Mirroring";
                gr.DrawImage(form1.MirroringImage, 0, 0, width, height);
                this.Width = width + widthOffset;
                this.Height = height + heightOffset;
            }

            else if (mode == "Rotation")
            {
                this.Text = "Rotation";
                gr.DrawImage(form1.Right90DegreeRotation, 0, 0, width, height);
                gr.DrawImage(form1.Left90DegreeRotation, width + 10, 0, width, height);
                this.Width = 2 * width + 10 + widthOffset;
                this.Height = height + heightOffset;
            }

            base.OnPaint(e);
        }

        //form resize edildiğinde görüntünün kaymaması için her resize edildiginde formu refreshleyerek resize edilmesini önler.
        private void Form2_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        //RGB resim için renk aralığı dizisi oluşturur.
        public long[] GetHistogram(System.Drawing.Bitmap picture)
        {
            long[] myHistogram = new long[256];

            for (int i = 0; i < picture.Size.Height; i++)
                for (int j = 0; j < picture.Size.Width; j++)
                {
                    Color c = picture.GetPixel(j, i);
                    
                    myHistogram[c.R]++;
                    myHistogram[c.G]++;
                    myHistogram[c.B]++;
                }

            return myHistogram;
        }

        //rgb resmi griye çevirerek renk aralığı dizisi oluşturur.
        public long[] GetHistogram_Gray(Bitmap picture)
        {
            long[] myHistogram = new long[256];

            for (int i = 0; i < picture.Size.Height; i++)
                for (int j = 0; j < picture.Size.Width; j++)
                {
                    Color c = picture.GetPixel(j, i);
                    int a = c.A;
                    int gray = (c.R + c.G + c.B) / 3;
                    Color newColor = Color.FromArgb(a, gray, gray, gray);
                    picture.SetPixel(j, i, newColor);

                    c = picture.GetPixel(j, i);
                    
                    myHistogram[c.R]++;
                    myHistogram[c.G]++;
                    myHistogram[c.B]++;
                }

            return myHistogram;
        }
    }
}
