using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpritePatron : MonoBehaviour
{
    [SerializeField]
    private GameObject Patron = null;
    private SpriteRenderer Sprite=null;

    // Start is called before the first frame update
    void Start()
    {
        Sprite = Patron.GetComponent<SpriteRenderer>();
    }

    public void SetSprite()
    {
        Sprite.sprite = this.GetComponent<SpriteRenderer>().sprite;
    }
}
