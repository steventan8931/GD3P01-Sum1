using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public List<string> m_Classes = new List<string>();
    public PlayerCreator m_Manager;

    public static PlayerData m_ChosenClass;
    public CharacterLoader m_Loader;

    public Text m_ClassText;
    public int m_Index = 0;

    public Text m_DmgText;
    public Text m_HPText;
    public Text m_SpeedText;

    private void Awake()
    {
        m_Manager.ManagedUpdate();

        m_Classes.Clear();
        for (int i = 0; i < m_Manager.GetComponent<PlayerCreator>().m_Data.Count; i++)
        {
            m_Classes.Add(m_Manager.GetComponent<PlayerCreator>().m_Data[i].m_Name);
        }

        m_ChosenClass = m_Manager.GetComponent<PlayerCreator>().m_Data[0];
    }

    private void Update()
    {
        m_ClassText.text = m_Classes[m_Index];
        m_ChosenClass = m_Manager.GetComponent<PlayerCreator>().m_Data[m_Index];

        m_DmgText.text = m_Manager.GetComponent<PlayerCreator>().m_Data[m_Index].m_AttackDamage.ToString();
        m_HPText.text = m_Manager.GetComponent<PlayerCreator>().m_Data[m_Index].m_HP.ToString();
        m_SpeedText.text = m_Manager.GetComponent<PlayerCreator>().m_Data[m_Index].m_MoveSpeed.ToString();
    }

    public void PreviousClass()
    {
        if (m_Index > 0)
        {
            m_Index--;
        }
    }

    public void NextClass()
    {
        if (m_Index < m_Classes.Count - 1)
        {
            m_Index++;
        }
    }

    public void Play()
    {
        m_Loader.LoadCharacter();
    }
}
