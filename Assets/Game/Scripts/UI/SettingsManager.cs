using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] settingsUIs; 
    [SerializeField] private GameObject[] otherComponents;

    private void Start()
    {
        SettingsUIActiveState(false);
    }

    public void OnCLickExitButton()
    {
        Application.Quit();
    }

    public void ResetPlayerPosition()
    {
        player.transform.position = Vector3.zero;
    }
    
    
    public void OnClickSettingButton()
    {
        OtherUIComponentsActiveState(false);
        SettingsUIActiveState(true);
    }

    public void OnClickCrossButton()
    {
        SettingsUIActiveState(false);
        OtherUIComponentsActiveState(true);
    }
    
    private void SettingsUIActiveState(bool state)
    {
        foreach (GameObject settingsUI in settingsUIs)
        {
            settingsUI.SetActive(state);
        }
    }
    
    private void OtherUIComponentsActiveState(bool state)
    {
        foreach (GameObject otherUIComponent in otherComponents)
        {
            otherUIComponent.SetActive(state);
        }
    }
}
