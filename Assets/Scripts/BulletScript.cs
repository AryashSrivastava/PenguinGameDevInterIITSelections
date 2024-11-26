using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifetime = 3f;
    void Start()
    {
        
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy bullet on collision
        if(collision.TryGetComponent(out GhostScript Ghost)){
            Ghost.Damage();
            Destroy(gameObject);
        }
    }
}
