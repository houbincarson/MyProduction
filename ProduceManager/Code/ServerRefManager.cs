using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;


namespace ProduceManager
{
    public abstract class ServerRefManager
    {
        private readonly static object Syn = new object();
        private readonly static object SynProd = new object();
        public static PrdManager.ShareWSSoapClient PrdManagerSoapClient = null;
        /// <summary>
        /// 图片存放路径，保存在App.config，只读
        /// </summary>
        public static string frmImageFilePath = System.Configuration.ConfigurationManager.AppSettings["ImageFilePath"];
        public static string frmImageReadFilePath = ConfigurationManager.AppSettings["ReadFile"];

        public static PrdManager.ShareWSSoapClient GetPrdManager()
        {
            lock (Syn)
            {
                LogisticsSeverRefAbort();
                return PrdManagerSoapClient;
            }
        }
        private static void LogisticsSeverRefAbort()
        {
            lock (SynProd)
            {
                if (PrdManagerSoapClient == null)
                {
                    PrdManagerSoapClient = new PrdManager.ShareWSSoapClient();
                    //PrdManagerSoapClient.InnerChannel.Close();
                    //PrdManagerSoapClient.Abort();
                    //PrdManagerSoapClient = null;
                    //System.GC.Collect();
                }
            }
        }
        /// <summary>
        /// 获取图片文件
        /// </summary>
        /// <param name="StylePic"></param>
        /// <param name="Pic_Version"></param>
        /// <returns></returns>
        public static byte[] PicFileRead(string StylePic, string Pic_Version)
        {
            if (StylePic == string.Empty || Pic_Version == string.Empty)
                return null;

            string AppPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string AllPath = AppPath + frmImageFilePath;
            string PicFileName = string.Format("{0}\\{1}_ver{2}", AllPath, StylePic, Pic_Version);

            byte[] retBytes = null;
            if (!Directory.Exists(AllPath))
            {
                Directory.CreateDirectory(AllPath);
            }
            if (File.Exists(PicFileName))
            {
                retBytes = GetbytesFromFile(PicFileName);
            }
            else
            {
                if (PrdManagerSoapClient != null)
                {
                    try
                    {
                        retBytes = PrdManagerSoapClient.FileRead(frmImageReadFilePath, StylePic);
                        string[] oldPics = Directory.GetFiles(string.Format("{0}\\", AllPath), string.Format("{0}_ver*", StylePic));
                        if (retBytes != null)
                        {
                            foreach (string o in oldPics)
                            {
                                File.SetAttributes(o, FileAttributes.Normal);
                                File.Delete(o);
                            }
                            File.WriteAllBytes(PicFileName, retBytes);
                        }
                        else
                        {
                            if (oldPics.Length > 0)
                            {
                                retBytes = GetbytesFromFile(oldPics[0]);
                            }
                        }
                    }
                    catch (ObjectDisposedException)
                    {
                    }
                }
            }
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            return retBytes;
        }

        public static byte[] GetbytesFromFile(string FileName)
        {
            byte[] retBytes = null;
            FileStream tp_Fs = File.OpenRead(FileName);
            using (MemoryStream ms = new MemoryStream())
            {
                int b;
                while ((b = tp_Fs.ReadByte()) != -1)
                {
                    ms.WriteByte((byte)b);
                }
                retBytes = ms.ToArray();
            }
            tp_Fs.Close();
            tp_Fs.Dispose();
            ReleaseObj(tp_Fs);
            return retBytes;
        }
        private static void ReleaseObj(object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            catch { }
            finally { o = null; }
        }

        /// <summary>
        /// 文件上传至服务器
        /// </summary>
        /// <param name="FileName">保存到服务器上的图片文件名称</param>
        /// <param name="JpgFileName">本地图片文件名称</param>
        /// <param name="IsDel">是否删除本地文件</param>        
        public static void FileSave(string FileName, string JpgFileName, bool IsDel)
        {
            if (JpgFileName == null)
            {
                return;
            }
            if (PrdManagerSoapClient != null)
            {
                FileStream tp_Fs = File.OpenRead(JpgFileName);
                byte[] retBytes = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    int b;
                    while ((b = tp_Fs.ReadByte()) != -1)
                    {
                        ms.WriteByte((byte)b);
                    }
                    retBytes = ms.ToArray();
                }
                PrdManagerSoapClient.FileSave(retBytes, FileName,
                     ConfigurationManager.AppSettings["ImgFile"],
                     ConfigurationManager.AppSettings["BigImgFile"],
                     ConfigurationManager.AppSettings["SmallImgFile"],
                     ConfigurationManager.AppSettings["RecentSmallFile"],
                     ConfigurationManager.AppSettings["RecentBigFile"],
                     ConfigurationManager.AppSettings["RecentOrgFile"]);
                tp_Fs.Close();
                retBytes = null;
                if (IsDel)
                {
                    File.SetAttributes(JpgFileName, FileAttributes.Archive);
                    File.Delete(JpgFileName);
                }
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
        }
    }
}
