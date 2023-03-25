using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _mouseSensitivity;
    private float _xRotation;
    private float _yRotation;
    
    private const string AXIS_X = "Mouse X";
    private const string AXIS_Y = "Mouse Y";

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        var valueX = Input.GetAxis(AXIS_X) * Time.deltaTime * _mouseSensitivity;
        var valueY = Input.GetAxis(AXIS_Y) * Time.deltaTime * _mouseSensitivity;

        _xRotation -= valueY;
        _xRotation = Mathf.Clamp(_xRotation, -25f, 25f);
        
        _yRotation += valueX;
        _yRotation = Mathf.Clamp(_yRotation, -50f, 50f);

        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
    }
}