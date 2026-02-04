using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField]private float speed = 50f;
    float horizontal;
    float vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        //vertical= Input.GetAxis("Vertical");
        //horizontal = Mathf.Clamp01(horizontal);
        Move();

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Move()
    {
        if(animator != null) 
            animator.SetFloat("Speed", horizontal);
    }

    private void Jump()
    {
        if(animator != null)
            animator.SetTrigger("JUMP");
    }

}
