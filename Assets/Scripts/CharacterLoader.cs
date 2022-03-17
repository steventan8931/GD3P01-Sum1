using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    PlayerCreator m_Manager;
    GameObject lastCharacter;

    private void Awake()
    {
        m_Manager = FindObjectOfType<PlayerCreator>();
    }

    public void LoadCharacter()
    {
        if (CharacterSelect.m_ChosenClass)
        {
            //If there is already a charcater loaded
            if (lastCharacter)
            {
                Destroy(lastCharacter);
            }

            GameObject newObj = Instantiate(m_Manager.m_DefaultPlayerPrefab, transform);

            newObj.GetComponent<PlayerCharacter>().m_Data = CharacterSelect.m_ChosenClass;
            Instantiate(newObj.GetComponent<PlayerCharacter>().m_Data.m_Weapon, newObj.GetComponent<PlayerCharacter>().m_HandPosition);
            newObj.transform.parent = null;
            lastCharacter = newObj;
        }
    }

    public void Update()
    {

    }
}
