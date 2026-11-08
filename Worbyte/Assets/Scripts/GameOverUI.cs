using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth player;
    [SerializeField] private GameObject panel;

    private void Start()
    {
        panel.SetActive(false);
        player.OnDied += Show;
    }

    private void OnDestroy()
    {
        if (player != null) player.OnDied -= Show;
    }

    private void Show()
    {
        panel.SetActive(true);
        Time.timeScale = 0f; // freezes bugs and the spawner
    }

    // Hook this to the Restart button's OnClick
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}