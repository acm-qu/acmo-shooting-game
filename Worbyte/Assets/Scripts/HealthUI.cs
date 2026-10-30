using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth player;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private void Start()
    {
        player.OnHealthChanged += Refresh;
        Refresh(player.CurrentHealth, player.MaxHealth);
    }

    private void OnDestroy()
    {
        if (player != null) player.OnHealthChanged -= Refresh;
    }

    private void Refresh(int current, int max)
    {
        for (int i = 0; i < hearts.Length; i++)
            hearts[i].sprite = i < current ? fullHeart : emptyHeart;
    }
}