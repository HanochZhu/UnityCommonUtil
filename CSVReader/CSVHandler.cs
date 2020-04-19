using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVHandler:IEnumerable
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
    public void AddWithoutReplace(string key, CSVItem value)
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
    public void Add(string key, CSVItem value)
    {
        AddAndReplace(key, value);
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

    public IEnumerator GetEnumerator()
    {
        return (IEnumerator)dicItems.GetEnumerator(); 
    }
}

public class CSVItem:IEnumerable
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
            dicmetaData.Add(key, meta);
            keyList.Add(key);
        }
    }

    public IEnumerator GetEnumerator()
    {
        return (IEnumerator)dicmetaData.GetEnumerator();
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
        if (!float.TryParse(metadata, out outresult))
        {
            return outresult;
        }
        CDebug.CDebugErrorLog(string.Format("{0} can not translate to float", metadata));
        return float.NaN;
    }

    public float ToInt()
    {
        int outresult = 0;
        if (int.TryParse(metadata, out outresult))
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

    // implicit conversion from csvmetadata to string
    public static implicit operator string(CSVMetaData meta) => meta.ToString();
    // string a = meta // implicit
    // string a = (string)meta // explicit
}