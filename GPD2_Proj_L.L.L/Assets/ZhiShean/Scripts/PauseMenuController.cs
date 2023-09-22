using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenuController : MonoBehaviour
{
    public GameObject PauseMenuPanel;
    public GameObject PauseText;
    public GameObject PauseMainPanel;
    public GameObject GalleryPanel;
    public GameObject SettingsPanel;
    public GameObject CreditsPanel;

    public GameObject inGameNote1;
    public GameObject inGameNote2;
    public GameObject inGameNote3;

    public GameObject Note1;
    public GameObject Note2;
    public GameObject Note3;
    public int noteCount;
    public TMP_Text noteNum;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenuPanel.SetActive(false);
        PauseText.SetActive(false);
        PauseMainPanel.SetActive(false);
        GalleryPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        CreditsPanel.SetActive(false);

        noteCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (GalleryPanel.activeSelf)
        {
            ShowNotes();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(PauseMenuPanel.activeSelf)
            {
                PauseMenuPanel.SetActive(false);
                PauseText.SetActive(false);
                PauseMainPanel.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                if (!inGameNote1.activeSelf && !inGameNote2.activeSelf && !inGameNote3.activeSelf)
                {
                    PauseMenuPanel.SetActive(true);
                    PauseText.SetActive(true);
                    PauseMainPanel.SetActive(true);
                    Time.timeScale = 0f;
                }
            }

            if (GalleryPanel.activeSelf)
            {
                PauseMenuPanel.SetActive(true);
                PauseText.SetActive(true);
                PauseMainPanel.SetActive(true);
                GalleryPanel.SetActive(false);
                noteCount = 1;
            }

            if (SettingsPanel.activeSelf)
            {
                PauseMenuPanel.SetActive(true);
                PauseText.SetActive(true);
                PauseMainPanel.SetActive(true);
                SettingsPanel.SetActive(false);
            }

            if (CreditsPanel.activeSelf)
            {
                PauseMenuPanel.SetActive(true);
                PauseText.SetActive(true);
                PauseMainPanel.SetActive(true);
                CreditsPanel.SetActive(false);
            }
        }
    }

    void ShowNotes()
    {
        noteNum.text = noteCount.ToString() + "/3";

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (noteCount <= 1)
            {
                noteCount = 1;
            }
            else
            {
                noteCount--;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (noteCount >= 3)
            {
                noteCount = 3;
            }
            else
            {
                noteCount++;
            }
        }

        if (noteCount == 1)
        {
            Note1.SetActive(true);
        }
        else
        {
            Note1.SetActive(false);
        }

        if (noteCount == 2)
        {
            Note2.SetActive(true);
        }
        else
        {
            Note2.SetActive(false);
        }

        if (noteCount == 3)
        {
            Note3.SetActive(true);
        }
        else
        {
            Note3.SetActive(false);
        }
    }

    public void ResumeButton()
    {
        PauseMenuPanel.SetActive(false);
        PauseText.SetActive(false);
        PauseMainPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

}
