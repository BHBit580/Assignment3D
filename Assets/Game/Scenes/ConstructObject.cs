using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructObject : MonoBehaviour
{ 
    [SerializeField] private GenericEventChannelSO objectTobeDraggedEventChannel;
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
        ChangeTransparentMaterial(ghost);
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
                Instantiate(placedObjectTypeSo.prefab,
                    new Vector3(Mouse3D.GetMouseWorldPosition().x, 0, Mouse3D.GetMouseWorldPosition().z),
                    Quaternion.identity);
                
                ghostMode = false;
            }
        }
    }

    
    private void ChangeTransparentMaterial(GameObject obj)
    {
        if (obj != null)
        {
            Renderer renderer = obj.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.material = transparentMaterial;
            }
            
            foreach (Transform child in obj.transform)
            {
                ChangeTransparentMaterial(child.gameObject);
            }
        }
    }


    private void OnDisable()
    {
        objectTobeDraggedEventChannel.UnregisterListener(MakeGhost);
    }
    
}
