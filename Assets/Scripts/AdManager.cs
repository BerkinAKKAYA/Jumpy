using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class AdManager : MonoBehaviour
{
    private float adShowChance = .25f;
    private string gameId = "3258449";
    private bool testMode = false;

    private Platforms platforms;

    #region Awake & Start
    void Awake ()
    {
        platforms = FindObjectOfType<Platforms>();
    }

    void Start ()
    {
        Advertisement.Initialize (gameId, testMode);
        StartCoroutine ("ShowBannerWhenReady");
    }
    #endregion

    #region ShowAd
    public void ShowAd ()
    {
        if (Advertisement.IsReady("video"))
            Advertisement.Show("video");
        else
            StartCoroutine("ShowAdWhenReady", 1);
    }

    public void ShowAdByChance ()
    {
        if (Random.value > (1f-adShowChance))
            ShowAd();
    }

    public void ShowAdByChance (out bool shown)
    {
        if (Random.value > (1f-adShowChance))
        {
            shown = true;
            ShowAd();
        }

        shown = false;
    }
    #endregion

    #region Show Ad When Ready
    IEnumerator ShowAdWhenReady(float searchDuration)
    {
        float time = 0;

        while (time < searchDuration && !Advertisement.IsReady("video"))
        {
            time += 0.1f;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        if (Advertisement.IsReady("video"))
            Advertisement.Show("video");
    }
    #endregion

    #region Show Banner
    IEnumerator ShowBannerWhenReady ()
    {
        while (!Advertisement.IsReady ("banner"))
            yield return new WaitForSeconds (0.3f);
        
        Advertisement.Banner.Show ("banner");
        Advertisement.Banner.SetPosition (BannerPosition.BOTTOM_CENTER);
    }

    public static void HideBanner() => Advertisement.Banner.Hide();
    #endregion

    #region Handle Show Result
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                platforms.Reset();
                break;
            case ShowResult.Skipped:
                platforms.Reset();
                break;
            case ShowResult.Failed:
                platforms.Reset();
                break;
        }
    }
    #endregion
}