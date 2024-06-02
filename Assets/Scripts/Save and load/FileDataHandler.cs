using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class FileDataHandler
{
    private string dataDirPath ="" ; 
    private string dataFileName = "";

    public FileDataHandler( string dataDirPath, string dataFileName)
    {
        this.dataDirPath=dataDirPath;
        this.dataFileName = dataFileName;
    }

    public WishData Load()
    {
        //use path.combine to account for diffrent OS's having diffrent path separators
        string fullPath = Path.Combine(dataDirPath,dataFileName);

        WishData loadedData = null;
        if (File.Exists(fullPath))
        {
        try
        {
            // Load the serialized data from the file
            string dataToLoad = "";
            using ( FileStream stream = new FileStream(fullPath,FileMode.Open))
            {
                using(StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }

            loadedData = JsonUtility.FromJson<WishData>(dataToLoad);
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }

        }
        return loadedData;
    }

    public void Save(WishData data)
    {
        //use path.combine to account for diffrent OS's having diffrent path separators
        string fullPath = Path.Combine(dataDirPath,dataFileName);
        Debug.Log(fullPath);
        try
        {
            // create the dictionery the file will be written to if it dosen't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));


            // serialize the c# game data object into Json
            string dataToStore = JsonUtility.ToJson(data,true);

            // write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath,FileMode.Create))
            {
                using ( StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }
}
