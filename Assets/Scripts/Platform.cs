using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameManager manager;

    public bool startingPlatform;
    private float speed = 5f;

    public bool playerLanded;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if(!manager.GameStarted) { return; }
        transform.Translate(Vector3.left * GetDifficulty() * Time.deltaTime);

       /* if(startingPlatform) {*/ Destroy(gameObject, 10);/* }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Enter");
    }

    public float GetDifficulty()
    {
        float platformSpeed = speed + (float)manager.score/5;
        return platformSpeed;
    }
}
