using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuestionsController : MonoBehaviour
{
    public GameObject startingPanel;
    public GameObject endingPanel;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        startingPanel.SetActive(true);
        endingPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartingContinue();
        EndingContinue();
    }

    void StartingContinue()
    {
        if(startingPanel.activeSelf)
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                Time.timeScale = 1f;
                startingPanel.SetActive(false);
            }
        }
    }

    void EndingContinue()
    {
        if (endingPanel.activeSelf)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Time.timeScale = 1f;
                endingPanel.SetActive(false);
                SceneManager.LoadScene(0);
            }
        }
    }
    public void StartingQuestion()
    {

    }
    public void EndingQuestion()
    {

    }
}
