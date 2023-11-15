using Cinemachine;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera upViewCamera;
    [SerializeField] private CinemachineVirtualCamera playerFollowCamera;

    private void Start()
    {
        upViewCamera.Priority = 0;
        playerFollowCamera.Priority = 1;
    }

    public void ChangePriorityToPlayerFollowCamera()
    {
        upViewCamera.Priority = 0;
        playerFollowCamera.Priority = 1;
    }
    
    public void ChangePriorityToUpViewCamera()
    {
        upViewCamera.Priority = 1;
        playerFollowCamera.Priority = 0;
    }
}
