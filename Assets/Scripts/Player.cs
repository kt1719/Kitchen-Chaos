using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking;
    private Vector3 lastInteractDir;

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // Calculate move direction based on input
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        Debug.DrawRay(transform.position, lastInteractDir * interactDistance, Color.red, 15f);
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                Debug.Log("Interacting with " + raycastHit.transform.name);
                clearCounter.Interact();
            }
        }
        else
        {
            // No counter detected
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // Calculate move direction based on input
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

        // Calculate the distance travelled per frame
        float moveDistance = Time.deltaTime * moveSpeed;

        ///// Check for collisions using CapsuleCast //////
        float playerRadius = 0.7f;
        float playerHeight = 2f;

        // Calculate the canMove in the x direction
        Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
        bool canMoveX = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, Mathf.Abs(moveDir.x) * moveDistance);
        if (!canMoveX)
        {
            moveDir.x = 0;
        }

        // Calculate the canMove in the z direction
        Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
        bool canMoveZ = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, Mathf.Abs(moveDir.z) * moveDistance);
        if (!canMoveZ)
        {
            moveDir.z = 0;
        }

        ///////////////////////////////////////////////////

        transform.position += moveDir * moveDistance;

        isWalking = moveDir != Vector3.zero;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // Calculate move direction based on input
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
