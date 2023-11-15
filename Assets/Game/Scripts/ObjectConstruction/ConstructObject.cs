using System.Collections;
using UnityEngine;

public class ConstructObject : MonoBehaviour
{ 
    [SerializeField] private GenericEventChannelSO objectTobeDraggedEventChannel;
    [SerializeField] private GameObject cannotBuildHereText;
    [SerializeField] private Material transparentMaterial;

    GameObject ghost , dataGameObject;
    private bool ghostMode;


    private void Start()
    {
        objectTobeDraggedEventChannel.RegisterListener(MakeGhost);
    }
    
    private void MakeGhost(object data)
    {
        dataGameObject = (GameObject)data;
        ghost = SpawnGameObject(dataGameObject);
        ChangeMaterialToTransparent(ghost);
        
        foreach (var meshCollider in ghost.GetComponentsInChildren<MeshCollider>())
        {
            meshCollider.enabled = false;
        }
        
        ghost.GetComponentsInChildren<Rigidbody>()[0].useGravity = false;
        ghostMode = true;
    }

    private void Update()
    {
        if (ghostMode)
        {
            ghost.transform.position =
                new Vector3(Mouse3D.GetMouseWorldPosition().x, 0, Mouse3D.GetMouseWorldPosition().z);

            if (Input.GetMouseButtonUp(0))
            {
                if (!ghost.GetComponentsInChildren<BuildingCollision>()[0].isColliding)
                {
                    SpawnGameObject(dataGameObject);
                }
                else StartCoroutine(InstantiateCannotBuildPopUp());
                
                Destroy(ghost);
                ghostMode = false;
            }
        }
    }

    private GameObject SpawnGameObject(GameObject objectToBeSpawn)
    {
        GameObject obj = Instantiate(objectToBeSpawn,
            new Vector3(Mouse3D.GetMouseWorldPosition().x, 0, Mouse3D.GetMouseWorldPosition().z), Quaternion.identity);
        return obj;
    }
    
    private void ChangeMaterialToTransparent(GameObject obj)
    {
        if (obj != null)
        {
            Renderer renderer = obj.GetComponent<Renderer>();

            if (renderer != null)
            {
                Material[] materials = renderer.materials;

                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i] = transparentMaterial;
                }

                renderer.materials = materials;
            }

            foreach (Transform child in obj.transform)
            {
                ChangeMaterialToTransparent(child.gameObject);
            }
        }
    }
    
    private IEnumerator InstantiateCannotBuildPopUp()
    {
        float elapsedTime = 0f;
        GameObject popUpText = Instantiate(cannotBuildHereText, Mouse3D.GetMouseWorldPosition(), Quaternion.identity);
        
        while (elapsedTime < 1.2f)
        {
            popUpText.transform.position += new Vector3(0, 0.2f, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(popUpText);
    }
    
    private void OnDisable()
    {
        objectTobeDraggedEventChannel.UnregisterListener(MakeGhost);
    }

}
