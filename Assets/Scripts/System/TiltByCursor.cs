using UnityEngine;
using UnityEngine.EventSystems;

public class TiltByCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Настройки наклона")]
    [SerializeField] private float maxTiltAngle;
    [SerializeField] private float tiltSmoothSpeed;
    
    [Header("Настройки увеличения")]
    [SerializeField] private float hoverScale = 1.2f;
    [SerializeField] private float scaleSmoothSpeed = 10f;
    
    private RectTransform rectTransform;
    private bool isHovering = false;
    private Quaternion targetRotation;
    private Quaternion originalRotation;
    private Vector3 originalScale;
    private Vector3 targetScale;
    private Camera mainCamera;
    
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalRotation = rectTransform.localRotation;
        originalScale = rectTransform.localScale;
        targetRotation = originalRotation;
        targetScale = originalScale;
        
        mainCamera = Camera.main;
        if (mainCamera == null)
            mainCamera = FindObjectOfType<Camera>();
    }
    
    private void Update()
    {
        if (isHovering)
        {
            Vector2 tilt = CalculateTilt();
            targetRotation = Quaternion.Euler(tilt.y, tilt.x, 0f);
            targetScale = originalScale * hoverScale;
        }
        else
        {
            targetRotation = originalRotation;
            targetScale = originalScale;
        }
        
        rectTransform.localRotation = Quaternion.Slerp(
            rectTransform.localRotation, 
            targetRotation, 
            Time.deltaTime * tiltSmoothSpeed
        );
        
        rectTransform.localScale = Vector3.Lerp(
            rectTransform.localScale,
            targetScale,
            Time.deltaTime * scaleSmoothSpeed
        );
    }
    
    private Vector2 CalculateTilt()
    {
        Vector2 mousePos = Input.mousePosition;
        
        Vector3 worldCenter = rectTransform.position;
        Vector2 screenCenter = RectTransformUtility.WorldToScreenPoint(mainCamera, worldCenter);
        
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        
        Vector2 bottomLeft = RectTransformUtility.WorldToScreenPoint(mainCamera, corners[0]);
        Vector2 topRight = RectTransformUtility.WorldToScreenPoint(mainCamera, corners[2]);
        
        float width = topRight.x - bottomLeft.x;
        float height = topRight.y - bottomLeft.y;
        
        float offsetX = mousePos.x - screenCenter.x;
        float offsetY = mousePos.y - screenCenter.y;
        
        float normalizedX = Mathf.Clamp(offsetX / (width * 0.5f), -1f, 1f);
        float normalizedY = Mathf.Clamp(offsetY / (height * 0.5f), -1f, 1f);

        float tiltX = normalizedY * maxTiltAngle;
        float tiltY = -normalizedX * maxTiltAngle;
        
        return new Vector2(tiltX, tiltY);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}