using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject Player=null;
    [SerializeField]
    private float timeOffset=0.0f;
    [SerializeField]
    private Vector3 posOffset=Vector3.zero;

    private Vector3 velocity=Vector3.zero;
    

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, Player.transform.position+posOffset, ref velocity, timeOffset);
    }
}
