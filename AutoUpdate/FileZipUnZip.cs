using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using Microsoft.Win32;
using System.Diagnostics;
using ICSharpCode.SharpZipLib.Zip;
namespace AutoUpdate
{
    public class FileZipUnZip
    {
        /// <summary>
        /// 是否安装了Winrar
        /// </summary>
        /// <returns></returns>
        public static bool Exists()
        {
            RegistryKey the_Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
            if (the_Reg == null)
                return false;
            return !string.IsNullOrEmpty(the_Reg.GetValue("").ToString());
        }

        /// <summary>
        /// 利用 WinRAR 进行压缩
        /// </summary>
        /// <param name="path">将要被压缩的文件夹（绝对路径）</param>
        /// <param name="rarPath">压缩后的 .rar 的存放目录（绝对路径）</param>
        /// <param name="rarName">压缩文件的名称（包括后缀）</param>
        /// <returns>true 或 false。压缩成功返回 true，反之，false。</returns>
        public static bool RAR(string path, string rarPath, string rarName)
        {
            bool flag = false;
            string rarexe;       //WinRAR.exe 的完整路径
            RegistryKey regkey;  //注册表键
            Object regvalue;     //键值
            string cmd;          //WinRAR 命令参数
            ProcessStartInfo startinfo;
            Process process;
            try
            {
                regkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
                regvalue = regkey.GetValue("");
                rarexe = regvalue.ToString();
                regkey.Close();

                //regkey = Registry.ClassesRoot.OpenSubKey(@"Applications\WinRAR.exe\shell\open\command");
                //regvalue = regkey.GetValue("");  // 键值为 "d:\Program Files\WinRAR\WinRAR.exe" "%1"
                //rarexe = regvalue.ToString();
                //regkey.Close();
                //rarexe = rarexe.Substring(1, rarexe.Length - 7);  // d:\Program Files\WinRAR\WinRAR.exe

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                //压缩命令，相当于在要压缩的文件夹(path)上点右键->WinRAR->添加到压缩文件->输入压缩文件名(rarName)
                cmd = string.Format("a {0} {1} -r",
                                    rarName,
                                    path);
                startinfo = new ProcessStartInfo();
                startinfo.FileName = rarexe;
                startinfo.Arguments = cmd;                          //设置命令参数
                startinfo.WindowStyle = ProcessWindowStyle.Hidden;  //隐藏 WinRAR 窗口

                startinfo.WorkingDirectory = rarPath;
                process = new Process();
                process.StartInfo = startinfo;
                process.Start();
                process.WaitForExit(); //无限期等待进程 winrar.exe 退出
                if (process.HasExited)
                {
                    flag = true;
                }
                process.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            return flag;
        }

        /// <summary>
        /// 利用 WinRAR 进行解压缩
        /// </summary>
        /// <param name="path">文件解压路径（绝对）</param>
        /// <param name="rarPath">将要解压缩的 .rar 文件的存放目录（绝对路径）</param>
        /// <param name="rarName">将要解压缩的 .rar 文件名（包括后缀）</param>
        /// <returns>true 或 false。解压缩成功返回 true，反之，false。</returns>
        public static bool UnRAR(string path, string rarPath, string rarName)
        {
            bool flag = false;
            string rarexe;
            RegistryKey regkey;
            Object regvalue;
            string cmd;
            ProcessStartInfo startinfo;
            Process process;
            try
            {
                regkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
                regvalue = regkey.GetValue("");
                rarexe = regvalue.ToString();
                regkey.Close();

                //regkey = Registry.ClassesRoot.OpenSubKey(@"Applications\WinRAR.exe\shell\open\command");
                //regvalue = regkey.GetValue("");
                //rarexe = regvalue.ToString();
                //regkey.Close();
                //rarexe = rarexe.Substring(1, rarexe.Length - 7);


                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //处理目录中的空格
                if (path.IndexOf(' ') != -1)
                    path = "\"" + path + "\"";
                if (rarPath.IndexOf(' ') != -1)
                    rarPath = "\"" + rarPath + "\"";
                if (rarName.IndexOf(' ') != -1)
                    rarName = "\"" + rarName + "\"";

                //解压缩命令，相当于在要压缩文件(rarName)上点右键->WinRAR->解压文件->目标路径(path)
                cmd = string.Format("x {0} {1} -y",
                                    rarName,
                                    path);
                startinfo = new ProcessStartInfo();
                startinfo.FileName = rarexe;
                startinfo.Arguments = cmd;
                startinfo.WindowStyle = ProcessWindowStyle.Hidden;

                startinfo.WorkingDirectory = rarPath;
                process = new Process();
                process.StartInfo = startinfo;
                process.Start();
                process.WaitForExit();
                if (process.HasExited)
                {
                    flag = true;
                }
                process.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            return flag;
        }


        /// <summary>文件解压方法    
        /// 
        /// </summary>
        /// <param name="zipFilePath">要解压的文件的名称的路径，ex:Application.StartupPath + "\\UploadFile\\CetMod-00000015551.rar"</param>
        /// <param name="targetDir">解压到哪个文件，ex：Application.StartupPath + "\\UploadFile\\"</param>
        public static bool CompressFile(string zipFilePath, string targetDir)
        {
            bool flag = false;
            try
            {
                ICSharpCode.SharpZipLib.Zip.FastZipEvents Fz = new FastZipEvents();
                FastZip fs = new FastZip(Fz);
                fs.ExtractZip(zipFilePath, targetDir, "");
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                flag = true;
            }
            return flag;
        }
    }
}


