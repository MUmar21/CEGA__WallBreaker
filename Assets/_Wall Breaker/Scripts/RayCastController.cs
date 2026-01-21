using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayCastController : MonoBehaviour
{
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private ObjectPool impactVfxPool;
    [SerializeField] private LayerMask layerMask;
    private Camera playerCamera;
    private InputAction inputAction;

    [SerializeField] private float clickDelay = 0.2f;
    private float lastClickTime;

    private void Awake()
    {
        inputAction = new InputAction();
        inputAction.AddBinding("<Mouse>/leftButton");
        inputAction.performed += OnClick;
    }

    private void Start()
    {
        playerCamera = Camera.main;
        if (playerCamera == null)
        {
            Debug.LogError("Camera not Found!");
        }
        else
        {
            Debug.Log("Camera Found!");
        }

    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }


    private void OnClick(InputAction.CallbackContext context)
    {
        if (Time.time - lastClickTime < clickDelay)
            return;

        lastClickTime = Time.time;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        Ray ray = playerCamera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, layerMask))
        {
            //Debug.Log("Raycast Hit: " + hit.collider.name);
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green, 5f);

            Vector3 hitPoint = hit.point;
            //GameObject vfxObj = Instantiate(explosionVFX, hitPoint, Quaternion.identity);

            //Destroy(vfxObj, 4f);

            GameObject vfxObj = impactVfxPool.Get();
            vfxObj.transform.position = hitPoint;
            StartCoroutine(ReturnToPool(vfxObj));

            IClickable brick = hit.collider.GetComponent<IClickable>();
            if (brick != null)
            {
                Debug.Log("Detected!!!!!!!!!!!!!");
                brick.OnClicked(hit.point);
            }
        }
    }

    private IEnumerator ReturnToPool(GameObject obj)
    {
        yield return new WaitForSeconds(4f);
        impactVfxPool.Return(obj);
    }
}
