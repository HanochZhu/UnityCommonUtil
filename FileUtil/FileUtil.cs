/*
 * 创建日期：2017318
 * 文档工具类
 * 主要封装文档的读取和写入
 * 通用的接口主要表现为：
 * 读取： 输入一个字符串路径，返回对应文件的内容
 * 写入：输入字符串路径和修改的文件内容
 * 修改日期：2017.3.19
 * CGL
 * **/

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ZFramework
{
    public static class FileUtil
    {

        /// <summary>
        /// 获取当前工程文件所对应的文件目录,
        /// 注：这个方法需要放在awake或者start开始
        /// </summary>
        public static string StaticFileRootPath
        {
            get
            {
#if UNITY_ANDROID
            return "jar:file//" + Application.dataPath + "!/assets";
#elif UNITY_IPHONE
            return Application.dataPath + "/Raw";
#else
                return Application.dataPath + "/StreamingAssets";
#endif
            }
        }
#if UNITY_EDITOR_WIN && UNITY_STANDALONE_WIN
        /// <summary>
        /// 选择文件并返回文件字节流
        /// </summary>
        /// <returns>文件路径</returns>
        //public static byte[] ReadFileFromFolder()
        //{
        //    return ReadFileToBytes(OpenImageFormFolder());
        //}
        /// <summary>
        /// 从文件夹中选择一个文件，并返回对应的文件地址
        /// </summary>
        /// <returns></returns>
        //public static string OpenImageFormFolder()
        //{
        //    OpenFileName ofn = new OpenFileName();

        //    ofn.m_structSize = Marshal.SizeOf(ofn);

        //    ofn.m_filter = "All Files\0*.*\0\0";

        //    ofn.m_file = new string(new char[256]);

        //    ofn.m_maxFile = ofn.m_file.Length;

        //    ofn.m_fileTitle = new string(new char[64]);

        //    ofn.m_maxFileTitle = ofn.m_fileTitle.Length;

        //    ofn.m_initialDir = UnityEngine.Application.dataPath;//默认路径

        //    ofn.m_title = "Open Project";

        //    ofn.m_defExt = "JPG";//显示文件的类型
        //                         //注意 一下项目不一定要全选 但是0x00000008项不要缺少
        //    ofn.m_flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR

        //    if (WindowDll.GetOpenFileName(ofn))
        //    {
        //        Debug.Log("Selected file with full path: {0}" + ofn.m_file);
        //    }
        //    return ofn.m_file;//ReadFileToBytes();
        //}
#endif
        /// <summary>
        /// 输入带后缀的文件名称，在默认的根目录下读取文件。
        /// </summary>
        /// <param name="filename">输入文件名</param>
        /// <returns></returns>
        public static string GetFilePath(string filename)
        {
            return StaticFileRootPath + filename;
        }

        public static bool WriteFile(string path, string content)
        {
            return WriteFile(Path.GetDirectoryName(path), Path.GetFileName(path), content);

        }

        public static bool WriteFile(string path, string name, string content)
        {
            Debug.Log(path);
            bool writeSuccess = true;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                StreamWriter sw;
                FileInfo fi = new FileInfo(path + "/" + name);
                sw = fi.CreateText();
                
                //fi.Refresh();

                sw.WriteLine(content);
                sw.Close();
                sw.Dispose();
            }
            catch
            {
                writeSuccess = false;
                //同时需要输出ulog
            }
            return writeSuccess;
        }
        public static bool WriteFile(string path, string name,byte[] content)
        {
            bool success = true;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                File.WriteAllBytes(path + "/" + name,content);
            }
            catch (Exception)
            {
                success = false;
                throw;
            }
            return success;
        }
        /// <summary>
        /// 将内容隔行放置在文件尾
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool WriteFileAppendLine(string path, string content)
        {
            return WriteFileAppend(path, "\n" + content);
        }

        /// <summary>
        /// 将内容放置在文件尾
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool WriteFileAppend(string path, string content)
        {
            return true;
        }
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadFile(string path)
        {
            StreamReader sr;
            if (File.Exists(path))
            {
                try
                {
                    sr = File.OpenText(path);
                }
                catch(Exception e)
                {
                    
                    return null;
                }
                //final
            }
            else
            {
                return null;
            }
            string content = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return content;
        }
        public static IEnumerator LoadFileAsyn(string path, Action<string> callBack)
        {
            WWW www = new WWW(path);

            yield return www;
            if (!String.IsNullOrEmpty(www.error))
            {
                callBack(www.error);
            }
            else
            {
                callBack(www.text);
            }
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        public static void CreatFile(string path, string name)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.Create(path);
        }
        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        /// <summary>
        ///创建目录
        /// </summary>
        /// <param name="path"></param>
        public static void CreatDirector(string path)
        {
            if (Directory.Exists(path))//若存在
            {
                DeleteDirector(path);
            }
            Directory.CreateDirectory(path);
        }
        public static void DeleteDirector(string path)
        {
            //debug
            if (Directory.Exists(path))
            {
                Directory.Delete(path);
            }
        }
        /// <summary>
        /// 读取路径中的文件，并转化成二进制
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] ReadFileToBytes(string path)
        {
            FileStream fs = new FileStream(path,FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                return buffer;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                if(fs != null)
                {
                    fs.Dispose();
                    fs.Close();
                }
            }

        }

    }

}
