using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Imaging;

namespace ImageProcessingEditor
{
    public partial class Form1 : Form
    {
        public string fileName = "";
        public int width;
        public int height;
        public string mode = "";
        public Bitmap RedScale, GreenScale, BlueScale, GrayScale, NegativeImage;
        public Bitmap Right90DegreeRotation, Left90DegreeRotation, MirroringImage;

        public Form1()
        {
            InitializeComponent();
        }

        //choose image button
        private void button1_Click(object sender, EventArgs e)
        {
            label4.Visible = false;
            comboBox1.Visible = false;

            DialogResult result = openFileDialog1.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("Görsel Seçmediniz!");
                return;
            }

            else
            {
                Image i = Image.FromFile(openFileDialog1.FileName);
                fileName = openFileDialog1.FileName;
                pictureBox1.Image = i;
                textBox3.Text = fileName;
                label4.Visible = true;
                comboBox1.Visible = true;
            }
        }

        //functions combobox
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            string secim = comboBox1.SelectedItem.ToString();

            switch (secim)
            {
                case "Histogram":
                    Histogram();
                    break;
                case "Scalling":
                    groupBox1.Visible = true;
                    break;
                case "Gray-Scaling":
                    GrayScalling(true);
                    break;
                case "Red-Green-Blue Scaling":
                    RedGreenBlueScaling();
                    break;
                case "Negative/Invert Image":
                    NegativeInvertImage();
                    break;
                case "Mirroring":
                    Mirroring();
                    break;
                case "Rotation":
                    Rotation();
                    break;
            }
        }

        public void Histogram()
        {
            Bitmap tempImage = new Bitmap(fileName);
            GrayScalling(false);

            mode = "Histogram";

            Form2 form2 = new Form2(this);
            form2.Show();
        }

        public void Mirroring()
        {
            mode = "Mirroring";

            Bitmap tempImage = new Bitmap(fileName);

            MirroringImage = new Bitmap(tempImage.Width, tempImage.Height);

            for (int i = 0; i < tempImage.Width; i++)
            {
                for (int j = 0; j < tempImage.Height; j++)
                {
                    Color pixel = tempImage.GetPixel(i, j);
                    MirroringImage.SetPixel(tempImage.Width - 1 - i, j, pixel);
                }
            }

            Form2 form2 = new Form2(this);
            form2.Show();
        }

        public void Rotation()
        {
            mode = "Rotation";
            Bitmap tempImage = new Bitmap(fileName);
            Right90DegreeRotation = new Bitmap(tempImage.Height, tempImage.Width);
            Left90DegreeRotation = new Bitmap(tempImage.Height, tempImage.Width);

            Right90DegreeRotation = RotationWithAngle(90, tempImage, Right90DegreeRotation);
            Left90DegreeRotation = RotationWithAngle(270, tempImage, Left90DegreeRotation);

            Form2 form2 = new Form2(this);
            form2.Show();
        }

        //it makes to rotate the original image by angle value.
        public Bitmap RotationWithAngle(int rotationAngle, Bitmap originalBitmap, Bitmap rotatedBitmap)
        {
            int newWidth = rotatedBitmap.Width;
            int newHeight = rotatedBitmap.Height;

            int originalWidth = originalBitmap.Width;
            int originalHeight = originalBitmap.Height;

            int newWidthMinusOne = newWidth - 1;
            int newHeightMinusOne = newHeight - 1;

            BitmapData originalData = originalBitmap.LockBits(new Rectangle(0, 0, originalWidth, originalHeight), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
            BitmapData rotatedData = rotatedBitmap.LockBits(new Rectangle(0, 0, rotatedBitmap.Width, rotatedBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);

            unsafe
            {
                int* originalPointer = (int*)originalData.Scan0.ToPointer();
                int* rotatedPointer = (int*)rotatedData.Scan0.ToPointer();

                switch (rotationAngle)
                {
                    case 90:
                        for (int y = 0; y < originalHeight; ++y)
                        {
                            int destinationX = newWidthMinusOne - y;
                            for (int x = 0; x < originalWidth; ++x)
                            {
                                int sourcePosition = (x + y * originalWidth);
                                int destinationY = x;
                                int destinationPosition = (destinationX + destinationY * newWidth);
                                rotatedPointer[destinationPosition] = originalPointer[sourcePosition];
                            }
                        }
                        break;
                    case 270:
                        for (int y = 0; y < originalHeight; ++y)
                        {
                            int destinationX = y;
                            for (int x = 0; x < originalWidth; ++x)
                            {
                                int sourcePosition = (x + y * originalWidth);
                                int destinationY = newHeightMinusOne - x;
                                int destinationPosition = (destinationX + destinationY * newWidth);
                                rotatedPointer[destinationPosition] = originalPointer[sourcePosition];
                            }
                        }
                        break;
                }

                originalBitmap.UnlockBits(originalData);
                rotatedBitmap.UnlockBits(rotatedData);
                return rotatedBitmap;
            }
        }

        public void NegativeInvertImage()
        {
            mode = "NegativeImage";
            Bitmap tempImage = new Bitmap(fileName);
            NegativeImage = new Bitmap(tempImage.Width, tempImage.Height);

            for (int x = 0; x < tempImage.Width; x++)
            {
                for (int y = 0; y < tempImage.Height; y++)
                {
                    Color pixel = tempImage.GetPixel(x, y);

                    Color newColor = Color.FromArgb(255, 255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
                    NegativeImage.SetPixel(x, y, newColor);
                }
            }
            
            Form2 form2 = new Form2(this);
            form2.Show();
        }

        //if "open" prameter is true,it makes the picture gray scale and shows gray scale image on new form (Form2.cs)
        //else if "open" parameter is false,it 
        //open parametresi true ise grayscale için formun devamında yeni formda(Form2.cs) gray scale resim açılır.
        //open parametresi false ise aynı fonksiyon sadece renkli resmi griye çevirmek için kullanır ve devamında histogramını çizer.
        private void GrayScalling(bool Open)
        {
            mode = "GrayScaling";
            Bitmap tempImage = new Bitmap(fileName);
            GrayScale = new Bitmap(tempImage.Width, tempImage.Height);

            for (int x = 0; x < tempImage.Width; x++)
            {
                for (int y = 0; y < tempImage.Height; y++)
                {
                    Color pixel = tempImage.GetPixel(x, y);
                    int a = pixel.A;
                    int gray = (pixel.B + pixel.G + pixel.R) / 3;

                    Color newColor = Color.FromArgb(a, gray, gray, gray);
                    GrayScale.SetPixel(x, y, newColor);
                }
            }

            if (Open)
            {
                Form2 form2 = new Form2(this);
                form2.Show();
            }

        }

        private void RedGreenBlueScaling()
        {
            mode = "RGBScale";
            Bitmap tempImage = new Bitmap(fileName);
            RedScale = new Bitmap(tempImage.Width, tempImage.Height);
            GreenScale= new Bitmap(tempImage.Width, tempImage.Height);
            BlueScale = new Bitmap(tempImage.Width, tempImage.Height);

            for (int x = 0; x < tempImage.Width; x++)
            {
                for (int y = 0; y < tempImage.Height; y++)
                {
                    Color pixel = tempImage.GetPixel(x, y);
                    int a = pixel.A;
                    int red = pixel.R;

                    Color newColor = Color.FromArgb(a, red, 0, 0);
                    RedScale.SetPixel(x, y, newColor);
                }
            }

            for (int x = 0; x < tempImage.Width; x++)
            {
                for (int y = 0; y < tempImage.Height; y++)
                {
                    Color pixel = tempImage.GetPixel(x, y);
                    int a = pixel.A;
                    int green = pixel.G;

                    Color newColor = Color.FromArgb(a, 0, green, 0);
                    GreenScale.SetPixel(x, y, newColor);
                }
            }
            
            for (int x = 0; x < tempImage.Width; x++)
            {
                for (int y = 0; y < tempImage.Height; y++)
                {
                    Color pixel = tempImage.GetPixel(x, y);
                    int a = pixel.A;
                    int blue = pixel.B;

                    Color newColor = Color.FromArgb(a, 0, 0, blue);
                    BlueScale.SetPixel(x, y, newColor);
                }
            }

            Form2 form2 = new Form2(this);
            form2.Show();
        }

        //scalling göster button
        private void button2_Click(object sender, EventArgs e)
        {
            mode = "scalling";
            width = Convert.ToInt32(textBox1.Text);
            height = Convert.ToInt32(textBox2.Text);
            Form2 form2 = new Form2(this);
            
            form2.Show();
        }

    }
}
