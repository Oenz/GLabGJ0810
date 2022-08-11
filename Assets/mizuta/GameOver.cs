using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private string transscene;
    public void MoveScene()
    {
        SceneManager.LoadScene(transscene);
    }
}
