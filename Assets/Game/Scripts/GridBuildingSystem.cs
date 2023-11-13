using System;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    [Header("Grid sizes")]
    [SerializeField] private int gridWidth = 8;
    [SerializeField] private int gridHeight = 8;
    [SerializeField] private float cellSize = 10f;


    [SerializeField] private GenericEventChannelSO buildTheObject;

    private GridXZ<GridObject> grid;
    private PlacedObjectTypeSO placedObjectTypeSo;

    private void Awake()
    {
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, 
            (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
    }

    private void Start() => buildTheObject.RegisterListener(StartBuilding);
    
    public class GridObject
    {
        private GridXZ<GridObject> grid;
        private int x;
        private int z;
        private Transform transform;
        
        public GridObject(GridXZ<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }
        
        public void SetTransform(Transform transform)
        {
            this.transform = transform;
            grid.TriggerGridObjectChanged(x, z);
        }
        
        public void ClearTransform()
        {
            transform = null;
            grid.TriggerGridObjectChanged(x, z);
        }
        
        public bool CanBuild()
        {
            return transform == null;
        }
        
        public override string ToString()
        {
            return String.Empty;
        }
    }
    
    

    private void StartBuilding(object data)
    {
        placedObjectTypeSo = (PlacedObjectTypeSO)data;
        
        grid.GetXZ(Mouse3D.GetMouseWorldPosition() , out int x , out int z);

        List<Vector2Int> gridPositionList = placedObjectTypeSo.GetGridPositionList(new Vector2Int(x, z), PlacedObjectTypeSO.Dir.Up);
            
        bool canBuild = true; 
        foreach (Vector2Int gridPosition in gridPositionList) 
        { 
            if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild()) 
            { 
                canBuild = false; 
                break;
            }
        }

        if (canBuild) 
        { 
            Transform buildingTransform = InstantiateBuilding(x , z); 
            foreach (Vector2Int gridPosition in gridPositionList) 
            { 
                grid.GetGridObject(gridPosition.x, gridPosition.y).SetTransform(buildingTransform);
            }
        }
        else 
        {
                UtilsClass.CreateWorldTextPopup("Cannot build here!", Mouse3D.GetMouseWorldPosition()); 
        }
    }
    
    public Transform InstantiateBuilding(int x , int z)
    {
        Vector2Int rotationOffset = placedObjectTypeSo.GetRotationOffset(PlacedObjectTypeSO.Dir.Up);
        Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + 
                                            new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
        Transform buildingTransform = Instantiate(placedObjectTypeSo.prefab, placedObjectWorldPosition,
            Quaternion.Euler(0 , placedObjectTypeSo.GetRotationAngle(PlacedObjectTypeSO.Dir.Up) , 0));


        return buildingTransform;
    }
    
    
    private void OnDisable()
    {
        buildTheObject.UnregisterListener(StartBuilding);
    }

    
    
}

