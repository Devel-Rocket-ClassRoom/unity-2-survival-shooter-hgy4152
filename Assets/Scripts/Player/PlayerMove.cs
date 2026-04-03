using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerMove : MonoBehaviour
{
    public static readonly int HashMove = Animator.StringToHash("Move");

    public float moveSpeed = 5f;
    public LayerMask targetLayer;

    private Animator playerAnimator;
    private PlayerInput playerInput;
    private PlayerState playerState;
    private Rigidbody playerRigidbody;
    private Vector3 direction;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerState = GetComponent<PlayerState>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(playerState.isDead)
        {
 
            return;
        }


        playerAnimator.SetFloat(HashMove, direction.normalized.magnitude);



        // 마우스 따라 돌기
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, targetLayer))
        {
            Vector3 targetPoint = hit.point;
            targetPoint.y = transform.position.y;
            transform.LookAt(targetPoint);
        }

    }

    private void FixedUpdate()
    {
        if (playerState.isDead)
        {

            return;
        }

        direction = new Vector3(playerInput.MoveSide, 0f, playerInput.MoveForward);

        //direction = Vector3.ClampMagnitude(direction, 1f);

        Vector3 delta = moveSpeed * Time.deltaTime * direction;

        playerRigidbody.MovePosition(playerRigidbody.position + delta);



    }
}
