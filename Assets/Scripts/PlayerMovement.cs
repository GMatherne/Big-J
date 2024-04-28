using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    public Transform orientation;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMovementSpeedMultiplier;
    private bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    public LayerMask obstacles;
    private bool grounded;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rigidBody;

    private void Start(){
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        readyToJump = true;
    }

    private void Update(){

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground) || Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, obstacles);

        GetInput();
        LimitSpeed();

        if(grounded){
            rigidBody.drag = groundDrag;
        }else{
            rigidBody.drag = 0f;
        }
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void GetInput(){

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded){

            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);

        }

    }

    private void MovePlayer(){

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
       
        if(grounded){

            rigidBody.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Force);
       
        }else if(!grounded){

            rigidBody.AddForce(moveDirection.normalized * movementSpeed * 10f * airMovementSpeedMultiplier, ForceMode.Force);
    
        }

    }

    private void LimitSpeed(){
       
        Vector3 velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        if(velocity.magnitude > movementSpeed){
            Vector3 limitedVelocity = velocity.normalized * movementSpeed;
            rigidBody.velocity = new Vector3(limitedVelocity.x, rigidBody.velocity.y, limitedVelocity.z);
        }

    }

    private void Jump(){

        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    private void ResetJump(){
        readyToJump = true;
    }
}