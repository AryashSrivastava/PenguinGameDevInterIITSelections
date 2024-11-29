using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private Color[] newColor;
    private Camera mainCamera;
    private bool playerInTriggerZone = false;
    public bool refTrigger{
        get{
            return playerInTriggerZone;
        }
        set{
            playerInTriggerZone=value;
        }
    }
    private int i = 0;
    private float wait=0;
    private Light directionalLight;
    void Start()
    {
        directionalLight = GameObject.Find("Directional Light").GetComponent<Light>();
        directionalLight.enabled = false;
        newColor=new Color[2];
        newColor[0]=Color.black;
        newColor[1]=new Color(0.6886f,0.596f,0.198f);
        
        mainCamera = Camera.main;
        mainCamera.backgroundColor = newColor[i];
    }

    void Update()
    {
        
        if (playerInTriggerZone && Input.GetKeyDown(KeyCode.F) && wait<=0)
        {
            i^=1;
            wait=1+1/Time.deltaTime;
            
            mainCamera.backgroundColor = newColor[i];
            ToggleLight();
        }
        if(wait>0){
            wait-=1;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInTriggerZone = true;  
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInTriggerZone = false;  
        }
    }
    private void ToggleLight()
    {
        if (directionalLight != null)
        {
            directionalLight.enabled = !directionalLight.enabled;
        }
    }
}
