using Unity.Cinemachine;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    public CinemachineCamera topDownCamera;
    public CinemachineCamera sideScrollingCamera;

    public void SwapCameras(int playMode)
    {
        if(playMode == 0)
        {
            topDownCamera.Priority = 10;
            sideScrollingCamera.Priority = 0;
        }
        else
        {
            topDownCamera.Priority = 0;
            sideScrollingCamera.Priority = 10;
        }
    }
}
