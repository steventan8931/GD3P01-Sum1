using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    //Player Creator Prefab
    PlayerCreator m_Manager;
    //Previous created character
    GameObject m_LastCharacter;

    private void Awake()
    {
        //Find reference to the player creator prefab
        m_Manager = FindObjectOfType<PlayerCreator>();
    }

    public void LoadCharacter()
    {
        if (CharacterSelect.m_ChosenClass)
        {
            //If there is already a character loaded
            if (m_LastCharacter)
            {
                //Delete the last character
                Destroy(m_LastCharacter);
            }

            //Create an instance of the default player prefab from the player creator 
            GameObject newObj = Instantiate(m_Manager.m_DefaultPlayerPrefab, transform);

            //Assign the data of the chosen character class to this created player
            newObj.GetComponent<PlayerCharacter>().m_Data = CharacterSelect.m_ChosenClass;
            //Spawn the weapon and attach it to the player
            Instantiate(newObj.GetComponent<PlayerCharacter>().m_Data.m_Weapon, newObj.GetComponent<PlayerCharacter>().m_HandPosition);
            //Detach the player from the character loader object
            newObj.transform.parent = null;
            
            //Set the last character reference to the created player
            m_LastCharacter = newObj;
        }
    }

}
