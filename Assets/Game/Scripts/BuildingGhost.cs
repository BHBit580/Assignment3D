using System;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    [SerializeField] private GenericEventChannelSO objectTobeDraggedEventChannel;
    [SerializeField] private GenericEventChannelSO buildTheObject;
    [SerializeField] private Material transparentMaterial;

    GameObject ghost;
    private bool ghostMode;
    private PlacedObjectTypeSO placedObjectTypeSo;

    private void Start()
    {
        objectTobeDraggedEventChannel.RegisterListener(MakeGhost);
    }
    
    private void MakeGhost(object data)
    {
        placedObjectTypeSo = (PlacedObjectTypeSO)data;
        ghost = Instantiate(placedObjectTypeSo.prefab, new Vector3(Mouse3D.GetMouseWorldPosition().x, 0, Mouse3D.GetMouseWorldPosition().z),
            Quaternion.identity).gameObject;
        ChangeTransparentMaterial(ghost, true);
        ghostMode = true;
    }

    private void Update()
    {
        if (ghostMode)
        {
            ghost.transform.position = new Vector3(Mouse3D.GetMouseWorldPosition().x, 0, Mouse3D.GetMouseWorldPosition().z);

            if (Input.GetMouseButtonUp(0))
            {
                Destroy(ghost);
                buildTheObject.RaiseEvent(placedObjectTypeSo);
                ghostMode = false;
            }
        }
    }

    
    private void ChangeTransparentMaterial(GameObject obj, bool condition)
    {
        if (obj != null)
        {
            Renderer renderer = obj.GetComponent<Renderer>();

            if (renderer != null)
            {
                Material currentMaterial = condition ? transparentMaterial : renderer.material;
                renderer.material = currentMaterial;
            }
            
            // Recursively update materials for children
            foreach (Transform child in obj.transform)
            {
                ChangeTransparentMaterial(child.gameObject, condition);
            }
        }
    }


    private void OnDisable()
    {
        objectTobeDraggedEventChannel.UnregisterListener(MakeGhost);
    }
}







