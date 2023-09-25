using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GalleryController : MonoBehaviour
{
    public Sprite[] noteSprite;

    public Image note1Image;
    public Image note2Image;
    public Image note3Image;

    public TMP_Text note1Text;
    public TMP_Text note2Text;
    public TMP_Text note3Text;

    public int note1Obtained;
    public int note2Obtained;
    public int note3Obtained;


    // Start is called before the first frame update
    void Start()
    {
        note1Obtained = PlayerPrefs.GetInt("Note1Obtained");
        note2Obtained = PlayerPrefs.GetInt("Note2Obtained");
        note3Obtained = PlayerPrefs.GetInt("Note3Obtained");
    }

    // Update is called once per frame
    void Update()
    {
        note1Obtained = PlayerPrefs.GetInt("Note1Obtained");
        note2Obtained = PlayerPrefs.GetInt("Note2Obtained");
        note3Obtained = PlayerPrefs.GetInt("Note3Obtained");

        if (note1Obtained == 0)
        {
            note1Image.sprite = noteSprite[0];
            note1Text.text = "Not Obtained";
        }
        else
        {
            note1Image.sprite = noteSprite[1];
            note1Text.text = "Obtained";
        }


        if (note2Obtained == 0)
        {
            note2Image.sprite = noteSprite[0];
            note2Text.text = "Not Obtained";
        }
        else
        {
            note2Image.sprite = noteSprite[2];
            note2Text.text = "Obtained";
        }


        if (note3Obtained == 0)
        {
            note3Image.sprite = noteSprite[0];
            note3Text.text = "Not Obtained";
        }
        else
        {
            note3Image.sprite = noteSprite[3];
            note3Text.text = "Obtained";
        }
    }
}
