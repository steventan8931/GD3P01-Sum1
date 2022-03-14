using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    public List<PlayerData> m_Data;
    private PlayerData[] data2;

    [ExecuteAlways]
    public void ManagedUpdate()
    {
        //dataAssets = AssetDatabase.LoadAllAssetsAtPath("Assets/Data");
        data2 = Resources.LoadAll<PlayerData>("Data") as PlayerData[];
        m_Data.Clear();
        foreach (var data in data2)
        {
            m_Data.Add((PlayerData)data);
        }
    }
}

