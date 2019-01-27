using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public RectTransform m_curtainL;
    public RectTransform m_curtainR;
    public GameObject m_background;
    public GameObject m_titleelement;

    public float m_AnimTime;
    private bool m_playPressed;
    private float m_animFactor;
    private bool m_sceneLoaded;
    // Start is called before the first frame update
    void Start()
    {
        m_playPressed = false;
        m_sceneLoaded = false;
        m_animFactor = (m_curtainR.rect.width - 50) /m_AnimTime;
    }

    public void OnPlayPressed()
    {
        m_playPressed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_playPressed)
        {
            m_AnimTime -= Time.deltaTime;

            if (m_AnimTime < 2)
            {
                m_background.SetActive(false);
                m_titleelement.SetActive(false);
            }

            if(m_AnimTime < 5 && !m_sceneLoaded)
            {
                SceneManager.LoadScene("main", LoadSceneMode.Additive);
                m_sceneLoaded = true;
            }

            if (m_AnimTime <= 0)
            {
                m_playPressed = false;
            }
            else
            {
                Vector3 newPositionL = new Vector3(m_curtainL.position.x, m_curtainL.position.y, m_curtainL.position.z);
                newPositionL.x = newPositionL.x - (m_animFactor * Time.deltaTime);
                m_curtainL.SetPositionAndRotation(newPositionL, m_curtainL.rotation);

                Vector3 newPositionR = new Vector3(m_curtainR.position.x, m_curtainR.position.y, m_curtainR.position.z);
                newPositionR.x = newPositionR.x + (m_animFactor * Time.deltaTime); 
                m_curtainR.SetPositionAndRotation(newPositionR, m_curtainR.rotation);

            }
        }
    }
}
