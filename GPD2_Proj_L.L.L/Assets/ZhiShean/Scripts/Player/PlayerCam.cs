using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject note1Panel;
    public GameObject note2Panel;
    public GameObject note3Panel;

    public GameObject startingPanel;
    public GameObject endingPanel;

    public float horiValue;
    public float vertiValue;

    public float oriSens;

    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    ObjectInteraction oI;

    // Start is called before the first frame update
    void Start()
    {
        oI = GetComponentInParent<ObjectInteraction>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu.activeSelf || note1Panel.activeSelf || note2Panel.activeSelf || note3Panel.activeSelf || startingPanel.activeSelf || endingPanel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (!oI.isDead)
            {
                horiValue = PlayerPrefs.GetFloat("HoriSensitivity", 1);
                sensX = oriSens * horiValue;

                vertiValue = PlayerPrefs.GetFloat("VertiSensitivity", 1);
                sensY = oriSens * vertiValue;

                //get mouse input
                float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
                float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

                yRotation += mouseX;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                //rotate cam and orientation
                transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
                orientation.rotation = Quaternion.Euler(0, yRotation, 0);
            }
        }
    }
}
