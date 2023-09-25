using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ObjectInteraction : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public TMP_Text HPText;

    public int damage;

    public bool isInfoFound;
    public int infoFound;
    public bool info1Found;
    public bool info2Found;
    public bool info3Found;

    public bool infoObjectiveSound;
    public bool destroyObjectiveSound;
    public bool endObjectiveSound;

    public int spawnDestroyed;
    public bool isSpawnDestroy;

    public bool isEnded;
    public bool isDead;

    public bool hasLighter;
    public Image lighterImage;
    public Image damageImage;

    public TMP_Text notificationText;
    public TMP_Text noteObjective;
    public TMP_Text destroyObjective;
    public TMP_Text endObjective;

    public Toggle destroyObjectiveToggle;
    public Toggle noteObjectiveToggle;
    public Toggle endObjectiveToggle;

    public GameObject note1Panel;
    public GameObject note2Panel;
    public GameObject note3Panel;

    public AudioSource SFX;
    public AudioClip[] characterSFX;

    public GameObject endingPanel;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        damage = 1;

        isInfoFound = false;
        infoFound = 0;
        info1Found = false;
        info2Found = false;
        info3Found = false;

        infoObjectiveSound = false;
        destroyObjectiveSound = false;
        endObjectiveSound = false;

        spawnDestroyed = 0;
        isSpawnDestroy = false;

        isEnded = false;
        isDead = false;

        hasLighter = false;

        lighterImage.enabled = false;
        damageImage.enabled = false;

        notificationText.enabled = false;

        noteObjective.alpha = 255f;
        destroyObjective.alpha = 255f;
        endObjective.alpha = 255f;
        noteObjective.text = "Informations found (0/3)";
        destroyObjective.text = "Enemy Spawn Destroyed (0/5)";
        endObjective.text = "Reach Spawn";

        noteObjectiveToggle.isOn = false;
        destroyObjectiveToggle.isOn = false;
        endObjectiveToggle.isOn = false;

        note1Panel.SetActive(false);
        note2Panel.SetActive(false);
        note3Panel.SetActive(false);

        endingPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HPText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

        noteObjective.text = "Informations found (" + infoFound.ToString() + "/3)";
        destroyObjective.text = "Enemy Spawn Destroyed (" + spawnDestroyed.ToString() + "/5)";
        endObjective.text = "Reach Spawn";

        DisableNotePanel();
        ObjectivesCompleted();

        PlayerDead();
    }

    void PlayerDead()
    {
        if(currentHealth <= 0)
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        isDead = true;
        Time.timeScale = 0f;
        notificationText.enabled = true;
        notificationText.text = "You are dead!";

        yield return new WaitForSecondsRealtime(2);

        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    void ObjectivesCompleted()
    {
        if (infoFound >= 3)
        {
            if(!infoObjectiveSound)
            {
                SFX.PlayOneShot(characterSFX[7]);
                infoObjectiveSound = true;
            }
            
            noteObjective.alpha = 0.5f;
            noteObjectiveToggle.isOn = true;
        }

        if (spawnDestroyed >= 5)
        {
            if(!destroyObjective)
            {
                SFX.PlayOneShot(characterSFX[7]);
                destroyObjectiveSound = true;
            }

            destroyObjective.alpha = 0.5f;
            destroyObjectiveToggle.isOn = true;
        }

        if(isEnded)
        {
            if(!endObjectiveSound)
            {
                SFX.PlayOneShot(characterSFX[7]);
                endObjectiveSound = true;
            }
            
            endObjective.alpha = 0.5f;
            endObjectiveToggle.isOn = true;
        }
    }

    void DisableNotePanel()
    {
        if (note1Panel.activeSelf)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Time.timeScale = 1f;
                PlayerPrefs.SetInt("Note1Obtained", 1);
                PlayerPrefs.Save();
                SFX.PlayOneShot(characterSFX[1]);
                isInfoFound = false;
                note1Panel.SetActive(false);
                StartCoroutine(Note1Obtained());
            }
        }

        if (note2Panel.activeSelf)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Time.timeScale = 1f;
                PlayerPrefs.SetInt("Note2Obtained", 1);
                PlayerPrefs.Save();
                SFX.PlayOneShot(characterSFX[1]);
                isInfoFound = false;
                note2Panel.SetActive(false);
                StartCoroutine(Note2Obtained());
            }
        }

        if (note3Panel.activeSelf)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Time.timeScale = 1f;
                PlayerPrefs.SetInt("Note3Obtained", 1);
                PlayerPrefs.Save();
                SFX.PlayOneShot(characterSFX[1]);
                isInfoFound = false;
                note3Panel.SetActive(false);
                StartCoroutine(Note3Obtained());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyAttack"))
        {
            SFX.PlayOneShot(characterSFX[4]);

            currentHealth--;
        }

        if(other.CompareTag("Note 1"))
        {
            notificationText.enabled = true;
            notificationText.text = "Press 'E' to collect Note 1";
        }

        if (other.CompareTag("Note 2"))
        {
            notificationText.enabled = true;
            notificationText.text = "Press 'E' to collect Note 2";
        }

        if (other.CompareTag("Note 3"))
        {
            notificationText.enabled = true;
            notificationText.text = "Press 'E' to collect Note 3";
        }

        if (other.CompareTag("Mask"))
        {
            notificationText.enabled = true;
            notificationText.text = "Press 'E' to heal";
        }

        if (other.CompareTag("Lighter"))
        {
            notificationText.enabled = true;
            notificationText.text = "Press 'E' to collect lighter";
        }

        if (other.CompareTag("EnemySpawn"))
        {
            notificationText.enabled = true;
            
            if (hasLighter == true)
            {
                notificationText.text = "Press 'E' to destroy enemy spawn";
            }
            else
            {
                notificationText.text = "Required a ligher to destroy enemy spawn";
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Note 1"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                Time.timeScale = 0f;
                notificationText.enabled = true;
                notificationText.text = "Press 'Esc' to close the Note 1";
                note1Panel.SetActive(true);

                if(!isInfoFound)
                {
                    SFX.PlayOneShot(characterSFX[1]);
                    isInfoFound = true;
                }

                if (!info1Found)
                {
                    infoFound++;
                    info1Found = true;
                }
            }
        }

        if (other.CompareTag("Note 2"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                Time.timeScale = 0f;
                notificationText.enabled = true;
                notificationText.text = "Press 'Esc' to close the Note 2";
                note2Panel.SetActive(true);

                if (!isInfoFound)
                {
                    SFX.PlayOneShot(characterSFX[1]);
                    isInfoFound = true;
                }

                if (!info2Found)
                {
                    infoFound++;
                    info2Found = true;
                }
            }
        }

        if (other.CompareTag("Note 3"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                Time.timeScale = 0f;
                notificationText.enabled = true;
                notificationText.text = "Press 'Esc' to close the Note 3";
                note3Panel.SetActive(true);
                damageImage.enabled = true;
                damage = 2;

                if (!isInfoFound)
                {
                    SFX.PlayOneShot(characterSFX[1]);
                    isInfoFound = true;
                }

                if (!info3Found)
                {
                    infoFound++;
                    info3Found = true;
                }
            }
        }

        if (other.CompareTag("Mask"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                if(currentHealth != maxHealth)
                {
                    SFX.PlayOneShot(characterSFX[3]);
                    currentHealth = maxHealth;
                    other.gameObject.SetActive(false);
                    notificationText.enabled = true;
                    StartCoroutine(HealthRecovered());
                }
                else
                {
                    notificationText.enabled = true;
                    StartCoroutine(HealthFull());
                }
            }
        }

        if (other.CompareTag("Lighter"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                SFX.PlayOneShot(characterSFX[0]);
                lighterImage.enabled = true;
                hasLighter = true;
                notificationText.enabled = false;
                other.gameObject.SetActive(false);
                StartCoroutine(LighterObtained());
            }
        }

        if (other.CompareTag("EnemySpawn"))
        {
            if (Input.GetKey(KeyCode.E) && hasLighter == true)
            {
                if (!isSpawnDestroy)
                {
                    SFX.PlayOneShot(characterSFX[2]);
                    spawnDestroyed++;
                    notificationText.enabled = true;
                    StartCoroutine(EnemySpawnDestroy());
                    other.gameObject.SetActive(false);
                    isSpawnDestroy = true;
                }
                else
                {
                    isSpawnDestroy = false;
                }
            }
        }

        if (other.CompareTag("End"))
        {
            if (infoFound >= 3 && spawnDestroyed >= 5)
            {
                if(!isEnded)
                {
                    SFX.PlayOneShot(characterSFX[6]);
                    isEnded = true;
                }

                notificationText.enabled = true;
                StartCoroutine(GameEnded());
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Note 1"))
        {
            notificationText.enabled = false;
        }

        if (other.CompareTag("Note 2"))
        {
            notificationText.enabled = false;
        }

        if (other.CompareTag("Note 3"))
        {
            notificationText.enabled = false;
        }

        if (other.CompareTag("Mask"))
        {
            notificationText.enabled = false;
        }

        if (other.CompareTag("Lighter"))
        {
            notificationText.enabled = false;
        }

        if (other.CompareTag("EnemySpawn"))
        {
            notificationText.enabled = false;
        }
    }

    IEnumerator Note1Obtained()
    {
        notificationText.enabled = true;
        notificationText.text = "Note 1 obtained & placed in gallery";

        yield return new WaitForSeconds(3f);

        notificationText.enabled = false;
    }

    IEnumerator Note2Obtained()
    {
        notificationText.enabled = true;
        notificationText.text = "Note 2 obtained & placed in gallery";

        yield return new WaitForSeconds(3f);

        notificationText.enabled = false;
    }

    IEnumerator Note3Obtained()
    {
        notificationText.enabled = true;
        notificationText.text = "Note 3 obtained & placed in gallery";

        yield return new WaitForSeconds(3f);

        notificationText.enabled = false;
    }

    IEnumerator HealthRecovered()
    {
        notificationText.enabled = true;
        notificationText.text = "Health Recovered";

        yield return new WaitForSeconds(3f);

        notificationText.enabled = false;
    }

    IEnumerator HealthFull()
    {
        notificationText.enabled = true;
        notificationText.text = "Health is Full";

        yield return new WaitForSeconds(3f);

        notificationText.enabled = true;
        notificationText.text = "Press 'E' to heal";
    }

    IEnumerator LighterObtained()
    {
        notificationText.enabled = true;
        notificationText.text = "Ligher obtained, can destroy enemy spawn";

        yield return new WaitForSeconds(3f);

        notificationText.enabled = false;
    }

    IEnumerator EnemySpawnDestroy()
    {
        notificationText.enabled = true;
        notificationText.text = "Enemy spawn destroyed";

        yield return new WaitForSeconds(3f);

        notificationText.enabled = false;
    }

    IEnumerator GameEnded()
    {
        notificationText.enabled = true;
        notificationText.text = "Congratulations!";

        yield return new WaitForSeconds(3f);

        Time.timeScale = 0f;
        endingPanel.SetActive(true);
    }
}
