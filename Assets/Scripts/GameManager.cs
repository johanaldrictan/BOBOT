using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Scene scene;
    private float timeTaken;

    EnemyMovement enemyMovement;
    EnemyVision enemyVision;
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        timeTaken += Time.deltaTime;
        Debug.Log(timeTaken);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(scene.name);
    }
}
