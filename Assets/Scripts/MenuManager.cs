using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditsPanel;

    private void Awake()
    {
        MenuDefault();
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        buttonsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        buttonsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void MenuDefault()
    {
        buttonsPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

}
