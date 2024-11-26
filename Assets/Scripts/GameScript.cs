using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{

    public GhostScript sourceScript;
    public GameObject enemyPrefab; // Drag your enemy prefab here
    public Transform spawnPoint;
    public int difficulty=1;

    void Start()
    {
        
        
        
        
    }

  
    void Update()
    {
       if(sourceScript.Round2==true){
            SpawnEnemy();
            difficulty++;
            sourceScript.Round2=false;

       }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            sourceScript.Actions=true;
          
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            sourceScript.Actions=false;
 
        }
    }
    public void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnPoint != null)
        {
            // Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            GameObject ghostInstance = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            GhostScript ghostScript = ghostInstance.GetComponent<GhostScript>();
            if (ghostScript != null)
            {
                ghostScript.target = GameObject.Find("penguin_idle_01").transform;
            }

        }
    }
}
