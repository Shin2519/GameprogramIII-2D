using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class fader : MonoBehaviour
{
    public Image Fade;
    public float fadeDuration;

    private void Awake()
    {
        Fade.color = new Color(0,0,0,1);
    }

    private void Start()
    {
        FadeOut(this.GetCancellationTokenOnDestroy()).Forget();
        Debug.Log("ŒÄ‚Ño‚µ‚½");
    }
    public async UniTask FadeOut(CancellationToken ct)
    {
        Debug.Log("ŒÄ‚Ño‚³‚ê‚½");
        float rest = fadeDuration;
        while(rest > 0f)
        {
            Fade.color = new Color(0, 0, 0, rest / fadeDuration);
            await UniTask.DelayFrame(1, cancellationToken: ct);
            rest -= Time.deltaTime;

           
            Fade.color = new Color(0, 0, 0, 0);
        }
        Debug.Log("ˆ—I—¹");
    }
}
