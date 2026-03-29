using UnityEngine;

public class MenuEnhancement : MonoBehaviour
{
    [Header("标题动画")]
    public bool enableTitleAnimation = true;
    public float breatheSpeed = 2f;
    public float breatheAmount = 0.05f;
    
    [Header("背景装饰")]
    public bool enableRotatingDecorations = true;
    public float rotationSpeed = 20f;
    
    private Vector3 originalScale;
    
    void Start()
    {
        originalScale = transform.localScale;
        
        // 如果是背景装饰，随机化旋转速度
        if (enableRotatingDecorations && rotationSpeed == 20f)
        {
            rotationSpeed = Random.Range(10f, 40f);
        }
    }
    
    void Update()
    {
        // 标题呼吸动画
        if (enableTitleAnimation)
        {
            float scale = 1 + Mathf.Sin(Time.time * breatheSpeed) * breatheAmount;
            transform.localScale = originalScale * scale;
        }
        
        // 背景装饰旋转
        if (enableRotatingDecorations)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }
}
