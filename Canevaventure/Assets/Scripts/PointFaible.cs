using UnityEngine;

public class PointFaible : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToDestroy=null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(objectToDestroy );
        }
    }
}
