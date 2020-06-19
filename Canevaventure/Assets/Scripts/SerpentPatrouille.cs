using UnityEngine;

public class SerpentPatrouille : MonoBehaviour
{
    [SerializeField]
    private float speed=0.0f;
    [SerializeField]
    private Transform[] waypoints=null;

    [SerializeField]
    private SpriteRenderer graphics=null;
    private Transform target;
    private int destPoint=0;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //Si l'ennemi est presque sur sa destination alors :
        if (Vector3.Distance(transform.position, target.position) <0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX;
        }
    }
}
