using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 offset;
   
    private void Start()
    {
        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        transform.position = player.position + player.rotation * offset;
        transform.rotation = player.rotation;
    }
}
