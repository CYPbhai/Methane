using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;

    [SerializeField] float scaleSpeed = 0.2f;
    [SerializeField] float scaleLerp = 5f;
    [SerializeField] Vector2 scaleLimits = new Vector2(0.5f, 3f);

    private Rigidbody rb;
    private Vector3 targetScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        targetScale = transform.localScale;
        rb.useGravity = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        HandleRotationInput();
        HandleScrollScaling();
        HandleClicks();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    void LateUpdate()
    {
        transform.localScale = Vector3.Lerp(transform.localScale,targetScale,Time.deltaTime * scaleLerp);
    }

    void HandleRotationInput()
    {
        if (Input.GetMouseButton(0))
        {
            float dx = Input.GetAxis("Mouse X");
            float dy = Input.GetAxis("Mouse Y");

            Vector3 torque = (Vector3.up * -dx + Vector3.right * dy) * rotationSpeed;

            rb.AddTorque(torque, ForceMode.VelocityChange);
        }
    }

    void HandleScrollScaling()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scroll) > 0.01f)
        {
            float scaleFactor = 1f + scroll * scaleSpeed;
            targetScale *= scaleFactor;
            float s = Mathf.Clamp(targetScale.x, scaleLimits.x, scaleLimits.y);
            targetScale = Vector3.one * s;
        }
    }

    void HandleClicks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject.CompareTag("Hydrogen"))
                {
                    UI.Instance.ShowHydrogenPanel();
                }
                else if (clickedObject.CompareTag("Carbon"))
                {
                    UI.Instance.ShowCarbonPanel();
                }
                else if (clickedObject.CompareTag("Bond"))
                {
                    UI.Instance.ShowBondPanel();
                }
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            UI.Instance.Hide();
        }
    }
}
