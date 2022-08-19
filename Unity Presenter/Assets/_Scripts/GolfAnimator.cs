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

    private GameObject _currentAnimation;
    private GameObject _nextAnimation;
    private bool _animationStarted;

    private void Awake()
    {
        GolfManager.Instance.SetAnimator(this);

        _animations = new Dictionary<Animations, GameObject>();
        // Finding objects like this isn't all that efficient, but 
        var animHelloWorld = Resources.FindObjectsOfTypeAll<GameObject>().First(g => g.name == "AnimHelloWorld");
        if (animHelloWorld is not null)
            _animations.Add(Animations.HelloWorld, animHelloWorld);
        
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
        }
    }
}
