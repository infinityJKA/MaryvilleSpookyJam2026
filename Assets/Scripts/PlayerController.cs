using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerMode
{
    TopDown,
    SideScroller
}

public class PlayerController : MonoBehaviour
{

    public PlayerMode playerMode = 0;
    public int moveSpeed;

    public Rigidbody rb;

    private InputAction moveAction;

    private InputAction swapAction;

    public CameraSwap cameraSwap;

    void OnEnable()
    {        
        rb.freezeRotation = true;

        moveAction = InputSystem.actions.FindAction("Move");
        swapAction = InputSystem.actions.FindAction("Swap");
    }

    void Update()
    {
        if (swapAction.WasPressedThisFrame())
        {
            SwapCamera();
        }
    }

    void FixedUpdate()
    {
        MovePlayer();

    }

    private void SwapCamera()
    {
        if(playerMode == 0)
        {
            playerMode = (PlayerMode)1;
        }
        else
        {
            playerMode = 0;
        }
        cameraSwap.SwapCameras((int)playerMode);
    }

    private void MovePlayer()
    {
        Vector2 moveAmount = moveAction.ReadValue<Vector2>();
        Debug.Log("Move amount: " + moveAmount);
        if(playerMode == 0)
        {
            rb.linearVelocity = new Vector3(moveAmount.x * moveSpeed, rb.linearVelocity.y, moveAmount.y * moveSpeed);
        }
        else
        {
            rb.linearVelocity = new Vector3(moveAmount.x * moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
        }
    }

}


