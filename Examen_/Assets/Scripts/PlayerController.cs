using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public bool canMove;
    bool isRunning;
    public float moveSpeed, originalMoveSpeed;
    private Vector2 currentMovementInput;
    public float jumpForce;
    public LayerMask groundLayerMask,enemyLayer;

    [Header("Look")]
    [SerializeField] float sensitivity = 2f;
    [SerializeField] float yRotationLimit = 90f;
    [SerializeField] Camera cam;
    Vector2 rotation;
    public float lookSensitivity;

    public float attackRange;
    private Vector2 mouseDelta;
    public GameObject Crosshair;

    [HideInInspector]
    public bool canLook = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
        originalMoveSpeed = moveSpeed;
        canMove = true;
    }
    private Rigidbody rig;
    public static PlayerController instance;
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();

        instance = this;
    }
    void Update()
    {
        Move();
        if (canLook == true)
        {
            CameraLook();
        }
    }

    private void Move()
    {
        if(canMove){
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = originalMoveSpeed * 2;
            }
            else
            {
                moveSpeed = originalMoveSpeed;
            }
            Vector3 dir = transform.forward * currentMovementInput.y + transform.right * currentMovementInput.x;
            dir *= moveSpeed;
            dir.y = rig.velocity.y;

            rig.velocity = dir;
        }
    }

    private void CameraLook()
    {
        rotation.x += Input.GetAxis("Mouse X") * sensitivity;
        rotation.y += Input.GetAxis("Mouse Y") * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        transform.rotation = Quaternion.Euler(0, rotation.x, 0);
        cam.transform.rotation = Quaternion.Euler(-rotation.y, rotation.x, 0);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            currentMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            currentMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (isGrounded() == true)
            {
                rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

     bool isGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray (transform.position + (transform.forward * 0.2f)+(Vector3.up*0.01f),Vector3.down*8),
            new Ray(transform.position + (-transform.forward * 0.2f)+(Vector3.up*0.01f),Vector3.down*8),
            new Ray(transform.position + (transform.right * 0.2f)+(Vector3.up*0.01f),Vector3.down * 8),
            new Ray(transform.position + (-transform.right * 0.2f)+(Vector3.up*0.01f),Vector3.down*8)

        };
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 8, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down*7);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down * 7);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down * 7);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down * 7);

    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
        canMove = !toggle;
        Crosshair.SetActive(!toggle);
    }
    public void Attack(int weaponDamage)
    {
        rig.velocity = Vector3.zero;
        Invoke("DealDamage", weaponDamage);
    }
    public void DealDamage(int weaponDamage)
    {
        Collider[] hitEnemy = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        Debug.Log("Enemigos detectados");
        foreach (Collider enemy in hitEnemy)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(weaponDamage);
            }
        }
    }
}