using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    private float progress = 0;
    private float minScale = 0.5f;
    private float maxScale = 2.0f;

    private void Update()
    {
        var newScale = Mathf.Lerp(minScale, maxScale, progress);
        transform.localScale = Vector3.one * newScale;
    }

    public void SetProgress(float value)
    {
        progress = value;
    }
}
