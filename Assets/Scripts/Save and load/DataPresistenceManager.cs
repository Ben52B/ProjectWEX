using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPresistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private WishData wishData;
    private List<IDataPresistence> dataPresistencesObjects;
    private FileDataHandler dataHandler;
    public static DataPresistenceManager instance {get; private set;}

    private void Awake()
    {
        if (instance!=null)
        {
            Debug.Log(instance);
            Debug.LogError("Found more than one Data Presistence Manager in the scene.");
        }
        instance=this;
    }

    private void Start()
    {
        this.dataHandler= new FileDataHandler(Application.persistentDataPath,fileName);
        this.dataPresistencesObjects = FindAllDataPresistenceObjects();
        LoadWish();
    }
    public void NewWish()
    {
        this.wishData= new WishData();
    }
    public void LoadWish()
    {
 

        this.wishData = dataHandler.Load();
        // if no data can be loaded, initialize to new game
        if (this.wishData == null)
        {
            Debug.Log("No previuse wish was found. Initializing new wish.");
            NewWish();
        }
        //push loaded data
        foreach (IDataPresistence dataPresistenceObj in dataPresistencesObjects)
        {
            dataPresistenceObj.LoadData(wishData);
        }

        Debug.Log("load is working");
    }

    public void SaveWish()
    {
        // save data to cloud or file
        foreach (IDataPresistence dataPresistenceObj in dataPresistencesObjects)
        {
            dataPresistenceObj.SaveData(ref wishData);
        }

        Debug.Log("Save is working");

        // save that data to a file using the data handler
        dataHandler.Save(wishData);
    }
 
    private List<IDataPresistence> FindAllDataPresistenceObjects()
    {
        IEnumerable<IDataPresistence> dataPresistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID).OfType<IDataPresistence>();

        return new List<IDataPresistence>(dataPresistenceObjects);
    }




}
