using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rules : MonoBehaviour
{
    [SerializeField] private GameObject RulesPanel;
    public bool RulesIsOpen;
    public static Rules Instance;

    private void Awake() 
    {
        Instance = this;
        CloseRules();
    }

    public void OpenRules()
    {
        RulesPanel.SetActive(true);
        RulesIsOpen = true;
    }

    public void CloseRules()
    {
        RulesPanel.SetActive(false);
        RulesIsOpen = false;
    }
}
