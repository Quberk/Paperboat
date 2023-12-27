using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class JsonReadWriteSystem
{
    public static void SaveToJson<T> (List<T> toSave, string fileName)
    {
        Debug.Log(GetPath(fileName));

        //Menambahkan kembali lagi data lama
        List<T> olderDatas = ReadListFromJson<T>(fileName);
        List<T> dataToSave = olderDatas;

        foreach (T s in toSave)
        {
            dataToSave.Add(s);
        }
            
        string content = JsonHelper.ToJson<T>(toSave.ToArray());
        WriteFile(GetPath(fileName), content);
    }

    public static void SaveToJsonAddNewItem<T>(T toSave, string fileName)
    {
        Debug.Log(GetPath(fileName));

        //Menambahkan kembali lagi data lama
        List<T> olderDatas = ReadListFromJson<T>(fileName);
        List<T> dataToSave = olderDatas;

        int index = 0;

        //Hanya untuk Skin
        foreach (T data in dataToSave)
        {
            if (!(data is SaveSkin))
                break;

            SaveSkin dataCompare = (SaveSkin)(object)data;
            SaveSkin toSaveCompare = (SaveSkin)(object)toSave;

            //Jika Id data yang ingin di Save ada yang sama dengan Data lama maka otomatis di return
            if (dataCompare.id == toSaveCompare.id )
            { 
                if (dataCompare.pesawatUnlocked != toSaveCompare.pesawatUnlocked)
                {
                    SaveSkin newSaveSkin = new SaveSkin(toSaveCompare.id, toSaveCompare.pesawatUnlocked);

                    T newObject = (T)(object)newSaveSkin;

                    dataToSave[index] = newObject;

                    string content1 = JsonHelper.ToJson<T>(dataToSave.ToArray());
                    WriteFile(GetPath(fileName), content1);
                }
                return;
            }

            index++;
        }

        dataToSave.Add(toSave);

        string content = JsonHelper.ToJson<T>(dataToSave.ToArray());
        WriteFile(GetPath(fileName), content);
    }

    public static void SaveToJsonOverwrite<T>(T toSave, string fileName)
    {
        Debug.Log(GetPath(fileName));
        string content = JsonUtility.ToJson(toSave);

        WriteFile(GetPath(fileName), content);
    }

    public static void SaveToJsonOverwrite<T>(T toSave, string fileName, bool LoadFromFirebase)
    {
        string content = JsonUtility.ToJson(toSave);

        WriteFile(fileName, content);
    }

    public static void SaveListToJsonOverwrite<T>(List<T> toSave, string filename, bool LoadFromFirebase)
    {
        string content = JsonHelper.ToJson<T>(toSave.ToArray());
        WriteFile(filename, content);
    }

    public static List<T> ReadListFromJson<T>(string fileName)
    {
        string content = ReadFile(GetPath(fileName));

        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return new List<T>();
        }

        List<T> res = JsonHelper.FromJson<T>(content).ToList<T>();
        return res;
    }

    public static T ReadFromJson<T>(string fileName)
    {
        string content = ReadFile(GetPath(fileName));

        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return default(T);
        }

        T res = JsonUtility.FromJson<T>(content);

        return res;
    }

    private static string GetPath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    private static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);

        }
    }

    private static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }

    public static void DeleteFile(string dataPath)
    {
        if (File.Exists(GetPath(dataPath)))
        {
            File.Delete(GetPath(dataPath));
        }
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
