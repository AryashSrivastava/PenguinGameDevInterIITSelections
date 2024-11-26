using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;  
    private bool JumpingCase=false;
    private bool JumpingCase2=false; // this is for thrustability 
    private GameObject JetPack;
    private GameObject Gun;
    private GameObject Fashion;
    private bool switchGun=false;
    private GameObject Bullet;
    public BoxScript sourceScript;
    public GhostScript source1Script;
    private bool fire=false;
    public GameObject bulletPrefab;  // Bullet prefab to instantiate
    public Transform gunTransform;  // Transform of the gun
    
    private float lastShotTime = 0f;
    
    void Start()
    {
        JetPack = transform.Find("JetPack").gameObject;
        Gun = transform.Find("Gun").gameObject;
        Fashion = transform.Find("Fashion").gameObject;
        Bullet = transform.Find("Bullet").gameObject;

        
        
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found on Player object!");
        }
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && JumpingCase==false)
        {
            SetAnimatorState("isWalking", true);
            
        }
        else
        {
            SetAnimatorState("isWalking", false);
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(sourceScript.GetThrustAbility()==true){
                JetPack.SetActive(true);
                JumpBomb();
            }else{
                Jump();
            }
            

            
        }


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PerformAction("isSliding");
        }

  
        if (fire==false && Input.GetKeyDown(KeyCode.Space))
        {
            PerformAction("isAttacking");
        }


        if(Input.GetKeyDown(KeyCode.F) && source1Script.Actions==true){
            Gun.SetActive(!switchGun);
            Fashion.SetActive(!switchGun);
            switchGun=(!switchGun);
            if(switchGun==true){
                fire=true;
            }else{
                fire=false;
            }
        }
        if(fire==true){
            AimGunAtMouse();
            if (Input.GetMouseButtonDown(0))
            {
                ShootBullet();
                
            }
        }
    }

    void Jump()
    {
        if(JumpingCase2==false){
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse); 
        }
        
    }
    void JumpBomb()
    {
        rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse); 
    }

    void PerformAction(string action)
    {
        SetAnimatorState(action, true);
        StartCoroutine(ResetAction(action));
    }

    void SetAnimatorState(string parameter, bool state)
    {
        animator.SetBool(parameter, state);
    }

    IEnumerator ResetAction(string action)
    {
        yield return new WaitForSeconds(0.5f); 
        SetAnimatorState(action, false);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground"))
        {
            JetPack.SetActive(false);
            JumpingCase = false; 
            JumpingCase2 =false;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground"))
        {
            JumpingCase = true;  
            JumpingCase2=true;
        }
    }
    void AimGunAtMouse()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the gun to the mouse
        Vector3 direction = mousePos - Gun.transform.position;

        // Calculate the angle in radians and convert to degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the gun
        Gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    void ShootBullet()
    {
        // Instantiate the bullet at the gun's position with its current rotation
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
        

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - gunTransform.position).normalized;  // Normalize to get a unit vector

        // Optional: Add force to the bullet in the direction of the mouse
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null) {
            bulletRb.velocity = direction * 100f;  // Adjust speed (10f) as needed
        }
    }
    
    
    
}
