using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    public GameObject[] icons; // Reference to your existing icon buttons
    private int currentlyActiveIconIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        // Attach a click handler to each existing icon
        for (int i = 0; i < icons.Length; i++)
        {
            int index = i; // Capture the current value of i in the closure

            // Assuming the icons have a Button component or similar for interaction
            Button iconButton = icons[i].GetComponent<Button>();
            if (iconButton != null)
            {
                iconButton.onClick.AddListener(() => OnIconButtonClick(index));
            }
        }
    }

    // Handle icon button click
    void OnIconButtonClick(int clickedIndex)
    {
        // Deactivate the children of the previously clicked icon
        if (currentlyActiveIconIndex != -1)
        {
            SetIconChildrenActiveState(icons[currentlyActiveIconIndex], false);
        }

        // Activate the children of the currently clicked icon
        SetIconChildrenActiveState(icons[clickedIndex], true);

        // Update the currently active icon index
        currentlyActiveIconIndex = clickedIndex;
    }

    // Activate or deactivate the children of an icon
    void SetIconChildrenActiveState(GameObject icon, bool activate)
    {
        foreach (Transform child in icon.transform)
        {
            child.gameObject.SetActive(activate);
        }
    }
}