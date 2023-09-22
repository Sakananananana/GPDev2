using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public GameObject Title;
    public GameObject MainMenuPanel;
    public GameObject GalleryPanel;
    public GameObject SettingsPanel;
    public GameObject CreditsPanel;

    public GameObject Note1;
    public GameObject Note2;
    public GameObject Note3;
    public int noteCount;

    public TMP_Text noteNum;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        Title.SetActive(true);
        MainMenuPanel.SetActive(true);
        GalleryPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        CreditsPanel.SetActive(false);

        noteCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        ShowNotes();

        if(Input.GetKey(KeyCode.Escape))
        {
            if(GalleryPanel.activeSelf)
            {
                Title.SetActive(true);
                MainMenuPanel.SetActive(true);
                GalleryPanel.SetActive(false);
                noteCount = 1;
            }

            if(SettingsPanel.activeSelf)
            {
                Title.SetActive(true);
                MainMenuPanel.SetActive(true);
                SettingsPanel.SetActive(false);
            }

            if(CreditsPanel.activeSelf)
            {
                Title.SetActive(true);
                MainMenuPanel.SetActive(true);
                CreditsPanel.SetActive(false);
            }
        }
    }

    void ShowNotes()
    {
        noteNum.text = noteCount.ToString() + "/3";

        if (Input.GetKeyDown(KeyCode.A))
        {
            if(noteCount <= 1)
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

        if(noteCount == 2)
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

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
