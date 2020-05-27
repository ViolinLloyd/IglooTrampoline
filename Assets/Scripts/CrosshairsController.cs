using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairsController : MonoBehaviour
{
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(MouseIsOnscreen())
        {
            transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }


    bool MouseIsOnscreen()
    {
        Vector2 mousePos = Input.mousePosition;
        if(mousePos.x < 0 || mousePos.x >= Screen.width)
            return false;
        else if(mousePos.y < 0 || mousePos.y >= Screen.height)
            return false;
        return true;
    }
}
