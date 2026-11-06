using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SideScrollerController : MonoBehaviour
{
    [SerializeField] PlayerInputReader input;
    [SerializeField] float speed = 6f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float gravity = -25f;

    CharacterController cc;
    float yVel;

    void Awake() => cc = GetComponent<CharacterController>();

    void OnEnable() => input.JumpPressed += Jump;
    void OnDisable() => input.JumpPressed -= Jump;

    void Jump()
    {
        if (cc.isGrounded)
            yVel = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void Update()
    {
        if (cc.isGrounded && yVel < 0f) yVel = -2f;
        yVel += gravity * Time.deltaTime;

        cc.Move(new Vector3(input.Move.x * speed, yVel, 0f) * Time.deltaTime);

        if (input.Move.x > 0.01f) transform.rotation = Quaternion.identity;
        else if (input.Move.x < -0.01f) transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }
}