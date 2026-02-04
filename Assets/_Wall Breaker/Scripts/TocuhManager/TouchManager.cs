using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;

    private PlayerInput playerInput;
    private InputAction tap;
    private InputAction tapPos;
    public GameObject tapEffect;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        tap = playerInput.actions["TouchPress"];
        tapPos = playerInput.actions["TouchPosition"];
        tap.performed += OnTap;
    }

    private void OnDestroy()
    {
        tap.performed -= OnTap;
    }

    private void OnTap(InputAction.CallbackContext context)
    {
        //Vector2 tapVal = tapPos.ReadValue<Vector2>();
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(tapVal.x, tapVal.y, 10f));

        //GameObject vfx = Instantiate(tapEffect, worldPos, Quaternion.identity);

        Debug.Log($"Tap");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        Vector3 move = new Vector3(input.x, 0f, input.y);
        player.transform.Translate(move * speed * Time.deltaTime);
        Debug.Log("Joystick Moving");
    }
}
