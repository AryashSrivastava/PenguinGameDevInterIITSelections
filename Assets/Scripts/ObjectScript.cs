using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    private bool playerInTriggerZone = false;
    private Color[] newColor;
    private Renderer objectRenderer;
    private int attacks = 0;
    private float wait = 0;
    private float waitTime = 0.5f;
    private bool isWaiting = false;

    public Player sourceScript;
    private Rigidbody2D _rb;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        _rb = sourceScript.gameObject.GetComponent<Rigidbody2D>();
        newColor = new Color[3];
        newColor[0] = new Color(1f, 0.752f, 0.796f);
        newColor[2] = new Color(0.7735849f, 0.726148f, 0.7261481f);
        newColor[1] = Color.red;
        objectRenderer.material.color = newColor[2];
    }


    void Update()
    {
        if (playerInTriggerZone && Input.GetKeyDown(KeyCode.Space) && wait <= 0 && !isWaiting)
        {

            wait = 1 + 1 / Time.deltaTime;

            StartCoroutine(WaitAndDeactivate());



        }
        if (wait > 0)
        {
            wait -= 1;
        }
    }
    IEnumerator WaitAndDeactivate()
    {
        isWaiting = true;

        yield return new WaitForSeconds(waitTime);
        if (attacks == 0)
        {
            objectRenderer.material.color = newColor[0];
            attacks += 1;
        }
        else if (attacks == 1)
        {
            objectRenderer.material.color = newColor[1];
            attacks += 1;
        }
        else
        {


            gameObject.SetActive(false);
            _rb.AddForce(Vector2.left * 5f, ForceMode2D.Impulse);
        }



        isWaiting = false;
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
}
