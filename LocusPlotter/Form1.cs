using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocusPlotter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void drawAxis(ref Bitmap source)
        {
            using (Graphics g = Graphics.FromImage(source))
            {
                g.DrawLine(Pens.Blue, source.Width / 2, 0, source.Width / 2, source.Height);
                g.DrawLine(Pens.Blue, 0, source.Height / 2, source.Width, source.Height / 2);

            }
        }

        private void drawMinusOne(ref Bitmap source, float scale)
        {
            using (Graphics g = Graphics.FromImage(source))
            {
                g.DrawLine(Pens.Blue, source.Width / 2 - scale * 1, source.Height / 2 + 10, source.Width / 2 - scale * 1, source.Height / 2 - 10);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double interval = 1;
            double startArg = 0.0, finishArg = 1000.0;
            float scale = 300;

            string u = uBox.Text.Replace(".", ",");
            string v = vBox.Text.Replace(".", ",");

            Locus godograph = new Locus(u, v);
            int deltaX = pictureBox1.Width / 2;
            int deltaY = pictureBox1.Height / 2;

            
            List<PointF> points = new List<PointF>();

            Bitmap resultImage = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            drawAxis(ref resultImage);
            drawMinusOne(ref resultImage, scale);

            for (double w = startArg; w < finishArg; w += interval)
            {
                PointF? newPoint = godograph.GetPoint(w);

                if (newPoint != null)
                {
                    points.Add(new PointF(newPoint.Value.X * scale + deltaX, newPoint.Value.Y * scale + deltaY));
                }
            }


            using (Graphics g = Graphics.FromImage(resultImage))
            {
                for (int i = 0; i < points.Count - 1; i++)
                    if (points[i].X<10000 && points[i].Y<10000 && points[i+1].X<10000 && points[i+1].Y<10000)
                    try
                    {
                        g.DrawLine(Pens.Black, points[i], points[i + 1]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        //throw;
                    }
                    
            }

            pictureBox1.Image = resultImage;

        }
    }
}
