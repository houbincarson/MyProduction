using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.BarCode;

namespace ProduceManager
{
    public partial class frmPickPicture : Form
    {
        private bool DeviceExist = false;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource = null;
        public static int i = 0;

        public List<string> ListPath
        {
            get;
            set;
        }

        public frmPickPicture()
        {
            InitializeComponent();
            ListPath = new List<string>();
            getCamList();
        }

        //获取摄像头列表
        private void getCamList()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                ComCamera.Items.Clear();
                if (videoDevices.Count == 0)
                    throw new ApplicationException();
                DeviceExist = true;
                foreach (FilterInfo device in videoDevices)
                {
                    ComCamera.Items.Add(device.Name);
                }
            }
            catch (ApplicationException)
            {
                DeviceExist = false;
                ComCamera.Items.Add("没找到摄像头");
            }
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = img;
        }

        private void CloseVideoSource()
        {
            if (!(videoSource == null))
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource.Stop();
                    videoSource = null;
                }
        }

        //截图
        private void PickPicture_Click(object sender, EventArgs e)
        {
            if (txtImagePath.Text.Trim().Length == 0)
            {
                MessageBox.Show("请先选择图片保存路径");
                return;
            }
            //if (txtMin.Text.Trim().Length == 0)
            //{

            //}
            //else
            //{
                //timer1.Start();
                string strPath = txtImagePath.Text + "\\" + (i++) + ".jpg";
                ListPath.Add(strPath);
                pictureBox1.Image.Save(strPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            //}
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            CloseVideoSource();
            //timer1.Dispose();
            this.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PickPicture_Click(null, null);
        }

        private void OpenSignal_Click(object sender, EventArgs e)
        {


            if (ComCamera.Text != "")
            {
                pictureBox1.Image = null;

                if (OpenSignal.Text == "开启摄像头")
                {
                    if (DeviceExist)
                    {
                        videoSource = new VideoCaptureDevice(videoDevices[ComCamera.SelectedIndex].MonikerString);
                        videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                        CloseVideoSource();
                        videoSource.DesiredFrameSize = new Size(331, 215);
                        //videoSource.DesiredFrameRate = 10;            
                        videoSource.Start();
                        OpenSignal.Text = "关闭摄像头";
                    }
                    else
                    {

                        label1.Text = "程序报错: 没有可用的拍摄设备！";
                    }
                }
                else
                {
                    if (videoSource.IsRunning)
                    {
                        CloseVideoSource();
                        //timer1.Dispose();
                        OpenSignal.Text = "开启摄像头";
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选择摄像头！"); 
            }

        }

        private void btnSetPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            this.txtImagePath.Text = path.SelectedPath;
        }

        private void frmPickPicture_Load(object sender, EventArgs e)
        {

        }
    }
}
