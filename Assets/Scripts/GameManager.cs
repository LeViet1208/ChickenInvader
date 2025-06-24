using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Replay()
    {
        Time.timeScale = 1f; // Khôi phục lại tốc độ bình thường
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
