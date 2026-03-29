using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("基本设置")]
    public Color hoverColor = Color.yellow;
    public Color clickColor = Color.green;
    public string objectName = "物体";
    
    [Header("动画效果 - 勾选你想要的效果")]
    public bool doScale = true;        // 点击变大
    public bool doRotate = false;      // 点击旋转
    public bool doJump = false;        // 点击跳跃
    public bool doShake = false;       // 点击摇晃
    
    private Color originalColor;
    private Renderer objectRenderer;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    private bool isAnimating = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
        originalScale = transform.localScale;
        originalPosition = transform.position;
    }

    void OnMouseEnter()
    {
        if (objectRenderer != null)
            objectRenderer.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        if (objectRenderer != null && !isAnimating)
            objectRenderer.material.color = originalColor;
    }

    void OnMouseDown()
    {
        if (objectRenderer != null)
            objectRenderer.material.color = clickColor;
        
        // 触发各种效果
        if (doScale && !isAnimating) StartCoroutine(ScaleEffect());
        if (doJump && !isAnimating) StartCoroutine(JumpEffect());
        if (doRotate && !isAnimating) StartCoroutine(RotateEffect());
        if (doShake && !isAnimating) StartCoroutine(ShakeEffect());
    }
    
    System.Collections.IEnumerator ScaleEffect()
    {
        isAnimating = true;
        // 变大
        transform.localScale = originalScale * 1.3f;
        yield return new WaitForSeconds(0.15f);
        // 恢复
        transform.localScale = originalScale;
        isAnimating = false;
        
        if (objectRenderer != null)
            objectRenderer.material.color = originalColor;
    }
    
    System.Collections.IEnumerator JumpEffect()
    {
        isAnimating = true;
        float jumpHeight = 1.5f;
        float duration = 0.3f;
        
        // 上升
        float elapsed = 0;
        while (elapsed < duration/2)
        {
            transform.position = Vector3.Lerp(originalPosition, originalPosition + Vector3.up * jumpHeight, elapsed / (duration/2));
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        // 下降
        elapsed = 0;
        while (elapsed < duration/2)
        {
            transform.position = Vector3.Lerp(originalPosition + Vector3.up * jumpHeight, originalPosition, elapsed / (duration/2));
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.position = originalPosition;
        isAnimating = false;
        
        if (objectRenderer != null)
            objectRenderer.material.color = originalColor;
    }
    
    System.Collections.IEnumerator RotateEffect()
    {
        isAnimating = true;
        float rotateAmount = 360f;
        float duration = 0.5f;
        float elapsed = 0;
        Vector3 startRotation = transform.eulerAngles;
        
        while (elapsed < duration)
        {
            float yRotation = Mathf.Lerp(0, rotateAmount, elapsed / duration);
            transform.eulerAngles = startRotation + new Vector3(0, yRotation, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.eulerAngles = startRotation;
        isAnimating = false;
        
        if (objectRenderer != null)
            objectRenderer.material.color = originalColor;
    }
    
    System.Collections.IEnumerator ShakeEffect()
    {
        isAnimating = true;
        float duration = 0.3f;
        float magnitude = 0.2f;
        float elapsed = 0;
        
        while (elapsed < duration)
        {
            float x = originalPosition.x + Random.Range(-magnitude, magnitude);
            float z = originalPosition.z + Random.Range(-magnitude, magnitude);
            transform.position = new Vector3(x, originalPosition.y, z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.position = originalPosition;
        isAnimating = false;
        
        if (objectRenderer != null)
            objectRenderer.material.color = originalColor;
    }
}
