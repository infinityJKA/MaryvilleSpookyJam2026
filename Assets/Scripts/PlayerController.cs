using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerMode
{
    TopDown,
    SideScroller
}

public enum Facing
{
    Left,
    Right,
    Up,
    Down
}

public static class FacingExtensions
{
    public static Vector3 ToVector3(this Facing facing)
    {
        return facing switch
        {
            Facing.Right => new Vector3(1f, 0f, 0f),
            Facing.Left => new Vector3(-1f, 0f, 0f),
            Facing.Up => new Vector3(0f, 0f, 1f),
            Facing.Down => new Vector3(0f, 0f, -1f),
            _ => Vector3.zero
        };
    }
}


public class PlayerController : MonoBehaviour
{

    [Header("Player Properties")]
    
    public int moveSpeed;
    public float interactionDistance = 1.2f;
   
   [Header("References")]
    public LevelUiCanvas levelUiCanvas;

    public Rigidbody rb;
    
    private InputAction moveAction, swapAction, jumpAction, interactAction;

    public CameraSwap cameraSwap;

    public Transform groundCheckTransform;

    public LayerMask interactableLayer;


    [Header("Automatic, don't edit in inspector")]
    private int keys = 0;
    public PlayerMode playerMode = 0;
    private bool isGrounded;
    public Facing facing = Facing.Right;
    public InteractableObject currentInteractable;

    void OnEnable()
    {        
        rb.freezeRotation = true;

        moveAction = InputSystem.actions.FindAction("Move");
        // swapAction = InputSystem.actions.FindAction("Swap");
        jumpAction = InputSystem.actions.FindAction("Jump");
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, 0.01f, -1);

        //if (swapAction.WasPressedThisFrame())
        //{
        //    SwapCamera();
        //}
        if(interactAction.WasPressedThisFrame() && currentInteractable != null)
        {
            currentInteractable.OnInteract(facing.ToVector3(), this);
        }
        else if (playerMode == PlayerMode.SideScroller)
        {
            if (jumpAction.WasPressedThisFrame() && isGrounded)
            {
                rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            }
        }

        CheckForInteractable();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    public int KeyCount() { return keys; }

    public void AddKey()
    {
        levelUiCanvas.keysText.gameObject.SetActive(true);
        keys++;
        levelUiCanvas.keysText.text = "Keys: " + keys;
    }

    public void RemoveKey()
    {
        keys--;
        if(keys <= 0)
        {
            keys = 0;
            levelUiCanvas.keysText.gameObject.SetActive(false);
        }
        else levelUiCanvas.keysText.text = "Keys: " + keys;
    }

    public void SwapCamera()
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
        //Debug.Log("Move amount: " + moveAmount);
        if(playerMode == 0) // if in topdown
        {
            rb.linearVelocity = new Vector3(moveAmount.x * moveSpeed, rb.linearVelocity.y, moveAmount.y * moveSpeed);
            
            if(Mathf.Abs(moveAmount.x) > Mathf.Abs(moveAmount.y))
            {
                if(moveAmount.x > 0)
                {
                    facing = Facing.Right;
                }
                else if(moveAmount.x < 0)
                {
                    facing = Facing.Left;
                }
            }
            else
            {
                if(moveAmount.y > 0)
                {
                    facing = Facing.Up;
                }
                else if(moveAmount.y < 0)
                {
                    facing = Facing.Down;
                }
            }
        }
        else // if in sidescroller
        {
            rb.linearVelocity = new Vector3(moveAmount.x * moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
            if(moveAmount.x > 0)
            {
                facing = Facing.Right;
            }
            else if(moveAmount.x < 0)
            {
                facing = Facing.Left;
            }
        }
    }

    void CheckForInteractable()
    {
        Ray ray = new Ray(rb.transform.position, facing.ToVector3());
        RaycastHit hit;

        Gizmos.color = Color.black;
        

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            if (hit.collider.TryGetComponent(out InteractableObject interactableObj))
            {
                interactableObj.EnableOutline();
                currentInteractable = interactableObj;
            }
        }
        else
        {
            if (currentInteractable != null) currentInteractable.DisableOutline();
            
            currentInteractable = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(rb.transform.position, facing.ToVector3() * interactionDistance);
    }

}


