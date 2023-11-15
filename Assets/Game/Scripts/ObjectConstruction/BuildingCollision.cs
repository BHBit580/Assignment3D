using System;
using UnityEngine;

public class BuildingCollision : MonoBehaviour
{
    public bool isColliding;

    private string constructObjectTag = "ConstructObject";
    private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        CheckCollisionTag(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CheckCollisionTag(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(constructObjectTag))
        {
            isColliding = false;
        }
    }

    private void CheckCollisionTag(Collider other)
    {
        if (other.CompareTag(constructObjectTag) && !other.CompareTag(playerTag))
        {
            isColliding = true;
        }
    }
}