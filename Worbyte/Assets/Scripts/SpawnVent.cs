using UnityEngine;

public class SpawnVent : MonoBehaviour
{
    [SerializeField] private GameObject glow; // red glow child, off by default

    private void Awake()
    {
        if (glow != null) glow.SetActive(false);
    }

    public void SetWarning(bool on)
    {
        if (glow != null) glow.SetActive(on);
    }
}