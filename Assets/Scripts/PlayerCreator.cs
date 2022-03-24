using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    //Scriptable Object Data
    public List<PlayerData> m_Data;
    //To load the array of player data
    PlayerData[] dataArray;

    public GameObject m_DefaultPlayerPrefab;

    [ExecuteAlways]
    public void ManagedUpdate()
    {
        //dataAssets = AssetDatabase.LoadAllAssetsAtPath("Assets/Data");
        dataArray = Resources.LoadAll<PlayerData>("Data") as PlayerData[];
        //Reset the list of player data
        m_Data.Clear();
        //Convert the array to a list
        foreach (var data in dataArray)
        {
            m_Data.Add((PlayerData)data);
        }
    }
}

