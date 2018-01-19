using AForge.Imaging;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ImageMM
{
	public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            onFormLoad();
        }

        #region Global Components

        public class Channels
        {
            public string channel { get; set; }
            public int index { get; set; }
        }

        public Bitmap globalImage { get; set; }

        public Point startPt;
        public Point endPt;
        public Rectangle rctZoom;
        public bool mouseStatus = false;
        public Bitmap noZoomBmp;
        List<Bitmap> zoomBmpList = new List<Bitmap>();

        #endregion Global Components

        #region Methods
        protected void onShowImage(Bitmap pBmp)
        {

            _pcbImage.Image = pBmp;
            // Bitmap data configs
            var bitmap = new Bitmap(pBmp);
            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            var bitmapInRgbFormat = bitmap.Clone(rect, PixelFormat.Format32bppRgb);
            BitmapData bmData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);

            // Average variables
            float AvgR = 0;
            float AvgG = 0;
            float AvgB = 0;
            float AvgY = 0;
            float AvgCr = 0;
            float AvgCb = 0;
            long loopcount = 0;

            // Clean Labels
            _lblAvgR.Text = "Avg R: ";
            _lblAvgG.Text = "Avg G: ";
            _lblAvgB.Text = "Avg B: ";
            _lblAvgY.Text = "Avg Y: ";
            _lblAvgCr.Text = "Avg Cr: ";
            _lblAvgCb.Text = "Avg Cb: ";

            // Generating Averages
            for (int y = 0; y <= bmData.Height - 1; y++)
            {
                for (int x = 0; x <= bmData.Width - 1; x++)
                {
                    Color PixelColor = Color.FromArgb(Marshal.ReadInt32(bmData.Scan0, (bmData.Stride * y) + (4 * x)));

                    AvgR += Convert.ToInt32(PixelColor.R);
                    AvgG += Convert.ToInt32(PixelColor.G);
                    AvgB += Convert.ToInt32(PixelColor.B);

                    RGB rgbcolor = new RGB(PixelColor);

                    AvgY += YCbCr.FromRGB(rgbcolor).Y;
                    AvgCr += YCbCr.FromRGB(rgbcolor).Cr;
                    AvgCb += YCbCr.FromRGB(rgbcolor).Cb;

                    loopcount++;

                }

            }

            _lblAvgR.Text += Convert.ToString(AvgR / loopcount);
            _lblAvgG.Text += Convert.ToString(AvgG / loopcount);
            _lblAvgB.Text += Convert.ToString(AvgB / loopcount);
            _lblAvgY.Text += Convert.ToString(AvgY / loopcount);
            _lblAvgCr.Text += Convert.ToString(AvgCr / loopcount);
            _lblAvgCb.Text += Convert.ToString(AvgCb / loopcount);
        }

        protected void onFormLoad()
        {
            //Building a dataset from combobox
            var dsChannels = new List<Channels>();
            dsChannels.Add(new Channels() { channel = "Channel R", index = 0 });
            dsChannels.Add(new Channels() { channel = "Channel G", index = 1 });
            dsChannels.Add(new Channels() { channel = "Channel B", index = 2 });
            dsChannels.Add(new Channels() { channel = "Channel Y", index = 3 });
            dsChannels.Add(new Channels() { channel = "Channel Cr", index = 4 });
            dsChannels.Add(new Channels() { channel = "Channel Cb", index = 5 });
            dsChannels.Add(new Channels() { channel = "Channel R+G", index = 6 });
            dsChannels.Add(new Channels() { channel = "Channel R+B", index = 7 });
            dsChannels.Add(new Channels() { channel = "Channel G+B", index = 8 });
            dsChannels.Add(new Channels() { channel = "Channel R+G+B", index = 9 });
            dsChannels.Add(new Channels() { channel = "Channel Y+Cr", index = 10 });
            dsChannels.Add(new Channels() { channel = "Channel Y+Cb", index = 11 });
            dsChannels.Add(new Channels() { channel = "Channel Cr+Cb", index = 12 });
            dsChannels.Add(new Channels() { channel = "Channel Y+Cr+Cb", index = 13 });

            _cmbSelectChannel.DataSource = dsChannels;
            _cmbSelectChannel.DisplayMember = "channel";
            _cmbSelectChannel.ValueMember = "index";
            _cmbSelectChannel.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        #endregion Methods

        #region Events
        private void _btnOpenFile_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            _ofdOpenImage = new OpenFileDialog();

            _ofdOpenImage.InitialDirectory = "c:\\";
            _ofdOpenImage.Filter = "Image files (*.png, *.jpg, *.jpeg, *.gif)|*.png;*.jpg;*.jpeg;*.gif";
            _ofdOpenImage.FilterIndex = 2;
            _ofdOpenImage.RestoreDirectory = true;

            if (_ofdOpenImage.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = _ofdOpenImage.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            Bitmap openBmp = new Bitmap(myStream);
                            globalImage = openBmp;
                            onShowImage(openBmp);
                            _btnChangeChannel.Enabled = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void _btnChangeChannel_Click(object sender, EventArgs e)
        {
            // Clear noZoom List
            zoomBmpList.Clear();

            // Bitmap data configs
            var bitmap = new Bitmap(globalImage);
            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            var bitmapInRgbFormat = bitmap.Clone(rect, PixelFormat.Format32bppRgb);
            BitmapData bmData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);

            if (_cmbSelectChannel.SelectedValue.ToString() == "0")
            {
                //Extracting Channel and Plotting New Bitmap - Channel R
                ExtractChannel extractFilter = new ExtractChannel(RGB.R);
                Bitmap bmp = new Bitmap(bmData.Width, bmData.Height, PixelFormat.Format32bppRgb);

                Bitmap MainChannel = extractFilter.Apply(bitmapInRgbFormat);
                ReplaceChannel replaceFilter = new ReplaceChannel(RGB.R, MainChannel);
                replaceFilter.ApplyInPlace(bmp);

                onShowImage(bmp);

                //Bitmap Closure
                bitmap.UnlockBits(bmData);
                bitmapInRgbFormat.Dispose();
                bitmap.Dispose();

            }
            else if (_cmbSelectChannel.SelectedValue.ToString() == "1")
            {
                //Extracting Channel and Plotting New Bitmap - Channel G
                ExtractChannel extractFilter = new ExtractChannel(RGB.G);
                Bitmap bmp = new Bitmap(bmData.Width, bmData.Height, PixelFormat.Format32bppRgb);

                Bitmap MainChannel = extractFilter.Apply(bitmapInRgbFormat);
                ReplaceChannel replaceFilter = new ReplaceChannel(RGB.G, MainChannel);
                replaceFilter.ApplyInPlace(bmp);

                onShowImage(bmp);

                //Bitmap Closure
                bitmap.UnlockBits(bmData);
                bitmapInRgbFormat.Dispose();
                bitmap.Dispose();
            }
            else if (_cmbSelectChannel.SelectedValue.ToString() == "2")
            {
                //Extracting Channel and Plotting New Bitmap - Channel B
                ExtractChannel extractFilter = new ExtractChannel(RGB.B);
                Bitmap bmp = new Bitmap(bmData.Width, bmData.Height, PixelFormat.Format32bppRgb);

                Bitmap MainChannel = extractFilter.Apply(bitmapInRgbFormat);
                ReplaceChannel replaceFilter = new ReplaceChannel(RGB.B, MainChannel);
                replaceFilter.ApplyInPlace(bmp);

                onShowImage(bmp);

                //Bitmap Closure
                bitmap.UnlockBits(bmData);
                bitmapInRgbFormat.Dispose();
                bitmap.Dispose();
            }
            else if (_cmbSelectChannel.SelectedValue.ToString() == "3")
            {
                //Extracting Channel and Plotting New Bitmap - Channel Y
                YCbCrExtractChannel extractFilter = new YCbCrExtractChannel(YCbCr.YIndex);
                Bitmap bmp = new Bitmap(bmData.Width, bmData.Height, PixelFormat.Format32bppRgb);

                Bitmap MainChannel = extractFilter.Apply(bitmapInRgbFormat);
                YCbCrReplaceChannel replaceFilter = new YCbCrReplaceChannel(YCbCr.YIndex, MainChannel);
                replaceFilter.ApplyInPlace(bmp);

                onShowImage(bmp);

                //Bitmap Closure
                bitmap.UnlockBits(bmData);
                bitmapInRgbFormat.Dispose();
                bitmap.Dispose();
            }
            else if (_cmbSelectChannel.SelectedValue.ToString() == "4")
            {
                //Extracting Channel and Plotting New Bitmap - Channel Cr
                YCbCrExtractChannel extractFilter = new YCbCrExtractChannel(YCbCr.CrIndex);
                Bitmap bmp = new Bitmap(bmData.Width, bmData.Height, PixelFormat.Format32bppRgb);

                Bitmap MainChannel = extractFilter.Apply(bitmapInRgbFormat);
                YCbCrReplaceChannel replaceFilter = new YCbCrReplaceChannel(YCbCr.CrIndex, MainChannel);
                replaceFilter.ApplyInPlace(bmp);

                onShowImage(bmp);

                //Bitmap Closure
                bitmap.UnlockBits(bmData);
                bitmapInRgbFormat.Dispose();
                bitmap.Dispose();
            }
            else if (_cmbSelectChannel.SelectedValue.ToString() == "5")
            {
                //Extracting Channel and Plotting New Bitmap - Channel Cb
                YCbCrExtractChannel extractFilter = new YCbCrExtractChannel(YCbCr.CbIndex);
                Bitmap bmp = new Bitmap(bmData.Width, bmData.Height, PixelFormat.Format32bppRgb);

                Bitmap MainChannel = extractFilter.Apply(bitmapInRgbFormat);
                YCbCrReplaceChannel replaceFilter = new YCbCrReplaceChannel(YCbCr.CbIndex, MainChannel);
                replaceFilter.ApplyInPlace(bmp);

                onShowImage(bmp);

                //Bitmap Closure
                bitmap.UnlockBits(bmData);
                bitmapInRgbFormat.Dispose();
                bitmap.Dispose();
            }
            else if (_cmbSelectChannel.SelectedValue.ToString() == "6")
            {
                //Extracting Channel and Plotting New Bitmap - Channel R + G
                ExtractChannel extractFilter = new ExtractChannel(RGB.R);
                Bitmap bmp = new Bitmap(bmData.Width, bmData.Height, PixelFormat.Format32bppRgb);

                Bitmap MainChannel = extractFilter.Apply(bitmapInRgbFormat);
                ReplaceChannel replaceFilter = new ReplaceChannel(RGB.R, MainChannel);
                replaceFilter.ApplyInPlace(bmp);

                ExtractChannel extractFilter2 = new ExtractChannel(RGB.G);

                MainChannel = extractFilter2.Apply(bitmapInRgbFormat);
                ReplaceChannel replaceFilter2 = new ReplaceChannel(RGB.G, MainChannel);
                replaceFilter2.ApplyInPlace(bmp);

                onShowImage(bmp);

                //Bitmap Closure
                bitmap.UnlockBits(bmData);
                bitmapInRgbFormat.Dispose();
                bitmap.Dispose();
            }
            else if (_cmbSelectChannel.SelectedValue.ToString() == "7")
            {
                //Extracting Channel and Plotting New Bitmap - Channel R + B
                ExtractChannel extractFilter = new ExtractChannel(RGB.R);
                Bitmap bmp = new Bitmap(bmData.Width, bmData.Height, PixelFormat.Format32bppRgb);

                Bitmap MainChannel = extractFilter.Apply(bitmapInRgbFormat);
                ReplaceChannel replaceFilter = new ReplaceChannel(RGB.R, MainChannel);
                replaceFilter.ApplyInPlace(bmp);

                ExtractChannel extractFilter2 = new ExtractChannel(RGB.B);

                MainChannel = extractFilter2.Apply(bitmapInRgbFormat);
                ReplaceChannel replaceFilter2 = new ReplaceChannel(RGB.B, MainChannel);
                replaceFilter2.ApplyInPlace(bmp);

                onShowImage(bmp);

                //Bitmap Closure
                bitmap.UnlockBits(bmData);
                bitmapInRgbFormat.Dispose();
                bitmap.Dispose();
            }
            else if (_cmbSelectChannel.SelectedValue.ToString() == "8")
            {
                //Extracting Channel and Plotting New Bitmap - Channel G + B
                ExtractChannel extractFilter = new ExtractChannel(RGB.G);
                Bitmap bmp = new Bitmap(bmData.Width, bmData.Height, PixelFormat.Format32bppRgb);

                Bitmap MainChannel = extractFilter.Apply(bitmapInRgbFormat);
                ReplaceChannel replaceFilter = new ReplaceChannel(RGB.G, MainChannel);
                replaceFilter.ApplyInPlace(bmp);

                ExtractChannel extractFilter2 = new ExtractChannel(RGB.B);

                MainChannel = extractFilter2.Apply(bitmapInRgbFormat);
                ReplaceChannel replaceFilter2 = new ReplaceChannel(RGB.B, MainChannel);
                replaceFilter2.ApplyInPlace(bmp);

                onShowImage(bmp);

                //Bitmap Closure
                bitmap.UnlockBits(bmData);
                bitmapInRgbFormat.Dispose();
                bitmap.Dispose();
            }
            else if (_cmbSelectChannel.SelectedValue.ToString() == "9")
            {
                //Extracting Channel and Plotting New Bitmap - Channel R + G + B
                ExtractChannel extractFilter = new ExtractChannel(RGB.R);
                Bitmap bmp = new Bitmap(bmData.Width, bmData.Height, PixelFormat.Format32bppRgb);

                Bitmap MainChannel = extractFilter.Apply(bitmapInRgbFormat);
                ReplaceChannel replaceFilter = new ReplaceChannel(RGB.R, MainChannel);
                replaceFilter.ApplyInPlace(bmp);

                ExtractChannel extractFilter2 = new ExtractChannel(RGB.G);

                MainChannel = extractFilter2.Apply(bitmapInRgbFormat);
                ReplaceChannel replaceFilter2 = new ReplaceChannel(RGB.G, MainChannel);
                replaceFilter2.ApplyInPlace(bmp);

                ExtractChannel extractFilter3 = new ExtractChannel(RGB.B);

                MainChannel = extractFilter3.Apply(bitmapInRgbFormat);
                ReplaceChannel replaceFilter3 = new ReplaceChannel(RGB.B, MainChannel);
                replaceFilter3.ApplyInPlace(bmp);

                onShowImage(bmp);

                //Bitmap Closure
                bitmap.UnlockBits(bmData);
                bitmapInRgbFormat.Dispose();
                bitmap.Dispose();
            }
            else if (_cmbSelectChannel.SelectedValue.ToString() == "10")
            {
                //Extracting Channel and Plotting New Bitmap - Channel Y + Cr
                YCbCrExtractChannel extractFilter = new YCbCrExtractChannel(YCbCr.YIndex);
                Bitmap bmp = new Bitmap(bmData.Width, bmData.Height, PixelFormat.Format32bppRgb);

                Bitmap MainChannel = extractFilter.Apply(bitmapInRgbFormat);
                YCbCrReplaceChannel replaceFilter = new YCbCrReplaceChannel(YCbCr.YIndex, MainChannel);
                replaceFilter.ApplyInPlace(bmp);

                YCbCrExtractChannel extractFilter2 = new YCbCrExtractChannel(YCbCr.CrIndex);
                MainChannel = extractFilter2.Apply(bitmapInRgbFormat);
                YCbCrReplaceChannel replaceFilter2 = new YCbCrReplaceChannel(YCbCr.CrIndex, MainChannel);
                replaceFilter2.ApplyInPlace(bmp);

                onShowImage(bmp);

                //Bitmap Closure
                bitmap.UnlockBits(bmData);
                bitmapInRgbFormat.Dispose();
                bitmap.Dispose();
            }
            else if (_cmbSelectChannel.SelectedValue.ToString() == "11")
            {
                //Extracting Channel and Plotting New Bitmap - Channel Y + Cb
                YCbCrExtractChannel extractFilter = new YCbCrExtractChannel(YCbCr.YIndex);
                Bitmap bmp = new Bitmap(bmData.Width, bmData.Height, PixelFormat.Format32bppRgb);

                Bitmap MainChannel = extractFilter.Apply(bitmapInRgbFormat);
                YCbCrReplaceChannel replaceFilter = new YCbCrReplaceChannel(YCbCr.YIndex, MainChannel);
                replaceFilter.ApplyInPlace(bmp);

                YCbCrExtractChannel extractFilter2 = new YCbCrExtractChannel(YCbCr.CbIndex);
                MainChannel = extractFilter2.Apply(bitmapInRgbFormat);
                YCbCrReplaceChannel replaceFilter2 = new YCbCrReplaceChannel(YCbCr.CbIndex, MainChannel);
                replaceFilter2.ApplyInPlace(bmp);

                onShowImage(bmp);

                //Bitmap Closure
                bitmap.UnlockBits(bmData);
                bitmapInRgbFormat.Dispose();
                bitmap.Dispose();
            }
            else if (_cmbSelectChannel.SelectedValue.ToString() == "12")
            {
                //Extracting Channel and Plotting New Bitmap - Channel Cr + Cb
                YCbCrExtractChannel extractFilter = new YCbCrExtractChannel(YCbCr.CrIndex);
                Bitmap bmp = new Bitmap(bmData.Width, bmData.Height, PixelFormat.Format32bppRgb);

                Bitmap MainChannel = extractFilter.Apply(bitmapInRgbFormat);
                YCbCrReplaceChannel replaceFilter = new YCbCrReplaceChannel(YCbCr.CrIndex, MainChannel);
                replaceFilter.ApplyInPlace(bmp);

                YCbCrExtractChannel extractFilter2 = new YCbCrExtractChannel(YCbCr.CbIndex);
                MainChannel = extractFilter2.Apply(bitmapInRgbFormat);
                YCbCrReplaceChannel replaceFilter2 = new YCbCrReplaceChannel(YCbCr.CbIndex, MainChannel);
                replaceFilter2.ApplyInPlace(bmp);

                onShowImage(bmp);

                //Bitmap Closure
                bitmap.UnlockBits(bmData);
                bitmapInRgbFormat.Dispose();
                bitmap.Dispose();
            }
            else if (_cmbSelectChannel.SelectedValue.ToString() == "13")
            {
                //Extracting Channel and Plotting New Bitmap - Channel Y + Cr + Cb
                YCbCrExtractChannel extractFilter = new YCbCrExtractChannel(YCbCr.YIndex);
                Bitmap bmp = new Bitmap(bmData.Width, bmData.Height, PixelFormat.Format32bppRgb);

                Bitmap MainChannel = extractFilter.Apply(bitmapInRgbFormat);
                YCbCrReplaceChannel replaceFilter = new YCbCrReplaceChannel(YCbCr.YIndex, MainChannel);
                replaceFilter.ApplyInPlace(bmp);

                YCbCrExtractChannel extractFilter2 = new YCbCrExtractChannel(YCbCr.CrIndex);
                MainChannel = extractFilter2.Apply(bitmapInRgbFormat);
                YCbCrReplaceChannel replaceFilter2 = new YCbCrReplaceChannel(YCbCr.CrIndex, MainChannel);
                replaceFilter2.ApplyInPlace(bmp);

                YCbCrExtractChannel extractFilter3 = new YCbCrExtractChannel(YCbCr.CbIndex);
                MainChannel = extractFilter3.Apply(bitmapInRgbFormat);
                YCbCrReplaceChannel replaceFilter3 = new YCbCrReplaceChannel(YCbCr.CbIndex, MainChannel);
                replaceFilter3.ApplyInPlace(bmp);

                onShowImage(bmp);

                //Bitmap Closure
                bitmap.UnlockBits(bmData);
                bitmapInRgbFormat.Dispose();
                bitmap.Dispose();
            }

        }

        private void _pcbImage_MouseDown(object sender, MouseEventArgs e)
        {
            // Set status of MouseDown
            mouseStatus = true;

            // Starting point of selection
            startPt.X = e.X;
            startPt.Y = e.Y;

            rctZoom = new Rectangle(new Point(e.X, e.Y), new Size());

            // Clear End point
            endPt.X = -1;
            endPt.Y = -1;

            
        }

        private void _pcbImage_MouseMove(object sender, MouseEventArgs e)
        {
            // Tracking Point
            Point trackingPt = new Point(e.X, e.Y);

            if (mouseStatus)
            {
                // Update last point. 
                endPt = trackingPt;

                // Set rectangle
                if (e.X > startPt.X && e.Y > startPt.Y)
                {
                    rctZoom.Width = e.X - startPt.X;
                    rctZoom.Height = e.Y - startPt.Y;
                }
                else if (e.X < startPt.X && e.Y > startPt.Y)
                {
                    rctZoom.Width = startPt.X - e.X;
                    rctZoom.Height = e.Y - startPt.Y;
                    rctZoom.X = e.X;
                    rctZoom.Y = startPt.Y;
                }
                else if (e.X > startPt.X && e.Y < startPt.Y)
                {
                    rctZoom.Width = e.X - startPt.X;
                    rctZoom.Height = startPt.Y - e.Y;

                    rctZoom.X = startPt.X;
                    rctZoom.Y = e.Y;
                }
                else
                {
                    rctZoom.Width = startPt.X - e.X;
                    rctZoom.Height = startPt.Y - e.Y;
                    rctZoom.X = e.X;
                    rctZoom.Y = e.Y;
                }
                _pcbImage.Refresh();
            }
        }

        private void _pcbImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (startPt.X != e.X &&
                startPt.Y != e.Y)
            {
                // Configuration for zoom off button
                noZoomBmp = new Bitmap(_pcbImage.Image);
                zoomBmpList.Add(noZoomBmp);

                if (mouseStatus)
                {
                    // Set mouseStatus off
                    mouseStatus = false;

                    // Set flags to initial state 
                    endPt.X = -1;
                    endPt.Y = -1;
                    startPt.X = -1;
                    startPt.Y = -1;
                    _pcbImage.Invalidate();

                    // Zoom Code
                    using (Bitmap sourceBitmap = new Bitmap(_pcbImage.Image, _pcbImage.Width, _pcbImage.Height))
                    {
                        using (Bitmap zoomBmp = new Bitmap(_pcbImage.Width, _pcbImage.Height))
                        {
                            using (Graphics g = Graphics.FromImage(zoomBmp))
                            {
                                g.DrawImage(sourceBitmap, new Rectangle(0, 0, _pcbImage.Width, _pcbImage.Height),
                            rctZoom, GraphicsUnit.Pixel);
                            }
                            Bitmap newZoomBmp = new Bitmap(zoomBmp);
                            onShowImage(newZoomBmp);
                        }
                    }

                }
            }
            else
            {
                if (mouseStatus)
                {
                    // Set mouseStatus off
                    mouseStatus = false;

                    // Set flags to initial state 
                    endPt.X = -1;
                    endPt.Y = -1;
                    startPt.X = -1;
                    startPt.Y = -1;
                    _pcbImage.Invalidate();
                }
            }
        }


        private void _pcbImage_Paint(object sender, PaintEventArgs e)
        {
            if (mouseStatus)
                e.Graphics.DrawRectangle(Pens.Black, rctZoom);
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (zoomBmpList.Count > 0)
            {
                onShowImage(zoomBmpList.Last());
                zoomBmpList.Remove(zoomBmpList.Last());
            }
                
        }



        #endregion Events


    }
}
