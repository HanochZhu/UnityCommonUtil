using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class CSVReader
{
    private string filepath;
    private CSVHandler handler;

    private bool isAlreadyRead = false;

    public CSVHandler Handler
    {
        get
        {
            if (!isAlreadyRead)
            {
                this.ReadCSV();
            }
            return handler;
        }
    }

    public void SetCSVFilePath(string path)
    {
        filepath = path;
        InitCSVReader(path);
    }

    public void InitCSVReader(string path)
    {
        filepath = path;
        //this
        handler = ReadCSV();
    }

    /// <summary>
    /// each update manuplate might cause immense gc oprate
    /// do it carefully
    /// 可能会造成大量的GC
    /// 谨慎操作
    /// </summary>
    public void UpdateCSVReader()
    {
        handler = null;

        handler = ReadCSV();
    }


    // csvContent: read from file
    // convert content to csv object
    private CSVHandler ReadCSV()
    {
        try
        {
            if (string.IsNullOrEmpty(filepath))
            {
                return null;
            }
            //get content of each line
            string[] lines = File.ReadAllLines(filepath);

            // store length value,
            // function call is luxury
            int length = lines.Length;

            // check if csv file is empty
            if (length <= 1)
            {
                CDebug.CDebugErrorLog(string.Format("csv file is empty :{0}", filepath));
                return null;
            }

            // remove the space in the first line
            string[] head = CSVHandler.GetHead(lines[0]);// '0'(magic number) point at first row of csv file 

            // csv object init
            CSVHandler hander = new CSVHandler();

            for (int i = 1; i < length; i++)
            {
                string line = lines[i];
                string[] chunks = line.Split(',');
                int chunkNum = chunks.Length;
                if (chunkNum > 1)
                {
                    string key = chunks[0];
                    for (int j = 1; j < chunkNum; j++)
                    {
                        // first chunk is key
                        CSVItem csvitem = new CSVItem();
                        // 为啥这里要用j-1呢
                        // 因为在处理head的时候，已经把第一个空数据处理了，因此这里需要-1
                        csvitem.AddMetaData(head[j - 1], chunks[j]);
                        hander.Add(key, csvitem);
                    }
                }
                else
                {
                    CDebug.CDebugErrorLog("CSV file is empty!");
                }
            }

            this.isAlreadyRead = true;

            return hander;

        }
        catch(Exception e)
        {
            CDebug.CDebugErrorLog(e);
            return null;
        }
        finally
        {
            CDebug.CDebugErrorLog("test finally");
        }
    }
}
