using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//*******************************
//bibliotecas de EmguCV
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

//*******************************

namespace Tiro_parabolico
{
    public partial class Form1 : Form
    {
        VideoCapture video;
        bool IsPlaying =false;
        int TotalFrames;
        int CurrentFrameNo;
        Mat CurrentFrame;
        int FPS;

        public Form1()
        {
            InitializeComponent();

        }

        private void load_Click(object sender, EventArgs e)
            //Boton para load para cargar archivos
        {
            Buscar();
        }


        private void Buscar()//Buscar fotos en el explorador de windows
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string ruta;

            try
            {

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ruta = openFileDialog1.FileName;
                    video = new VideoCapture(ruta);
                    TotalFrames = Convert.ToInt32(video.GetCaptureProperty(CapProp.FrameCount));
                    FPS = Convert.ToInt32(video.GetCaptureProperty(CapProp.Fps));
                    IsPlaying = true;
                    CurrentFrame = new Mat();
                    CurrentFrameNo = 0;
                    trackBar1.Minimum = 0;
                    trackBar1.Maximum = TotalFrames - 1;
                    trackBar1.Value = 0;
                    PlayVideo();

                    
                   
                    //matrix.Data[]
                    
                    
                }
            }
            catch
            {
            }


        }

        private async void PlayVideo()
        {
           if(video == null)
            {
                return;
            }
            try
            {
                while(IsPlaying == true && CurrentFrameNo < TotalFrames)
                {
                    video.SetCaptureProperty(CapProp.PosFrames, CurrentFrameNo);
                    video.Read(CurrentFrame);
                    pictureBox1.Image = CurrentFrame.Bitmap;
                    trackBar1.Value = CurrentFrameNo;
                    CurrentFrameNo += 1;
                    await Task.Delay(1000 / FPS);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Play_Click(object sender, EventArgs e)
        {
            if(video != null)
            {
                IsPlaying = true;
                PlayVideo();
            }
            else
            {
                IsPlaying = false;
            }
            
           
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
           if(video != null)
            {
                CurrentFrameNo = trackBar1.Value;
            }

        }
    }
}
