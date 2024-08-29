using UnityEngine.UI;
using UnityEngine;

public class WindowMobileController : View
{
    [SerializeField] private FixedJoystick _zoom;
    [SerializeField] private FixedJoystick _right;

    public delegate void CameraSwitchDelegate();
    public event CameraSwitchDelegate OnCameraSwitch;

    private void Start()
    {
        
    }
    public Vector2 Rotate => new(-_right.Horizontal, -_right.Vertical);
    public Vector2 Move => new(_right.Horizontal, _right.Vertical);
    public float Zoom => _zoom.Vertical;
}