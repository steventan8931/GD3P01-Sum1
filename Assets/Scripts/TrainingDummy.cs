using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingDummy : MonoBehaviour
{
    public Text m_DamagePopup;
    private float m_DamageTimer = 0.0f;

    public GameObject m_ProjectilePrefab;

    private void Update()
    {
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

    public void TakeDamage(int _Damage)
    {
        m_DamagePopup.gameObject.SetActive(true);
        m_DamagePopup.text = _Damage.ToString();
    }

    public void Shoot()
    {
        GameObject obj = Instantiate(m_ProjectilePrefab, transform);
        obj.transform.parent = null;
    }
}
