using System.Collections;
using UnityEngine;

public class GameOverAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform gameWord;
    [SerializeField] private RectTransform overWord;
    [SerializeField] private float offscreenDistance = 1500f;
    [SerializeField] private float slideTime = 0.6f;
    [SerializeField] private float delayBetween = 0.15f;
    [SerializeField] private Vector4 bezier = new Vector4(0.61f, -0.01f, 0f, 0.99f);
    private Vector2 gameHome;
    private Vector2 overHome;

    private void Awake()
    {
        gameHome = gameWord.anchoredPosition;
        overHome = overWord.anchoredPosition;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        Vector2 gameStart = gameHome + Vector2.left * offscreenDistance;
        Vector2 overStart = overHome + Vector2.right * offscreenDistance;

        gameWord.anchoredPosition = gameStart;
        overWord.anchoredPosition = overStart;

        yield return Slide(gameWord, gameStart, gameHome);
        yield return new WaitForSecondsRealtime(delayBetween);
        yield return Slide(overWord, overStart, overHome);
    }

    private IEnumerator Slide(RectTransform rt, Vector2 from, Vector2 to)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / slideTime;
            float eased = CubicBezier.Evaluate(bezier.x, bezier.y, bezier.z, bezier.w, t);
            rt.anchoredPosition = Vector2.LerpUnclamped(from, to, eased);
            yield return null;
        }
        rt.anchoredPosition = to;
    }
}