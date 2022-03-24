using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingDummy : MonoBehaviour
{
    //Damage UI Popup
    public Text m_DamagePopup;
    private float m_DamageTimer = 0.0f;

    //Projectile Prefab
    public GameObject m_ProjectilePrefab;

    private void Update()
    {
        //If the popup is active, set a timer to turn it off
        if (m_DamagePopup.gameObject.activeInHierarchy)
        {
            m_DamageTimer += Time.deltaTime;
            if (m_DamageTimer > 0.5f)
            {
                m_DamagePopup.gameObject.SetActive(false);
                m_DamageTimer = 0.0f;
            }
        }

    }

    //Function to be called by the player's weapons
    public void TakeDamage(int _Damage)
    {
        //Set the popup to be active
        m_DamagePopup.gameObject.SetActive(true);
        //Change popup text to be the amount of damage the player dealt
        m_DamagePopup.text = _Damage.ToString();
    }

    //Spawns a projectile to hit the player
    public void Shoot()
    {
        GameObject obj = Instantiate(m_ProjectilePrefab, transform);
        obj.transform.parent = null;
    }
}
