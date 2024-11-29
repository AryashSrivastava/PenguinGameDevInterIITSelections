using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private bool isOpenPoss = false;
    // Start is called before the first frame update
    private float waitTime = 0.5f;
    private bool isBoxOpening=false;
    private GameObject Cap;
    private bool ThrustAbility=false;
   
    
    //Getters and setters wala method
    public bool GetThrustAbility()
    {
        return ThrustAbility;
    }
    void Start()
    {
       
        Cap = transform.Find("Cap").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpenPoss && Input.GetKeyDown(KeyCode.Space) && !isBoxOpening)
        {
            StartCoroutine(OpenBox());
           
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isOpenPoss = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isOpenPoss = false;
        }

    }
    IEnumerator OpenBox()
    {
        isBoxOpening = true;
        yield return new WaitForSeconds(waitTime);
        Cap.SetActive(false);
        ThrustAbility=true;
    }
}
