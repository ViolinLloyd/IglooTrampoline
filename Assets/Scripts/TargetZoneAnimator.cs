using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetZoneAnimator : MonoBehaviour
{
    private float timer = 0.0f;
    private float scale;
    private float initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        scale = initialScale + 0.4f * Mathf.Sin(timer);
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }
}
