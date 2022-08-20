using Assets._Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Animate golf messages as required.
/// </summary>
public class GolfAnimator : MonoBehaviour
{
    public enum Animations
    {
        HelloWorld,
        BallOnTee,
        BallInHole,
        SuperTubeActivated,
        HazardActivated
    }

    private Dictionary<Animations, GameObject> _animations;


    private TMPro.TextMeshProUGUI _scoreText;
    private TMPro.TextMeshProUGUI _shotsText;

    // Text components for the final shots (hole in X, etc)
    private TMPro.TextMeshProUGUI _finalShotsHeaderText;
    private TMPro.TextMeshProUGUI _finalShotsMessageText;

    private Dictionary<int, string> _shotsMessages;

    private GameObject _currentAnimation;
    private GameObject _nextAnimation;
    private bool _animationStarted;

    private void Awake()
    {
        _shotsMessages = new Dictionary<int, string>();
        _shotsMessages.Add(0, "Straight outta Hogwarts!");
        _shotsMessages.Add(1, "Babsolute Belter!");
        _shotsMessages.Add(2, "Nicely done!");
        _shotsMessages.Add(3, "That'll do nicely!");
        _shotsMessages.Add(4, "Respectable.");
        _shotsMessages.Add(5, "Number 5 is alive!");
        _shotsMessages.Add(6, "Took your time...");
        _shotsMessages.Add(7, "It's about the journey, not the destination...");


        GolfManager.Instance.SetAnimator(this);

        _animations = new Dictionary<Animations, GameObject>();
        // Finding objects like this isn't all that efficient, but doing it just the once on startup mitigates that.
        var allGameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        // Add the game object for each animation
        FindAndAddGameObjectWithName(allGameObjects, Animations.HelloWorld, "AnimHelloWorld");
        FindAndAddGameObjectWithName(allGameObjects, Animations.BallOnTee, "AnimBallOnTee");
        FindAndAddGameObjectWithName(allGameObjects, Animations.BallInHole, "AnimBallInHole");
        FindAndAddGameObjectWithName(allGameObjects, Animations.SuperTubeActivated, "AnimSuperTubeActivated");
        FindAndAddGameObjectWithName(allGameObjects, Animations.HazardActivated, "AnimHazardActivated");

        // Possible further animations:
        //   - More ball in hole animations; perhaps a generic 'fallback' + several available for common values (especially hole in 1!)


        var scoreTextObject = allGameObjects.First(g => g.name == "textScore");
        if (scoreTextObject is not null)
            _scoreText = scoreTextObject.GetComponent<TMPro.TextMeshProUGUI>();
        var shotsTextObject = allGameObjects.First(g => g.name == "textShots");
        if (shotsTextObject is not null)
            _shotsText = shotsTextObject.GetComponent<TMPro.TextMeshProUGUI>();

        var finalShotsTextHeaderObject = allGameObjects.First(g => g.name == "textHoleInX");
        if (finalShotsTextHeaderObject is not null)
            _finalShotsHeaderText = finalShotsTextHeaderObject.GetComponent<TMPro.TextMeshProUGUI>();

        var finalShotsTextMessageObject = allGameObjects.First(g => g.name == "textShotsMessage");
        if (finalShotsTextMessageObject is not null)
            _finalShotsMessageText = finalShotsTextMessageObject.GetComponent<TMPro.TextMeshProUGUI>();

    }

    private void FindAndAddGameObjectWithName(GameObject[] gameObjects, Animations key, string name)
    {
        var foundGameObject = gameObjects.First(g => g.name == name);
        if (foundGameObject is not null)
            _animations.Add(key, foundGameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Handle stopping existing animation and started new (by activating/deactvating objects
        // Doesn't apply for the score/shots
        if (!_animationStarted)
        {
            if (_currentAnimation is not null)
            {
                _currentAnimation.SetActive(false);
            }
            if (_nextAnimation is not null)
            {

                _currentAnimation = _nextAnimation;
                _currentAnimation.SetActive(true);
            }
            _animationStarted = true;
        }


        // TODO: Handle score/shots animations?
    }

    /// <summary>
    /// Triggers an animation, stopping any animation already underway.
    /// </summary>
    /// <param name="animation"></param>
    public void TriggerAnimation(Animations animation)
    {
        if (_animations.ContainsKey(animation))
        {
            _nextAnimation = _animations[animation];
            _animationStarted = false;

            if (animation == Animations.BallInHole)
            {
                // Handle ball in hole message
                if (_finalShotsHeaderText is not null)
                    _finalShotsHeaderText.SetText($"Hole in {GolfManager.Instance.Shots}");

                if (_finalShotsMessageText is not null)
                {
                    bool valueExists = _shotsMessages.TryGetValue(GolfManager.Instance.Shots, out string message);
                    _finalShotsMessageText.SetText(valueExists ? message : _shotsMessages.Last().Value);
                }
            }
        }
    }

    public void AnimateScore()
    {
        var score = GolfManager.Instance.Score;
        _scoreText.SetText($"{score} points");

        // TODO: Trigger an animation
    }
    public void AnimateShots()
    {
        var shots = GolfManager.Instance.Shots;
        _shotsText.SetText($"{shots} shots");

        // TODO: Trigger an animation
    }
}
