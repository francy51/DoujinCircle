using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class JsonHelper<T> where T : class
{

    public static T CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<T>(jsonString);
    }

    public static void SaveToJson(T obj,string path)
    {
        string json = JsonUtility.ToJson(obj,true);
        File.WriteAllText(path, json);
    }



}
