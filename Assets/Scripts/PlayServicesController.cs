using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayServicesController : MonoBehaviour
{

    public static PlayServicesController Instance = null;
    public bool IsLoggedIn { get; private set; }

    void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
            //if not, set instance to this
            Instance = this;
        //If instance already exists and it's not this:
        else if (Instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // requests the email address of the player be available.
        // Will bring up a prompt for consent.
        .RequestEmail()
        // requests a server auth code be generated so it can be passed to an
        //  associated back end server application and exchanged for an OAuth token.
        .RequestServerAuthCode(false)
        // requests an ID token be generated.  This OAuth token can be used to
        //  identify the player to other services such as Firebase.
        .RequestIdToken()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);

        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
    
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        GooglePlayLogin();
    }

    public void GooglePlayLogin()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            IsLoggedIn = success;
            Debug.Log(success.ToString());
            // handle success or failure
        });
    }

    public void PostScoreToLeaderboard(long score, string leaderboardId)
    {
        Social.ReportScore(score, leaderboardId, (bool success) =>
        {
            // handle success or failure
        });
    }

    public void ShowLeaderboard(string leaderboardId)
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardId);
    }
    public void ShowAllLeaderboards()
    {
        Social.ShowLeaderboardUI();
    }
}
