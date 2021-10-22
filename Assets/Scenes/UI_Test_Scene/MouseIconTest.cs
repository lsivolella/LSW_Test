using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseIconTest : MonoBehaviour
{
    [SerializeField] GameObject square;
    [Range(0, 2)]
    [SerializeField] float parentScale = 1;

    RectTransform squareRT;
    SpriteRenderer mouseIconSR;
    Transform mouseIconParent;

    private void Start()
    {
        squareRT = square.GetComponent<RectTransform>();
        mouseIconSR = GetComponent<SpriteRenderer>();
        mouseIconParent = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        mouseIconParent.localScale = Vector3.one * parentScale;

        Vector3 newPosition = Input.mousePosition;
        Vector3 adjustedPosition = Camera.main.ScreenToWorldPoint(newPosition);
        adjustedPosition.z = 0;
        transform.position = adjustedPosition;

        var cameraToScreenRatio = 1920 / (Camera.main.orthographicSize * Camera.main.aspect * 2);

        var horizontalOffset = mouseIconSR.sprite.bounds.size.x / 2 * cameraToScreenRatio * Vector3.right * parentScale;
        var verticalOffset = -mouseIconSR.sprite.bounds.size.y / 2 * cameraToScreenRatio * Vector3.up * parentScale + squareRT.rect.height * Vector3.up;
        Vector3 squarePosition = Input.mousePosition + horizontalOffset + verticalOffset;
        squareRT.anchoredPosition = squarePosition;
    }
}
