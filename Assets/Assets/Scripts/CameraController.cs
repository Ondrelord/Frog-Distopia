using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed;
    public float zoomMin;
    public float zoomMax;

    public Vector2 panningSensitivity;
    public float panningSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
    }

    void CameraMovement()
    {
        // mouse position
        Vector2 panningPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        
        // check if mouse is inside the window.
        if (panningPos.x < 0f || panningPos.x > 1f || panningPos.y < 0f || panningPos.y > 1f)
            return;

        // panning horizontal
        float horizontalPanning = Input.GetAxis("Horizontal");
        if (horizontalPanning == 0f)
            if (panningPos.x >= 0f && panningPos.x < panningSensitivity.x)
                horizontalPanning -= panningSpeed;
            else if (panningPos.x <= 1f && panningPos.x > 1f - panningSensitivity.x)
                horizontalPanning += panningSpeed;
        
        // panning vertical
        float verticalPanning = Input.GetAxis("Vertical");
        if (verticalPanning == 0f)
            if (panningPos.y >= 0f && panningPos.y < panningSensitivity.y)
                verticalPanning -= panningSpeed;
            else if (panningPos.y <= 1f && panningPos.y > 1f - panningSensitivity.y)
                verticalPanning += panningSpeed;
        
        Camera.main.transform.position = Camera.main.transform.position + new Vector3(horizontalPanning, verticalPanning);

        // Zoom
        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, zoomMin, zoomMax);

    }
}
