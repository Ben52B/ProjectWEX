using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPresistence
{

    void LoadData (WishData data);
    void SaveData (ref WishData data);

}
