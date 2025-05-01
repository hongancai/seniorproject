using UnityEngine;
using UnityEngine.InputSystem;

public class RightStickMouseControl : MonoBehaviour
{
    public InputAction rightStickAction;
    public InputAction leftTriggerAction;
    public float sensitivity = 1000f;
    public float deadZone = 0.1f;

    private bool isMousePressed = false;

    private void OnEnable()
    {
        rightStickAction.Enable();
        leftTriggerAction.Enable();
    }

    private void OnDisable()
    {
        rightStickAction.Disable();
        leftTriggerAction.Disable();
    }

    void Update()
    {
        Vector2 stickInput = rightStickAction.ReadValue<Vector2>();

        if (stickInput.magnitude >= deadZone)
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            mousePosition += new Vector3(stickInput.x, stickInput.y, 0) * sensitivity * Time.unscaledDeltaTime;
            mousePosition.x = Mathf.Clamp(mousePosition.x, 0, Screen.width);
            mousePosition.y = Mathf.Clamp(mousePosition.y, 0, Screen.height);
            Mouse.current.WarpCursorPosition(mousePosition);
        }

        float ltValue = leftTriggerAction.ReadValue<float>();

        if (ltValue > 0.5f)
        {
            if (!isMousePressed)
            {
                PressMouseButton();
                isMousePressed = true;
            }
        }
        else
        {
            if (isMousePressed)
            {
                ReleaseMouseButton();
                isMousePressed = false;
            }
        }
    }

    private void PressMouseButton()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;

        InputSystem.QueueDeltaStateEvent(mouse.leftButton, (byte)1); // 1 = 按下
    }

    private void ReleaseMouseButton()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;

        InputSystem.QueueDeltaStateEvent(mouse.leftButton, (byte)0); // 0 = 放開
    }
}