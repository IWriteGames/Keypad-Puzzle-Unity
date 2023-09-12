using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    private float timePlayed = 0f;
    private bool isWin;

    //States
    private bool stateOne, stateTwo, stateThree, stateFour, stateFive, stateSix, stateSeven, stateEight, stateNine;

    //Objects
    [SerializeField] private Material lockedCristal, unlockedCristal;

    [SerializeField] private GameObject cristalOne, cristalTwo, cristalThree, cristalFour, cristalFive, cristalSix, cristalSeven, cristalEight, cristalNine;

    [SerializeField] private GameObject keyOne, keyTwo, keyThree, keyFour, keyFive, keySix, keySeven, keyEight, keyNine;

    //Animations
    [SerializeField] private Animator AkeyOne, AkeyTwo, AkeyThree, AkeyFour, AkeyFive, AkeySix, AkeySeven, AkeyEight, AkeyNine;

    //Canvas
    [SerializeField] private TMP_Text textPlayed, victories, bestTimePlayed, timeVictory, numberVictories;

    [SerializeField] private GameObject GamePanel, VictoryPanel;

    [SerializeField] private Light lightScene;

    //Audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource musicBack;
    [SerializeField] private AudioClip key;
    [SerializeField] private AudioClip victory;

    void Awake()
    {
        StartPanel();
        isWin = false;

        victories.text = "Nº Victories: " + PlayerPrefs.GetInt("Victories");
        bestTimePlayed.text = "Your Best Time: " + PlayerPrefs.GetFloat("BestTime").ToString("f2");

        GamePanel.SetActive(true);
        VictoryPanel.SetActive(false);
        lightScene.enabled = true;
    }

    void Update()
    {
        TouchButton();
        if(!Rules.Instance.RulesIsOpen && !isWin)
        {
            TimePlayedCounter();
        }
    }

    private void TimePlayedCounter()
    {
        timePlayed += Time.deltaTime;
        textPlayed.text = "Time: " + timePlayed.ToString("f2") + " seconds";
    }

    void TouchButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "Key1")
                {
                    AkeyOne.SetTrigger("keyOneTrigger");
                    ButtonOne();
                    ButtonTwo();
                    ButtonFour();
                    audioSource.PlayOneShot(key);
                }
                else if (hit.collider.gameObject.name == "Key2")
                {
                    AkeyTwo.SetTrigger("keyTwoTrigger");
                    ButtonOne();
                    ButtonTwo();
                    ButtonThree();
                    ButtonFive();
                    audioSource.PlayOneShot(key);
                }
                else if (hit.collider.gameObject.name == "Key3")
                {
                    AkeyThree.SetTrigger("keyThreeTrigger");
                    ButtonTwo();
                    ButtonThree();
                    ButtonSix();
                    audioSource.PlayOneShot(key);
                }
                else if (hit.collider.gameObject.name == "Key4")
                {
                    AkeyFour.SetTrigger("keyFourTrigger");
                    ButtonOne();
                    ButtonFour();
                    ButtonSeven();
                    ButtonFive();
                    audioSource.PlayOneShot(key);
                }
                else if (hit.collider.gameObject.name == "Key5")
                {
                    AkeyFive.SetTrigger("keyFiveTrigger");
                    ButtonTwo();
                    ButtonFour();
                    ButtonSix();
                    ButtonEight();
                    ButtonFive();
                    audioSource.PlayOneShot(key);
                }
                else if (hit.collider.gameObject.name == "Key6")
                {
                    AkeySix.SetTrigger("keySixTrigger");
                    ButtonSix();
                    ButtonThree();
                    ButtonNine();
                    ButtonFive();
                    audioSource.PlayOneShot(key);
                }
                else if (hit.collider.gameObject.name == "Key7")
                {
                    AkeySeven.SetTrigger("keySevenTrigger");
                    ButtonSeven();
                    ButtonFour();
                    ButtonEight();
                    audioSource.PlayOneShot(key);
                }
                else if (hit.collider.gameObject.name == "Key8")
                {
                    AkeyEight.SetTrigger("keyEightTrigger");
                    ButtonEight();
                    ButtonFive();
                    ButtonSeven();
                    ButtonNine();
                    audioSource.PlayOneShot(key);
                }
                else if (hit.collider.gameObject.name == "Key9")
                {
                    AkeyNine.SetTrigger("keyNineTrigger");
                    ButtonNine();
                    ButtonEight();
                    ButtonSix();
                    audioSource.PlayOneShot(key);
                }
            }

            CheckPanel();
        }
    }

    public void CheckPanel()
    {
        if(!isWin)
        {
            if (stateOne == true && stateTwo == true && stateThree == true && stateFour == true
                && stateFive == true && stateSix == true && stateSeven == true && stateEight == true
                && stateNine == true)
            {
                isWin = true;

                audioSource.PlayOneShot(victory);
                musicBack.Stop();

                keyOne.GetComponent<BoxCollider>().enabled = false;
                keyTwo.GetComponent<BoxCollider>().enabled = false;
                keyThree.GetComponent<BoxCollider>().enabled = false;
                keyFour.GetComponent<BoxCollider>().enabled = false;
                keyFive.GetComponent<BoxCollider>().enabled = false;
                keySix.GetComponent<BoxCollider>().enabled = false;
                keySeven.GetComponent<BoxCollider>().enabled = false;
                keyEight.GetComponent<BoxCollider>().enabled = false;
                keyNine.GetComponent<BoxCollider>().enabled = false;

                GamePanel.SetActive(false);
                VictoryPanel.SetActive(true);
                lightScene.enabled = false;

                if(PlayerPrefs.GetInt("Victories") == 0)
                {
                    PlayerPrefs.SetFloat("BestTime", timePlayed);
                    timeVictory.text = "Time: " + timePlayed.ToString("f2") + " seconds <br> It's a new record!";
                } else
                {
                    if (timePlayed <= PlayerPrefs.GetFloat("BestTime"))
                    {
                        PlayerPrefs.SetFloat("BestTime", timePlayed);
                        timeVictory.text = "Time: " + timePlayed.ToString("f2") + " seconds <br> It's a new record!";

                    }
                    else
                    {
                        timeVictory.text = "Time: " + timePlayed.ToString("f2") + " seconds";
                    }
                }
                
                PlayerPrefs.SetInt("Victories", PlayerPrefs.GetInt("Victories") + 1);

                numberVictories.text = "You Win! <br> It's your victory Nº " + PlayerPrefs.GetInt("Victories") + "!";
            }
        }
    }

    bool GetRandomBool()
    {
        int randomNumber = Random.Range(0, 100);
        return (randomNumber % 2 == 0) ? true : false;
    }

    void StartPanel()
    {
        bool isBoolOne = GetRandomBool();
        bool isBoolTwo = GetRandomBool();
        bool isBoolThree = GetRandomBool();
        bool isBoolFour = GetRandomBool();
        bool isBoolFive = GetRandomBool();
        bool isBoolSix = GetRandomBool();
        bool isBoolSeven = GetRandomBool();
        bool isBoolEight = GetRandomBool();
        bool isBoolNine = GetRandomBool();

        keyOne.GetComponent<BoxCollider>().enabled = true;
        keyTwo.GetComponent<BoxCollider>().enabled = true;
        keyThree.GetComponent<BoxCollider>().enabled = true;
        keyFour.GetComponent<BoxCollider>().enabled = true;
        keyFive.GetComponent<BoxCollider>().enabled = true;
        keySix.GetComponent<BoxCollider>().enabled = true;
        keySeven.GetComponent<BoxCollider>().enabled = true;
        keyEight.GetComponent<BoxCollider>().enabled = true;
        keyNine.GetComponent<BoxCollider>().enabled = true;

        if (isBoolOne)
        {
            cristalOne.GetComponent<Renderer>().material = unlockedCristal;
            stateOne = true;
        }
        else
        {
            cristalOne.GetComponent<Renderer>().material = lockedCristal;
            stateOne = false;
        }

        if (isBoolTwo)
        {
            cristalTwo.GetComponent<Renderer>().material = unlockedCristal;
            stateTwo = true;
        }
        else
        {
            cristalTwo.GetComponent<Renderer>().material = lockedCristal;
            stateTwo = false;
        }

        if (isBoolThree)
        {
            cristalThree.GetComponent<Renderer>().material = unlockedCristal;
            stateThree = true;
        }
        else
        {
            cristalThree.GetComponent<Renderer>().material = lockedCristal;
            stateThree = false;
        }

        if (isBoolFour)
        {
            cristalFour.GetComponent<Renderer>().material = unlockedCristal;
            stateFour = true;
        }
        else
        {
            cristalFour.GetComponent<Renderer>().material = lockedCristal;
            stateFour = false;
        }

        if (isBoolFive)
        {
            cristalFive.GetComponent<Renderer>().material = unlockedCristal;
            stateFive = true;
        }
        else
        {
            cristalFive.GetComponent<Renderer>().material = lockedCristal;
            stateFive = false;
        }

        if (isBoolSix)
        {
            cristalSix.GetComponent<Renderer>().material = unlockedCristal;
            stateSix = true;
        }
        else
        {
            cristalSix.GetComponent<Renderer>().material = lockedCristal;
            stateSix = false;
        }

        if (isBoolSeven)
        {
            cristalSeven.GetComponent<Renderer>().material = unlockedCristal;
            stateSeven = true;
        }
        else
        {
            cristalSeven.GetComponent<Renderer>().material = lockedCristal;
            stateSeven = false;
        }

        if (isBoolEight)
        {
            cristalEight.GetComponent<Renderer>().material = unlockedCristal;
            stateEight = true;
        }
        else
        {
            cristalEight.GetComponent<Renderer>().material = lockedCristal;
            stateEight = false;
        }

        if (isBoolNine)
        {
            cristalNine.GetComponent<Renderer>().material = unlockedCristal;
            stateNine = true;
        }
        else
        {
            cristalNine.GetComponent<Renderer>().material = lockedCristal;
            stateNine = false;
        }
    }


    public void ButtonOne()
    {
        if (stateOne == true)
        {
            cristalOne.GetComponent<Renderer>().material = lockedCristal;
            stateOne = false;

        }
        else if (stateOne == false)
        {
            cristalOne.GetComponent<Renderer>().material = unlockedCristal;
            stateOne = true;
        }
    }

    public void ButtonTwo()
    {
        if (stateTwo == true)
        {
            cristalTwo.GetComponent<Renderer>().material = lockedCristal;
            stateTwo = false;
        }
        else if (stateTwo == false)
        {
            cristalTwo.GetComponent<Renderer>().material = unlockedCristal;
            stateTwo = true;
        }
    }

    public void ButtonThree()
    {
        if (stateThree == true)
        {
            cristalThree.GetComponent<Renderer>().material = lockedCristal;
            stateThree = false;
        }
        else if (stateThree == false)
        {
            cristalThree.GetComponent<Renderer>().material = unlockedCristal;
            stateThree = true;
        }
    }

    public void ButtonFour()
    {
        if (stateFour == true)
        {
            cristalFour.GetComponent<Renderer>().material = lockedCristal;
            stateFour = false;
        }
        else if (stateFour == false)
        {
            cristalFour.GetComponent<Renderer>().material = unlockedCristal;
            stateFour = true;
        }
    }

    public void ButtonFive()
    {
        if (stateFive == true)
        {
            cristalFive.GetComponent<Renderer>().material = lockedCristal;
            stateFive = false;
        }
        else
        {
            cristalFive.GetComponent<Renderer>().material = unlockedCristal;
            stateFive = true;
        }
    }

    public void ButtonSix()
    {
        if (stateSix == true)
        {
            cristalSix.GetComponent<Renderer>().material = lockedCristal;
            stateSix = false;
        }
        else
        {
            cristalSix.GetComponent<Renderer>().material = unlockedCristal;
            stateSix = true;
        }
    }

    public void ButtonSeven()
    {
        if (stateSeven == true)
        {
            cristalSeven.GetComponent<Renderer>().material = lockedCristal;
            stateSeven = false;
        }
        else
        {
            cristalSeven.GetComponent<Renderer>().material = unlockedCristal;
            stateSeven = true;
        }
    }

    public void ButtonEight()
    {
        if (stateEight == true)
        {
            cristalEight.GetComponent<Renderer>().material = lockedCristal;
            stateEight = false;
        }
        else
        {
            cristalEight.GetComponent<Renderer>().material = unlockedCristal;
            stateEight = true;
        }
    }

    public void ButtonNine()
    {
        if (stateNine == true)
        {
            cristalNine.GetComponent<Renderer>().material = lockedCristal;
            stateNine = false;
        }
        else
        {
            cristalNine.GetComponent<Renderer>().material = unlockedCristal;
            stateNine = true;
        }
    }

}
