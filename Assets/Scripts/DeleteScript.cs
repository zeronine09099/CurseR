using UnityEngine;

public class DeleteScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
           other.gameObject.GetComponent<SpawnScript>().Delete(other.gameObject);
        }
    }
}
