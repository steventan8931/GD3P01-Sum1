using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    //Creates list of string for the classes 
    public List<string> m_Classes = new List<string>();
    //Player Creator Prefab
    public PlayerCreator m_Manager;

    //Player Data and Character Loader
    public static PlayerData m_ChosenClass;
    public CharacterLoader m_Loader;

    //UI
    public Text m_ClassText;
    public int m_Index = 0;
    public Text m_DmgText;
    public Text m_HPText;
    public Text m_SpeedText;

    private void Awake()
    {
        m_Manager.ManagedUpdate();

        //Reset the list
        m_Classes.Clear();
        //Fill the list of strings with the data
        for (int i = 0; i < m_Manager.GetComponent<PlayerCreator>().m_Data.Count; i++)
        {
            m_Classes.Add(m_Manager.GetComponent<PlayerCreator>().m_Data[i].m_Name);
        }
        //Sets the chosen class as the index of the player data
        m_ChosenClass = m_Manager.GetComponent<PlayerCreator>().m_Data[0];
    }

    private void Update()
    {
        //Update UI 
        m_ClassText.text = m_Classes[m_Index];
        m_ChosenClass = m_Manager.GetComponent<PlayerCreator>().m_Data[m_Index];

        m_DmgText.text = m_Manager.GetComponent<PlayerCreator>().m_Data[m_Index].m_AttackDamage.ToString();
        m_HPText.text = m_Manager.GetComponent<PlayerCreator>().m_Data[m_Index].m_HP.ToString();
        m_SpeedText.text = m_Manager.GetComponent<PlayerCreator>().m_Data[m_Index].m_MoveSpeed.ToString();
    }

    //Changes index of the chosen class
    public void PreviousClass()
    {
        if (m_Index > 0)
        {
            m_Index--;
        }
    }

    //Changes index of the chosen class
    public void NextClass()
    {
        if (m_Index < m_Classes.Count - 1)
        {
            m_Index++;
        }
    }

    public void Play()
    {
        //Spawns the charcater with specified class
        m_Loader.LoadCharacter();
    }
}
