using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{

    public GhostScript sourceScript;
    public GameObject enemyPrefab1; // Drag your enemy prefab here
    public GameObject enemyPrefab2;
    // Get the Image component of the panel
    

    


    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public float difficulty=1f;

    private int round=1;
    private int wait_for_round=1;
    public int Round{
        get
        {
            return round;
        }
        set
        {
            round=value;
        }
    }
    public Text scoreText;  // Reference to the TextMeshProUGUI component to display the score
    
    private float score = 0;  // Your score variable
    public GameObject panelGameObject;
    public Image panelImage;
 
    public Text over;
    void Start()
    {
        // Initialize the score display
        scoreText.enabled = false;
        over.enabled=false;
        UpdateScoreText();
        panelImage = panelGameObject.GetComponent<Image>();

        // Disable the Image (rendering is disabled, but GameObject is still active)
        panelImage.enabled = false;
        // Enable the Image again
        
    }

        
    

    // Update the text displaying the score
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
  
    void Update()
    {
        if(sourceScript.DemonAttack==true){
            panelImage.enabled = true;
        }
       if(Round!=wait_for_round){
        
            SpawnEnemy();
            score+=difficulty*100f;
            UpdateScoreText();
            Round++;
            difficulty+=1;
            wait_for_round=Round;
            

       }
       if (sourceScript.Actions==true){
              // Disables the Text component, making it invisible
            scoreText.enabled = true;   // Enables the Text component, making it visible again

       }
       // For demonstration: Increment score over time (you can replace this with your own logic)
        
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
    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the player is inside the trigger area
        if (other.gameObject.CompareTag("Player"))
        {
            // Automatically trigger the enemy to follow the player while the player stays in the trigger
            sourceScript.Actions = true;
        }
    }
    public void SpawnEnemy()
    {
        
        
            // Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            Transform[] spawnPoints = new Transform[] { spawnPoint1, spawnPoint2,spawnPoint3,spawnPoint4 };
            GameObject[] enemyPrefabs = new GameObject[] { enemyPrefab1, enemyPrefab2 };
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            int randomIndex2 = Random.Range(0, spawnPoints.Length);
            GameObject ghostInstance = Instantiate(enemyPrefabs[randomIndex], spawnPoints[randomIndex2].position, Quaternion.identity);
            GhostScript ghostScript = ghostInstance.GetComponent<GhostScript>();
            if (ghostScript != null)
            {
                // Assign this GameScript to the GhostScript's sourceScript field
                ghostScript.sourceScript = this;

                // Add the GhostScript instance to this GameScript
                sourceScript = ghostScript; // Store the reference for future use
            }
            else
            {
                Debug.LogError("GhostScript component not found on the enemy prefab.");
            }

        
    }
}
