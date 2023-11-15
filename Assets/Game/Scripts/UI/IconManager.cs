using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    public GameObject[] icons;
    public GameObject followIcon; 
    
    public float speed = 5f; 

    private bool isFollowing;
    private int targetIndex;

    void Start()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            int index = i; 
            
            Button iconButton = icons[i].GetComponent<Button>();
            if (iconButton != null)
            {
                iconButton.onClick.AddListener(() => OnIconButtonClick(index));
            }
        }
    }

    void Update()
    {
        if (isFollowing)
        {
            Vector3 movementPos = Vector3.Lerp(followIcon.transform.position, icons[targetIndex].transform.position, Time.deltaTime * speed);
            followIcon.transform.position = new Vector3(movementPos.x, followIcon.transform.position.y,
                followIcon.transform.position.z);
        }
    }

    void OnIconButtonClick(int clickedIndex)
    {
        SetIconChildrenActiveState(icons[clickedIndex], true);
        
        targetIndex = clickedIndex;
        isFollowing = true;

        for (int i = 0; i < icons.Length; i++)
        {
            if (i == clickedIndex) continue;
            SetIconChildrenActiveState(icons[i], false);
        }
    }

    void SetIconChildrenActiveState(GameObject icon, bool activate)
    {
        foreach (Transform child in icon.transform)
        {
            child.gameObject.SetActive(activate);
        }
    }
}