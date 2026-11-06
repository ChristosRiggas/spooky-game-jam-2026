using UnityEngine;

public class SimpleFollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0f, 2f, -10f);
    [SerializeField] float smoothTime = 0.2f;

    Vector3 velocity;

    void LateUpdate()
    {
        Vector3 goal = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, goal, ref velocity, smoothTime);
    }
}