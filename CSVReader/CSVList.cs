using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

// 以文件名为key，文件内容的结构为内容 组成的数据机构
// 为应对多个csv文件的情况
public class CSVList
{
    private static CSVList _instance;
    public static CSVList Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CSVList();
            }
            return _instance;
        }
    }

    private Dictionary<string, CSVReader> dicReader = new Dictionary<string, CSVReader>();

    public CSVReader ReadCSV(string path)
    {
        // 根据文件名来读取，可能会存在的情况是，文件名相同的时候，文件
        string name = Path.GetFileNameWithoutExtension(path);

        //如果已经存在当前的csv对象
        if (!dicReader.ContainsKey(name))
        {
            dicReader.Add(name, new CSVReader());

            dicReader[name].SetCSVFilePath(path);
        }
        return dicReader[name];
    }

    /// <summary>
    /// 
    /// update all file
    /// </summary>
    /// <returns></returns>
    public void UpdateCSVFile()
    {
        foreach (var item in dicReader)
        {
            item.Value.UpdateCSVReader();
        }
    }

    public void UpdateCSVFile(string filePath)
    {
        string name = Path.GetFileNameWithoutExtension(filePath);

        dicReader[name]?.UpdateCSVReader();
    }

    public CSVReader GetCSV(string filePath)
    {
        string name = Path.GetFileNameWithoutExtension(filePath);

        if (dicReader.ContainsKey(name))
        {
            return dicReader[name];
        }
        CDebug.CDebugErrorLog("Can not find the csv file in " + filePath);

        return null;
    }
}