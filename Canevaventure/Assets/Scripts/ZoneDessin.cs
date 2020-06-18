using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDessin : MonoBehaviour
{
    [SerializeField]
    private GameObject Pinceau = null;
    [SerializeField]
    private int TaillePinceau = 1;
    [SerializeField]
    private Color CouleurPinceau = Color.black;
    [SerializeField]
    private Color CouleurDefaut = Color.white;
    [SerializeField]
    private bool ReinitLancement = true;
    [SerializeField]
    private LayerMask LayerDessin;

    public delegate void FonctionPinceau(Vector2 world_position);
    // This is the function called when a left click happens
    // Pass in your own custom one to change the brush type
    // Set the default function in the Awake method
    public FonctionPinceau PinceauActuel;

    private ZoneDessin dessinable = null;
    //private GameObject pinceau = null;
    private Sprite spritedessinable = null;
    private Texture2D texturedessinable = null;
    Color[] tableaudefaut=null;
    Color32[] CouleurActuelle;

    bool sourisdejaclique = false;
    bool dessinactuel = false;
    Vector2 PosPreced = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Nous vérifions tout d'abord si le joueur appuie sur le bouton de la souris.
        bool cliquesouris = Input.GetMouseButton(0);
        if (cliquesouris && !dessinactuel)
        {
            // Convertir la souris en position du monde
            Vector2 positionsouris = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // On vérifie si la souris est sur notre image.
            Collider2D hit = Physics2D.OverlapPoint(positionsouris, LayerDessin.value);
            if (hit != null && hit.transform != null)
            {
                PinceauActuel(positionsouris);
            }

            else
            {
                PosPreced = Vector2.zero;
                if (!sourisdejaclique)
                {
                    dessinactuel = true;
                }
            }
        }
        //La souris est lachée.
        else if (!sourisdejaclique)
        {
            PosPreced = Vector2.zero;
            dessinactuel = false;
        }
        sourisdejaclique = cliquesouris;
    }

    

    void Awake()
    {
        spritedessinable = this.GetComponent<SpriteRenderer>().sprite;
        texturedessinable = spritedessinable.texture;
        dessinable = this;
        PinceauActuel = DessinPinceau;
        GameObject pinceau = Pinceau;
        tableaudefaut = new Color[(int)spritedessinable.rect.width * (int)spritedessinable.rect.height];
        for (int x = 0; x < tableaudefaut.Length; x++)
            tableaudefaut[x] = CouleurDefaut;

        //Est-ce qu'on réinitialise au lancement ?
        if (ReinitLancement)
            ReinitCanvas();
    }

    private void ReinitCanvas()
    {
        texturedessinable.SetPixels(tableaudefaut);
        texturedessinable.Apply();
    }

    private void DessinPinceau(Vector2 Position)
    {
        Vector2 PosDessin = WorldToPixelCoordinates(Position);

        CouleurActuelle = texturedessinable.GetPixels32();

        if (PosPreced == Vector2.zero)
        {
            // Si c'est la première fois qu'on dessine sur notre image on fait un point là où est notre souris.
            MarquePixelCouleur(PosDessin, TaillePinceau, CouleurPinceau);
        }
        else
        {
            // Sinon on dessine une ligne par rapport à la position précédente.
            DessinLigne(PosPreced, PosDessin, TaillePinceau, CouleurPinceau);
        }
        AppliquerMarquePixel();

        PosPreced = PosDessin;
    }


    private void MarquePixelCouleur(Vector2 PosDessin, int TaillePinceau, Color CouleurPinceau)
    {
        // Voyons combien de pixels devons nous dessiner à gauche et à droite.
        int centreX = (int)PosDessin.x;
        int centreY = (int)PosDessin.y;
        //int Rayon = Mathf.Min(0, Taille - 2);

        for (int nI = centreX - TaillePinceau; nI <= centreX + TaillePinceau; nI++)
        {
            if (nI >= (int)spritedessinable.rect.width || nI < 0)
                continue;

            for (int nJ = centreY - TaillePinceau; nJ <= centreY + TaillePinceau; nJ++)
            {
                MarquePixelChange(nI, nJ, CouleurPinceau);
            }
        }
    }

    private void MarquePixelChange(int nX, int nY, Color Couleur)
    {
        int posTableau = nY * (int)spritedessinable.rect.width + nX;

        // On vérifie si on est à une position valide.
        if (posTableau> CouleurActuelle.Length || posTableau < 0)
            return;

        CouleurActuelle[posTableau] = Couleur;
    }

    private void AppliquerMarquePixel()
    {
        texturedessinable.SetPixels32(CouleurActuelle);
        texturedessinable.Apply();
    }

    private void DessinLigne(Vector2 pntdepart, Vector2 pntfin, int nLargeur, Color Couleur)
    {
        // On obtient la distance entre les deux points;
        float fDistance = Vector2.Distance(pntdepart, pntfin);
        Vector2 Direction = (pntdepart - pntfin).normalized;

        Vector2 positionactuelle = pntdepart;
        
        float Etapes = 1 / fDistance;

        for (float etape = 0; etape <= 1; etape += Etapes)
        {
            positionactuelle = Vector2.Lerp(pntdepart, pntfin, etape);
            MarquePixelCouleur(positionactuelle, nLargeur, Couleur);
        }
    }

    private Vector2 WorldToPixelCoordinates(Vector2 world_position)
    {
        // Change coordinates to local coordinates of this image
        Vector3 local_pos = transform.InverseTransformPoint(world_position);

        // Change these to coordinates of pixels
        float pixelWidth = spritedessinable.rect.width;
        float pixelHeight = spritedessinable.rect.height;
        float unitsToPixels = pixelWidth / spritedessinable.bounds.size.x * transform.localScale.x;

        // Need to center our coordinates
        float centered_x = local_pos.x * unitsToPixels + pixelWidth / 2;
        float centered_y = local_pos.y * unitsToPixels + pixelHeight / 2;

        // Round current mouse position to nearest pixel
        Vector2 pixel_pos = new Vector2(Mathf.RoundToInt(centered_x), Mathf.RoundToInt(centered_y));

        return pixel_pos;
    }
}
