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

public class CSVReader
{
    private string filepath;
    private CSVHandler handler;

    public CSVHandler Handler
    {
        get { return handler; }
    }

    public CSVReader()
    {

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

    public void UpdateCSVReader()
    {
        // 每一次update 可能会造成大量的GC
        // 谨慎操作
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

public class CSVHandler
{
    // row
    public Dictionary<string, CSVItem> dicItems;
    public List<string> keyList;


    public CSVHandler()
    {
        dicItems = new Dictionary<string, CSVItem>();
        keyList = new List<string>();
    }

    /// <summary>
    /// add pair<key,value>
    /// if key is already in dictionary, replace value with new value
    /// else add key and value in dictionary.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void AddAndReplace(string key, CSVItem value)
    {
        if (dicItems.ContainsKey(key))
        {
            dicItems[key] = value;
        }
        else
        {
            dicItems.Add(key, value);
            keyList.Add(key);
        }
    }

    /// <summary>
    /// if key do not contain in dictionary, add key and value in dictionary
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void AddWithoutReplace(string key,CSVItem value)
    {
        if (!dicItems.ContainsKey(key))
        {
            dicItems.Add(key, value);
        }
    }

    /// <summary>
    /// implement as AddAndReplace
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Add(string key,CSVItem value)
    {
        AddAndReplace(key,value);
    }

    public CSVItem this[string index]
    {
        get
        {
            if (dicItems.ContainsKey(index))
            {
                return dicItems[index];
            }
            // to avoid null exception error, null printer should avoid.
            // return an empty item object.
            return CSVItem.NullCSVItem;
        }
    }

    public CSVItem this[int index]
    {
        get
        {

            if (keyList.Count < index && index > 0)
            {
                string key = keyList[index];
                return this[key];
            }
            return CSVItem.NullCSVItem;
        }
    }

    // in addition to get item by keys(string),
    // it also provide visit by index, according to
    // order in csv file
    // todo
    /* public CSVItem this[int index]
    {
        get
        {

        }
    }*/

    //-------------------static method---------------
    public static string[] GetHead(string headline)
    {
        // formate: " ,name1,name2,name3,..,\n"
        if (headline.Length <= 1)
        {
            CDebug.CDebugErrorLog("headline is empty");
            return null;
        }

        string[] headitems = headline.Split(',');
        int headitemNum = headitems.Length;
        if (headitemNum <= 1)
        {
            CDebug.CDebugErrorLog("String format of headline is't correct");
            return null;
        }
        // delete first item in items;
        string[] headitemExpelFirst = new string[headitemNum - 1];

        try
        {
            // sourcearray, sourceindex,destinationarray, destinationindex,length
            Array.Copy(headitems, 1, headitemExpelFirst, 0, headitemNum - 1);
        }
        catch (Exception ex)
        {
            CDebug.CDebugErrorLog(ex.ToString());
        }
        return headitemExpelFirst;
    }

    public void Dispose()
    {
        dicItems = null;
        keyList = null;
    }
}

public class CSVItem
{
    private string name;
    // each column
    public Dictionary<string, CSVMetaData> dicmetaData;

    public List<string> keyList;

    public static CSVItem NullCSVItem = new CSVItem();

    public CSVItem()
    {
        dicmetaData = new Dictionary<string, CSVMetaData>();
        keyList = new List<string>();
    }

    public CSVMetaData this[string index]
    {
        get
        {
            return dicmetaData[index];
        }
    }

    /// <summary>
    ///  通过整数索引来获取值，从0开始
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public CSVMetaData this[int index]
    {
        get
        {
            if (keyList.Count > index && index > 0)
            {
                string key = keyList[index];
                if (dicmetaData.ContainsKey(key))
                {
                    return dicmetaData[key];
                }
            }
            return CSVMetaData.NULLMetaData;
        }
    }

    public void AddMetaData(string key, string content)
    {
        CSVMetaData meta = new CSVMetaData();
        meta.MetaData = content;
        if (dicmetaData.ContainsKey(key))
        {
            dicmetaData[key] = meta;
        }
        else
        {
            dicmetaData.Add(key,meta);
            keyList.Add(key);
        }
    }
}

public class CSVMetaData
{
    public static CSVMetaData NULLMetaData;

    private string metadata;

    public string MetaData
    {
        get
        {
            return metadata;
        }
        set
        {
            metadata = value;
        }
    }

    public float ToFloat()
    {
        float outresult = 0.0f;
        if (!float.TryParse(metadata,out outresult))
        {
            return outresult;
        }
        CDebug.CDebugErrorLog(string.Format("{0} can not translate to float", metadata));
        return float.NaN;
    }

    public float ToInt()
    {
        int outresult = 0;
        if (int.TryParse(metadata,out outresult))
        {
            return outresult;
        }
        CDebug.CDebugErrorLog(string.Format("{0} can not translate to int", metadata));
        // exception might caused client crash?
        // throw new System.Exception(string.Format("{0} can not translate to int", metadata));
        return 0;
    }

    public override string ToString()
    {
        return metadata;
    }

}