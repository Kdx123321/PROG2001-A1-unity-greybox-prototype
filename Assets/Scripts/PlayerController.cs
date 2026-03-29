using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 5f;  // 默认速度5，比相机慢
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    
    [Header("鼠标视角")]
    public float mouseSensitivity = 2f;
    public float lookUpLimit = 80f;
    
    private CharacterController controller;
    private Camera playerCamera;
    private Vector3 velocity;
    private float verticalRotation = 0f;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        
        // 锁定鼠标到屏幕中心
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 检查是否在地面上
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // WASD 移动
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // 重力
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // 空格跳跃
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // 鼠标视角控制
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 左右旋转（身体）
        transform.Rotate(Vector3.up * mouseX);

        // 上下旋转（相机）
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -lookUpLimit, lookUpLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // 按 ESC 解锁鼠标（用于点击UI）
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // 用于按钮调用，重新锁定鼠标
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
