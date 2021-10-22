using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseIconUITest : MonoBehaviour
{
    [SerializeField] GameObject square;

    Image squareImage;

    private void Start()
    {
        squareImage = square.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.mousePosition + " " + squareImage.sprite.rect.width / 2 * Vector3.right);

        Vector3 newPosition = Input.mousePosition;
        transform.position = newPosition;

        Vector3 squareNewPosition = Input.mousePosition + square.GetComponent<RectTransform>().rect.width * Vector3.right;
        square.transform.position = squareNewPosition;
    }
}
