using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace AutoUpdate
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    try
        //    {
        //        if (!FileZipUnZip.Exists())
        //        {
        //            Console.WriteLine("未安装WinRAR,请先安装，按任意键退出...");
        //            Console.ReadKey();
        //            return;
        //        }

        //        string strPath = System.Environment.CurrentDirectory; //E:\公司项目文档\WebService\LogisticsManager\bin\Debug
        //        string strAppName = args[0];// "LogisticsManager";

        //        Console.Write("成功下载更新文件压缩包，开始更新...");
        //        Thread.Sleep(5000);
        //        if (!FileZipUnZip.UnRAR(strPath, strPath, strAppName + ".rar"))
        //        {
        //            Console.Write(" 更新失败，按任意键退出...");
        //            Console.ReadKey();
        //            return;
        //        }
        //        //if (!FileZipUnZip.UnRAR(strPath + "\\UpdateTemp", strPath, strAppName + ".rar"))
        //        //{
        //        //    Console.Write(" 解压失败，按任意键退出...");
        //        //    Console.ReadKey();
        //        //    return;
        //        //}
        //        Console.Write("    更新成功.");
        //        Console.WriteLine();

        //        //Console.Write("更新文件...");
        //        //Thread.Sleep(5000);
        //        //foreach (string strFile in Directory.GetFiles(strPath + "\\UpdateTemp"))
        //        //{
        //        //    FileInfo f = new FileInfo(strFile);
        //        //    File.SetAttributes(strFile, FileAttributes.Normal);
        //        //    if (File.Exists(strPath + "\\" + f.Name))
        //        //    {
        //        //        File.SetAttributes(strPath + "\\" + f.Name, FileAttributes.Normal);
        //        //        File.Delete(strPath + "\\" + f.Name);
        //        //    }
        //        //    File.Move(strFile, strPath + "\\" + f.Name);
        //        //}
        //        //Directory.Delete(strPath + "\\UpdateTemp", true);
        //        //Console.Write("    更新成功.");
        //        //Console.WriteLine();

        //        Console.WriteLine("按任意键运行新程序...");
        //        Console.ReadKey();
        //        System.Diagnostics.Process.Start(strAppName + ".exe");
        //    }
        //    catch (Exception err)
        //    {
        //        Console.WriteLine("更新出错：" + err.Message + "按任意键退出更新...");
        //        Console.ReadKey();
        //        return;
        //    }
        //}



        static void Main(string[] args)
        {
            try
            {
                //if (!FileZipUnZip.Exists())
                //{
                //    Console.WriteLine("未安装WinRAR,请先安装，按任意键退出...");
                //    Console.ReadKey();
                //    return;
                //}

                string strPath = System.Environment.CurrentDirectory; //E:\公司项目文档\WebService\LogisticsManager\bin\Debug
                string strAppName = args[0];// "LogisticsManager";

                Console.Write("成功下载更新文件压缩包，开始更新...");
                Thread.Sleep(5000);

                if (!FileZipUnZip.CompressFile(strPath + "\\" + strAppName + ".zip", strPath))
                {
                    Console.Write(" 更新失败，按任意键退出..." + strPath + "\\" + strAppName + ".zip");

                    Console.ReadKey();
                    return;
                }
                //if (!FileZipUnZip.UnRAR(strPath, strPath, strAppName + ".rar"))
                //{
                //    Console.Write(" 更新失败，按任意键退出...");
                //    Console.ReadKey();
                //    return;
                //}
                //if (!FileZipUnZip.UnRAR(strPath + "\\UpdateTemp", strPath, strAppName + ".rar"))
                //{
                //    Console.Write(" 解压失败，按任意键退出...");
                //    Console.ReadKey();
                //    return;
                //}
                Console.Write("    更新成功.");
                Console.WriteLine();

                //Console.Write("更新文件...");
                //Thread.Sleep(5000);
                //foreach (string strFile in Directory.GetFiles(strPath + "\\UpdateTemp"))
                //{
                //    FileInfo f = new FileInfo(strFile);
                //    File.SetAttributes(strFile, FileAttributes.Normal);
                //    if (File.Exists(strPath + "\\" + f.Name))
                //    {
                //        File.SetAttributes(strPath + "\\" + f.Name, FileAttributes.Normal);
                //        File.Delete(strPath + "\\" + f.Name);
                //    }
                //    File.Move(strFile, strPath + "\\" + f.Name);
                //}
                //Directory.Delete(strPath + "\\UpdateTemp", true);
                //Console.Write("    更新成功.");
                //Console.WriteLine();

                Console.WriteLine("按任意键运行新程序...");
                Console.ReadKey();
                System.Diagnostics.Process.Start(strAppName + ".exe");
            }
            catch (Exception err)
            {
                Console.WriteLine("更新出错：" + err.Message + "按任意键退出更新...");
                Console.ReadKey();
                return;
            }
        }
    }
}
