using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // 确保物体有刚体组件
public class ObjectController : MonoBehaviour
{
    [Header("控制参数")]
    [Tooltip("每次旋转的角度")] 
    public float rotationAngle = 30f;
    [Tooltip("每次移动的距离")] 
    public float moveDistance = 0.5f;
    [Tooltip("每次缩放的比例")] 
    public float scaleFactor = 0.2f;
    [Tooltip("最小缩放尺寸")] 
    public float minScale = 0.3f;
    [Tooltip("最大缩放尺寸")] 
    public float maxScale = 3f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;
    private Rigidbody rb;

    void Start()
    {
        // 获取刚体组件
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // 设置为运动学刚体
        
        // 保存初始状态
        StoreOriginalState();
    }

    void StoreOriginalState()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;
    }

    // 旋转控制
    public void RotateLeft() => RotateObject(-rotationAngle);
    public void RotateRight() => RotateObject(rotationAngle);
    
    private void RotateObject(float angle)
    {
        transform.Rotate(Vector3.up, angle, Space.World);
    }

    // 移动控制
    public void MoveLeft() => MoveObject(Vector3.left * moveDistance);
    public void MoveRight() => MoveObject(Vector3.right * moveDistance);
    
    private void MoveObject(Vector3 direction)
    {
        transform.Translate(direction, Space.World);
    }

    // 缩放控制
    public void ScaleUp() => ScaleObject(1 + scaleFactor);
    public void ScaleDown() => ScaleObject(1 - scaleFactor);
    
    private void ScaleObject(float factor)
    {
        Vector3 newScale = transform.localScale * factor;
        newScale = Vector3.Max(newScale, Vector3.one * minScale);
        newScale = Vector3.Min(newScale, Vector3.one * maxScale);
        transform.localScale = newScale;
    }

    // 重置功能
    public void ResetObject()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        transform.localScale = originalScale;
        
        // 重置物理状态（如果有）
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}