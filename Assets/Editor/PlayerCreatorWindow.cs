using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerCreatorWindow : EditorWindow
{
    public GameObject m_Manager;

    //Player Data Variables
    string m_Name = "";
    int m_HP = 0;
    int m_AttackDamage = 0;
    int m_MoveSpeed = 0;
    GameObject m_Weapon;

    //String List to convert show Scriptable Object array names
    List<string> m_Options = new List<string>();
    //String List display index
    int index = 0;

    [MenuItem("Window/PlayerCreatorWindow")]
    static void init()
    {
        PlayerCreatorWindow window = (PlayerCreatorWindow)EditorWindow.GetWindow(typeof(PlayerCreatorWindow));
        window.Show();
        window.m_Options.Add("None");
    }

    private PlayerData ConvertData()
    {
        PlayerData tempData = ScriptableObject.CreateInstance<PlayerData>();

        tempData.m_Name = m_Name;
        tempData.m_HP = m_HP;
        tempData.m_AttackDamage = m_AttackDamage;
        tempData.m_MoveSpeed = m_MoveSpeed;
        tempData.m_Weapon = m_Weapon;

        return tempData;
    }

    private void OnGUI()
    {
        GUILayout.Label("Select Player Creator Prefab");

        m_Manager = (GameObject)EditorGUILayout.ObjectField("Player Creator Prefab", m_Manager, typeof(GameObject), false);
        GUILayout.Space(5);

        if (m_Manager)
        {
            if (!m_Manager.GetComponent<PlayerCreator>())
            {
                //Object is not the player creator
                return;
            }
            m_Manager.GetComponent<PlayerCreator>().ManagedUpdate();
            CreateDropdown();
            EditorGUI.BeginChangeCheck();

            GUILayout.Label("Preset Options");
            index = EditorGUILayout.Popup(index, m_Options.ToArray());
            
            if(EditorGUI.EndChangeCheck())
            {
                if (index > 0)
                {
                    m_Name = m_Manager.GetComponent<PlayerCreator>().m_Data[index - 1].m_Name;
                    m_HP = m_Manager.GetComponent<PlayerCreator>().m_Data[index - 1].m_HP;
                    m_AttackDamage = m_Manager.GetComponent<PlayerCreator>().m_Data[index - 1].m_AttackDamage;
                    m_MoveSpeed = m_Manager.GetComponent<PlayerCreator>().m_Data[index - 1].m_MoveSpeed;
                    m_Weapon = m_Manager.GetComponent<PlayerCreator>().m_Data[index - 1].m_Weapon;
                }
                ResetData();
            }

            GUILayout.Space(5);

            m_Name = EditorGUILayout.TextField("Class Name", m_Name);
            m_HP = EditorGUILayout.IntSlider("HP:",m_HP, 0, 10);
            m_AttackDamage = EditorGUILayout.IntSlider("Attack Damage:", m_AttackDamage, 0, 10);
            m_MoveSpeed = EditorGUILayout.IntSlider("Move Speed:", m_MoveSpeed, 0, 10);
            m_Weapon = (GameObject)EditorGUILayout.ObjectField("Weapon Prefab", m_Weapon, typeof(GameObject), false);

            if (m_Weapon)
            {
                if (m_Weapon.GetComponent<Weapon>())
                {
                    if (GUILayout.Button("Create New Preset"))
                    {
                        if (DoesClassExist(m_Name))
                        {
                            if (EditorUtility.DisplayDialog("Class Already Exists", "A class of this name already exist, do you want to override it?", "Yes", "No"))
                            {
                                AssetDatabase.CreateAsset(ConvertData(), "Assets/Resources/Data/" + m_Name + ".asset");
                                AssetDatabase.SaveAssets();
                                AssetDatabase.Refresh();
                            }
                        }
                        else
                        {
                            AssetDatabase.CreateAsset(ConvertData(), "Assets/Resources/Data/" + m_Name + ".asset");
                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();
                        }

                    }

                }
            }
        }
    }

    private void CreateDropdown()
    {
        m_Options.Clear();
        m_Options.Add("None");
        for (int i = 0; i <m_Manager.GetComponent<PlayerCreator>().m_Data.Count; i++)
        {
            m_Options.Add(m_Manager.GetComponent<PlayerCreator>().m_Data[i].m_Name);
        }
    }

    private bool DoesClassExist(string _Name)
    {
        for (int i = 0; i < m_Options.Count; i++)
        {
            if (_Name == m_Options[i])
            {
                return true;
            }
        }

        return false;
    }
    private void ResetData()
    {
        //If there is preset is set to none
        if (index == 0)
        {
            m_Name = "";
            m_HP = 0;
            m_AttackDamage = 0;
            m_MoveSpeed = 0;
            m_Weapon = null;
        }
    }
}
