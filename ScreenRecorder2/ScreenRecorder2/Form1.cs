using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Accord.Video;
using Accord.Video.FFMPEG;
using System.IO;


namespace ScreenRecorder2
{
    public partial class Form1 : Form
    {

        ScreenCaptureStream stream = new ScreenCaptureStream(Screen.PrimaryScreen.Bounds);

        VideoFileWriter writer = new VideoFileWriter();
        



        public Form1()
        {
            InitializeComponent();

            
        }

        
        private void RecordButton_Click(object sender, EventArgs e)
        {
            string folderpath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            string filename = FileNameBox.Text + ".WMV";
            string path = Path.Combine(folderpath, filename);
            MessageLabel.Text = "Recording";
            writer.Open(path, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height,10,VideoCodec.WMV1,12000000);
            stream.NewFrame += new NewFrameEventHandler(video_NewFrame);

            stream.Start();
        }

        private void StopRecordingButton_Click(object sender, EventArgs e)
        {
            stream.SignalToStop();
            writer.Close();

            Application.Exit();
            
            
          
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Bitmap bitmap = eventArgs.Frame;
                bitmap.SetResolution(3800f, 3800f);
                if (bitmap != null)
                    writer.WriteVideoFrame(bitmap);
            }

            catch (Exception)
            {

            }

        }
    }
}
