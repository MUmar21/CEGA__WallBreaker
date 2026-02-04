using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputAction _touchPressAction;
    private InputAction _touchPosAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _touchPressAction = _playerInput.actions["TouchPress"];
    }

    private void OnEnable()
    {
        _touchPressAction.performed += OnTouchStart;
    }

    private void OnDisable()
    {
        _touchPressAction.performed -= OnTouchStart;
    }

    private void OnTouchStart(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = _touchPosAction.ReadValue<Vector2>();

        Debug.Log($"Screen Touch at: {screenPosition}");

        //Convert screen pixels to World Space coordinates
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));
        Debug.Log($"World Position: {worldPos}");
    }
    public void OnMove(InputAction.CallbackContext _context)
    {
        Vector2 inputVec = _context.ReadValue<Vector2>();

        Debug.Log($"Joystick Input: {inputVec}");

        Vector3 move = new Vector3(inputVec.x, 0, inputVec.y);
        transform.Translate(move * 5f * Time.deltaTime);
    }

}
