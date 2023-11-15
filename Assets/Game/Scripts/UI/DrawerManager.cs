using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class DrawerManager : MonoBehaviour
{
    [SerializeField] private GameObject drawer;
    [SerializeField] private GameObject drawerButton;
    [SerializeField] private GenericEventChannelSO objectTobeDraggedEventChannel;
    
    [SerializeField] private float timeSpeed = 1;

    private void Start()
    {
        StartCoroutine(MoveDrawerCoroutine(new Vector2(400, 0)));
        objectTobeDraggedEventChannel.RegisterListener(DisableDrawer);
        drawer.SetActive(false);
    }

    public void OnClickDrawerButton()
    {
        StartCoroutine(MoveDrawerCoroutine(new Vector2(-3, 0)));
        drawer.SetActive(true);
    }
    
    private void DisableDrawer(object data)
    {
        StartCoroutine(MoveDrawerCoroutine(new Vector2(400, 0)));
        drawer.SetActive(false);
        drawerButton.SetActive(true);
    }
    
    private void OnDisable()
    {
        objectTobeDraggedEventChannel.UnregisterListener(DisableDrawer);
    }
    
    private IEnumerator MoveDrawerCoroutine(Vector2 targetPosition)
    {
        Vector2 initialPosition = drawer.GetComponent<RectTransform>().anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < timeSpeed)
        {
            drawer.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(initialPosition, targetPosition, elapsedTime / timeSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        drawer.GetComponent<RectTransform>().anchoredPosition = targetPosition;
    }
}