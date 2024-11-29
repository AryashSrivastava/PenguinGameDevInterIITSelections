using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.HealthSystemCM;

public class GhostScript : MonoBehaviour,IGetHealthSystem
{
    private Animator animator;
    private HealthSystem healthSystem;
    private bool action=false;
    public bool DemonAttack=false;
    public GameScript sourceScript;
    
    
    
    

    public bool Actions{
        get
        {
            return action;
        }
        set
        {
            action=value;
        }
    }
    
    public Transform target;
    
    // Start is called before the first frame update
    private void Awake(){
        healthSystem=new HealthSystem(100);
        healthSystem.OnDead+=HealthSystem_OnDead;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject targetObject = GameObject.FindWithTag("Player");
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            Debug.LogError("Target with tag 'Penguin' not found.");
        }

    }
    

    // Update is called once per frame
    void Update()
    {
        
        if (DemonAttack)
        {
            // Ghost attacks the player
            if (!animator.GetBool("isAttacking"))
            {
                PerformAction("isAttacking");
                sourceScript.panelImage.enabled=true;
                sourceScript.over.enabled=true;
                Debug.Log("GAmeOver");
            }
            SetAnimatorState("isRunning", false);
        }
        else if (action && !DemonAttack)
        {
            // Ghost is chasing the player
            SetAnimatorState("isRunning", true);
            Vector2 direction = target.position - transform.position;
            direction.Normalize();
            float speed = 1f*sourceScript.difficulty;
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
        
        else
        {
            // Idle state
            SetAnimatorState("isRunning", false);
        }
        
    }
    public void Damage(){
        healthSystem.Damage(65/sourceScript.difficulty);
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
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            DemonAttack=true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            DemonAttack=false;
        }
    }
    private void HealthSystem_OnDead(object sender,System.EventArgs e){
        sourceScript.Round++;
        Destroy(gameObject);
        
    }
    public HealthSystem GetHealthSystem(){
        return healthSystem;
    }
    
}
