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

    public PlayerMode playerMode;
    public int moveSpeed;

    public Rigidbody rb;

    private InputAction moveAction;


    void OnEnable()
    {        
        rb.freezeRotation = true;

        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        MovePlayer();

    }

    private void MovePlayer()
    {
        Vector2 moveAmount = moveAction.ReadValue<Vector2>();
        Debug.Log("Move amount: " + moveAmount);

        rb.linearVelocity = new Vector3(moveAmount.x * moveSpeed, rb.linearVelocity.y, moveAmount.y * moveSpeed);
    }

}


