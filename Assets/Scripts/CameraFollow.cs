using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour, IGameManagerObserver
{
    private GameObject umbrella;
    private GameManager gameManagerScript;
    private bool isDead;
    void Start()
    {
        umbrella = GameObject.Find("Umbrella");
        gameManagerScript = GameObject.FindObjectOfType<GameManager>();
        if (gameManagerScript != null)
            gameManagerScript.AddObserver(this);
    }
    void Update()
    {
        if (!isDead)
        {
            Vector3 preset = new Vector3(transform.position.x, umbrella.transform.position.y - 5, transform.position.z);
            transform.position = preset;
        }
    }
    public void Notify(IGameManagerObserver.ChooseEvent option)
    {
        if (option == IGameManagerObserver.ChooseEvent.death)
            isDead = true;
        if (option == IGameManagerObserver.ChooseEvent.gameContinue)
            isDead = false;
    }

    private void OnDisable()
    {
        gameManagerScript.RemoveObserver(this);
    }
}
