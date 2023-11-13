using System;
using UnityEngine;
using DG.Tweening;

public class DrawerManager : MonoBehaviour
{
    [SerializeField] private GameObject drawer;
    [SerializeField] private GenericEventChannelSO objectTobeDraggedEventChannel;
    [SerializeField] private float timeSpeed = 1;

    private void Start()
    {
        drawer.GetComponent<RectTransform>().DOAnchorPos(new Vector2(400, 0), timeSpeed);
        drawer.SetActive(false);
        objectTobeDraggedEventChannel.RegisterListener(DisableDrawer);
    }

    public void OnClickDrawerButton()
    {
        drawer.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-3, 0), timeSpeed);
        drawer.SetActive(true);
    }
    
    private void DisableDrawer(object data)
    {
        drawer.GetComponent<RectTransform>().DOAnchorPos(new Vector2(400, 0), timeSpeed);
        drawer.SetActive(false);
    }

    private void OnDisable()
    {
        objectTobeDraggedEventChannel.UnregisterListener(DisableDrawer);
    }
}