using System;
using System.IO;
using System.Windows.Forms;
using NReco.VideoConverter;
using System.Threading.Tasks;
using System.Threading;

namespace VideoConverter
{
    public partial class VideoConverterApp : Form
    {
        public VideoConverterApp()
        {
            InitializeComponent();
        }
        // variable to store the filename and extension
        string fName, fExt;
        int p = -1;
        private void VideoConverterApp_Load(object sender, EventArgs e)
        {
            //backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            //// Set the text.
            //this.Text = e.ProgressPercentage.ToString();
        }

        /// <summary>
        /// Run Process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //var result = openFileDialog1.ShowDialog();
            //OpenFileDialog ofd = new OpenFileDialog();
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    FileInfo fi = new FileInfo(ofd.FileName);
            //    fExt = fi.Extension;
            //    fName = fi.FullName.Substring(0, fi.FullName.Length - fExt.Length);
            //    try
            //    {



            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.GetBaseException().Message, @"Error", MessageBoxButtons.OK);
            //    }


            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //var result = openFileDialog1.ShowDialog();
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(ofd.FileName);
                fExt = fi.Extension;
                fName = fi.FullName.Substring(0, fi.FullName.Length - fExt.Length);
                //if (!fExt.Equals(".wmv"))
                //{
                //    MessageBox.Show("Only WMV file type is supported");
                //}
                //else
                //{ }
                try
                {

                    var x = false;
                    var message = "";
                    var input = ofd.FileName;//Path.Combine(@"D:\Dev\Company\Exalted Solutions 2016 July\Projects\CaliberMatrix-Development\CaliberMatrix\SeleniumTestCaseRecording\SeleniumTest-3034ef9ce96dfb5b02fb76a04851ae61", "Video-2017-04-07_12-02-10-AM.wmv");
                    if (!File.Exists(input))
                    {
                        MessageBox.Show(@"File does not exist", @"Error", MessageBoxButtons.OK);
                    }
                    else
                    {
                        var fileName = Path.GetFileNameWithoutExtension(input);

                        var outputFileNameOgg = fileName + ".ogg";
                        var outputFileNameMp4 = fileName + ".mp4";
                        var outputFileNameWebm = fileName + ".webm";

                        var bytes = File.ReadAllBytes(input);
                        var data = new MemoryStream(bytes);
                        var t = Task.Factory.StartNew(() =>
                         {
                             try
                             {
                                 //backgroundWorker1.RunWorkerAsync();
                                 var ffMpeg = new FFMpegConverter();
                                 //backgroundWorker1.RunWorkerAsync();
                                 ffMpeg.ConvertProgress += UpdateProgress;
                                 //ffMpeg.ConvertMedia(input, outputFileNameWebm, Format.webm);
                                 //ffMpeg.ConvertMedia(input, outputFileNameOgg, Format.ogg);
                                 //ffMpeg.ConvertMedia(input, outputFileNameMp4, Format.mp4);
                                 ffMpeg.ConvertMedia(input, null, outputFileNameMp4, null, new ConvertSettings()
                                 {
                                     CustomOutputArgs = "-profile:v baseline -level 3.0 -pix_fmt yuv420p -threads 2 -movflags +faststart"
                                 });

                                 x = true;
                                 p = 100;
                                 message = "Video converted successfully";
                                 return x;
                             }
                             catch (Exception ex)
                             {
                                 p = 100;
                                 x = false;
                                 message = ex.GetBaseException().Message;
                                 return x;
                             }
                         });
                        t.Wait();
                        if (t.IsCompleted)
                            if (x)
                            {
                                MessageBox.Show(message, @"Success", MessageBoxButtons.OK);
                            }
                            else
                            {
                                MessageBox.Show(message, @"Error", MessageBoxButtons.OK);
                            }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetBaseException().Message, @"Error", MessageBoxButtons.OK);
                }


            }
        }


        private async void UpdateProgress(object sender, ConvertProgressEventArgs e)
        {

            var processed = int.Parse(e.Processed.TotalMilliseconds.ToString()) * 100 / int.Parse(e.TotalDuration.TotalMilliseconds.ToString());
            //progressBar1.Step
            
            await Task.Run(() => { backgroundWorker1.ReportProgress(processed); });
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //for (int i = 1; i <= 100; i++)
            //{
            //    // Wait 100 milliseconds.
            //    Thread.Sleep(1000);
            //    // Report progress.
            //    if (p == -1)
            //        backgroundWorker1.ReportProgress(i);
            //    else
            //    {
            //        backgroundWorker1.ReportProgress(p);
            //        break;
            //    }
            //}
        }

    }
}
