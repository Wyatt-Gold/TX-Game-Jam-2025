using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName;

    void Start()
    {
        videoPlayer.loopPointReached += OnCutsceneFinished;
    }

    void OnCutsceneFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
