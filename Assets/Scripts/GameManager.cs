using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerMovement player;
    [SerializeField] Image ret;
    Color ogColor;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        ogColor = ret.color;
    }

    void Update()
    {
        //Debug.Log(player.isHitting);
        if(player.isHitting){
            ret.color = Color.magenta; 
        } else {
            ret.color = Color.gray;
        }

        if(player.isDed){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
