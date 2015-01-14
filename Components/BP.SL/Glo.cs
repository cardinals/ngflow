﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Silverlight;
using System.IO;
using System.Windows.Media.Imaging;
using BP.CY.ExportAsPNG;

namespace BP.SL
{
    public class Glo
    {
        public const double 
            RECOMMAND_SCREEN_WIDTH = 1024
            , RECOMMAND_SCREEN_HEIGHT = 768;

        public static void DDL_BindDataTable(DataTable dt)
        {
        }

        public static Color ToColor(string colorName)
        {
            try
            {
                if (colorName.StartsWith("#"))
                    colorName = colorName.Replace("#", string.Empty);
                int v = int.Parse(colorName, System.Globalization.NumberStyles.HexNumber);
                return new Color()
                {
                    A = Convert.ToByte((v >> 24) & 255),
                    R = Convert.ToByte((v >> 16) & 255),
                    G = Convert.ToByte((v >> 8) & 255),
                    B = Convert.ToByte((v >> 0) & 255)
                };
            }
            catch
            {
                return Colors.Black;
            }
        }

        private static string _BPMHost = null;
        /// <summary>
        ///  Current App Address BPMHost 
        /// </summary>
        public static string BPMHost
        {
            get
            {
                if (_BPMHost != null)
                    return _BPMHost;
                else
                {
                    try
                    {
                        Uri uri = System.Windows.Browser.HtmlPage.Document.DocumentUri;
                        int index = uri.AbsoluteUri.IndexOf(uri.AbsolutePath, StringComparison.CurrentCultureIgnoreCase);
                        string url = uri.AbsoluteUri.Substring(0, index);

                        _BPMHost = url;
                    }
                    catch(Exception e)
                    {
                        throw new Exception(" Address translation error :" + e.Message,e);
                    }
                    return _BPMHost;
                }
            }
        }

        /// <summary>
        ///  Double-click time interval 
        /// </summary>
        public const int DoubleClickTime = 300;
        private static System.Windows.Threading.DispatcherTimer doubleClickTimer;
        public static bool IsDbClick
        {
            get
            {
                if (null == doubleClickTimer)
                {
                    doubleClickTimer = new System.Windows.Threading.DispatcherTimer();
                    doubleClickTimer.Interval = new TimeSpan(0, 0, 0, 0, DoubleClickTime);
                    doubleClickTimer.Tick += (object sender, EventArgs e) =>
                    {
                        doubleClickTimer.Stop();
                    };
                }

                if (doubleClickTimer.IsEnabled)
                {
                    doubleClickTimer.Stop();
                    return true;
                }
                else
                {
                    doubleClickTimer.Start();
                    return false;
                }
            }
        }

        /// <summary>
        ///  Silverlight Screenshot upload server element 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="DoType">1 :ShareFLow,2: ShareForm</param>
        /// <param name="fk_ID"></param>
        public static void SaveUIElementAsPng(UIElement element, int DoType, string fk_ID)
        {
            WriteableBitmap _bitmap = new WriteableBitmap(element, null);
            int width = _bitmap.PixelWidth;
            int height = _bitmap.PixelHeight;

            EditableImage ei = new EditableImage(width, height);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int pixel = _bitmap.Pixels[(i * width) + j];
                    ei.SetPixel(j, i,
                    (byte)((pixel >> 16) & 0xFF),
                    (byte)((pixel >> 8) & 0xFF),
                    (byte)(pixel & 0xFF),
                    (byte)((pixel >> 24) & 0xFF)
                    );
                }
            }
            // Get streaming 
            Stream png = ei.GetStream();

            WebClient uploadClient = new WebClient();

            // Connection open after the operation 
            uploadClient.OpenWriteCompleted += delegate(object sender, OpenWriteCompletedEventArgs e)
            {
                // To send the file to the server data stream 

                // e.UserState -  Need to upload stream （ Client flow ）
                using (Stream clientStream = e.UserState as Stream)
                {
                    if (null != clientStream)
                        using (Stream serverStream = e.Result)                    // e.Result -  Flow target address （ Streaming server ）
                        {
                            byte[] buffer = new byte[4096];
                            int readcount = 0;
                            // clientStream.Read -  Will need to upload the stream read into a byte array specified 
                            while ((readcount = clientStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                // serverStream.Write -  The array of bytes written to the destination address specified stream 
                                serverStream.Write(buffer, 0, readcount);
                            }
                        }
                }
            };

            string url = string.Empty;
            if (DoType == 1)
                url = "/WF/Admin/XAP/DoPort.aspx?DoType=UploadShare&FK_Flow=" + fk_ID;
            else if (DoType == 2)
                url = "/WF/Admin/XAP/DoPort.aspx?DoType=UploadShare&FK_MapData=" + fk_ID;

            if (!string.IsNullOrEmpty(url))
                uploadClient.OpenWriteAsync(new Uri(url, UriKind.Relative), "POST", png);   // Open upload connection 

        }

    }
}
