using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _scrollCoefficient = 1000f;
    private float _scrollSpeed = 5f;
    private float _minYPosition = 15f;
    private float _maxYPosition = 70f;
    private float _panoramicSpeed = 30f;
    private float _panoramicBoardThickness = 4f; // толщина полосы, где курсор регистрирует подведение к краю экрана
    private bool _movementIsActive = true;


    private void Update()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKey(KeyCode.Escape))
            _movementIsActive = !_movementIsActive;

        if (!_movementIsActive)
            return;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - _panoramicBoardThickness)
            transform.Translate(Vector3.forward * _panoramicSpeed * Time.deltaTime, Space.World);
        
        if (Input.GetKey("s") || Input.mousePosition.y <= _panoramicBoardThickness)
            transform.Translate(Vector3.back * _panoramicSpeed * Time.deltaTime, Space.World);
        
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - _panoramicBoardThickness)
            transform.Translate(Vector3.right * _panoramicSpeed * Time.deltaTime, Space.World);
        
        if (Input.GetKey("a") || Input.mousePosition.x <= _panoramicBoardThickness)  
            transform.Translate(Vector3.left * _panoramicSpeed * Time.deltaTime, Space.World);
        

        float _scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 _position = transform.position;

        _position.y -= _scroll * _scrollSpeed * Time.deltaTime * _scrollCoefficient;
        _position.y = Mathf.Clamp(_position.y, _minYPosition, _maxYPosition);          //зажимаем возможность прокрутки в фиксированных значениях
        
        transform.position = _position;
    }
}
