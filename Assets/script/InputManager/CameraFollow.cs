using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] protected Transform shipTransform;
    [SerializeField] protected Vector3 cameraOffset = new Vector3(0f, 0f, -10f);

    [Range(0f, 1f)]
    [SerializeField] protected float mouseWeight = 0.2f;
    [SerializeField] protected float maxMouseDistance = 8f;
    [SerializeField] protected float smoothTime = 0.05f; 

    private Vector3 currentVelocity;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

   
    private void LateUpdate()
    {
        if (shipTransform == null) return;

        // 1. Tính vị trí con trỏ chuột trong World Space
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = -cameraOffset.z;
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(mouseScreenPos);

        // 2. Tính khoảng cách nhìn trước (Look-ahead) theo chuột
        Vector3 mouseDir = mouseWorldPos - shipTransform.position;
        mouseDir.z = 0f;

        if (mouseDir.magnitude > maxMouseDistance)
        {
            mouseDir = mouseDir.normalized * maxMouseDistance;
        }

        // 3. Vị trí mục tiêu của Camera
        Vector3 targetPosition = shipTransform.position + (mouseDir * mouseWeight) + cameraOffset;

        // 4. Di chuyển Camera mượt mà bằng SmoothDamp trong LateUpdate với Time.unscaledDeltaTime
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref currentVelocity,
            smoothTime,
            Mathf.Infinity,
            Time.unscaledDeltaTime
        );
    }
}
