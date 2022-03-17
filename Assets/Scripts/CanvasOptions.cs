using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasOptions : MonoBehaviour
{
    public Canvas m_Canvas;
    private bool m_Open = true;
    public Text m_CharacterSelectTab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            m_Open = !m_Open;
        }

        m_Canvas.enabled = m_Open;
        if (m_Canvas.enabled)
        {
            m_CharacterSelectTab.text = "Press TAB to Close";
        }
        else
        {
            m_CharacterSelectTab.text = "Press TAB to Open";
        }
    }

}
