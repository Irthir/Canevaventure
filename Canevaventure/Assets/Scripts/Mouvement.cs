using UnityEngine;

public class Mouvement : MonoBehaviour
{
    [SerializeField]
    private float MouvementVitesse = 1.0f;
    [SerializeField]
    private float ForceSaut = 1.0f;

    [SerializeField]
    private Rigidbody2D rb=null;
    [SerializeField]
    Animator Animateur=null;
    [SerializeField]
    private SpriteRenderer spriteRenderer=null;

    private Vector3 velocite=Vector3.zero;

    [SerializeField]
    public bool bDroite=false;

    [SerializeField]
    public bool bSaut = false;

    private bool bEnSaut = false;
    [SerializeField]
    private bool bAuSol = true;

    private float mouvhorizontal=0.0f;

    [SerializeField]
    private Transform verifSol=null;
    [SerializeField]
    private float verifSolRayon = 1.0f;
    [SerializeField]
    public LayerMask collisionLayers;

    void Update()
    {
        bAuSol = Physics2D.OverlapCircle(verifSol.position, verifSolRayon, collisionLayers);

        if (bSaut && bAuSol)
        {
            bEnSaut = true;
        }

        if (bDroite)
        {
            mouvhorizontal = 1 * MouvementVitesse * Time.deltaTime;
        }
        else
        {
            mouvhorizontal = 0.0f;
        }

        Flip(rb.velocity.x);

        float fVitessePersonnage = Mathf.Abs(rb.velocity.x);
        Animateur.SetFloat("Vitesse", fVitessePersonnage);
        Animateur.SetFloat("VitesseSaut", rb.velocity.y);
    }

    void FixedUpdate()
    {
        MouvementJoueur(mouvhorizontal);
    }

    void MouvementJoueur(float _mouvhorizontal)
    {
        Vector3 velocitecible = new Vector2(_mouvhorizontal, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, velocitecible, ref velocite, 0.05f);

        if (bEnSaut)
        {
            rb.AddForce(new Vector2(0f, ForceSaut));
            bEnSaut = false;
            bSaut = false;
        }
    }

    void Flip(float _velocite)
    {
        if(_velocite > -0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocite < -0.1f)
        {
            spriteRenderer.flipX = true;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(verifSol.position, verifSolRayon);
    }
}
