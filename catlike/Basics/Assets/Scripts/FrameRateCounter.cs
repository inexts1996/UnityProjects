using TMPro;
using UnityEngine;

public enum DisplayMode
{
    FPS,
    MS
}

public class FrameRateCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI display = default;

    [Range(0.1f, 2f)] [SerializeField] private float sampleDuration = 1f;
    [SerializeField] private DisplayMode displayMode = DisplayMode.FPS;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private float duration;
    private int frames;
    private float bestDuration = float.MaxValue;
    private float worstDuration;
    private float frameDuration;

    private void Update()
    {
        frameDuration = Time.unscaledDeltaTime;
        duration += frameDuration;
        ++frames;

        if (frameDuration < bestDuration) bestDuration = frameDuration;

        if (frameDuration > worstDuration) worstDuration = frameDuration;

        if (duration >= sampleDuration)
        {
            if (displayMode == DisplayMode.FPS)
                display.SetText("FPS\n{0:0}\n{1:0}\n{2:0}", 1f / bestDuration, frames / duration, 1f / worstDuration);
            else
                display.SetText("MS\n{0:1}\n{1:1}\n{2:1}", 1000f * bestDuration, 1000f * duration / frames,
                    1000f * worstDuration);

            duration = 0f;
            frames = 0;
            bestDuration = float.MaxValue;
            worstDuration = 0f;
        }
    }
}