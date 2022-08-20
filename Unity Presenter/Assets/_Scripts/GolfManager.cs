using Assets._Scripts.Messages;
using Assets._Scripts.Messages.Enums;

namespace Assets._Scripts
{
    public enum GolfAnimations
    {
        HelloWorld,
        OnTee,
        InHole,
        SuperTubeActivated,
        HazardActivated
    }

    public class GolfManager : Singleton<GolfManager>
    {
        private int _score = 0;
        private int _shots = 0;

        private GolfAnimator _animator;

        public int Score => _score;
        public int Shots => _shots;

        public GolfManager() : base()
        {

        }

        /// <summary>
        /// Update variables available in the common data message that all golf messages derive.
        /// </summary>
        /// <param name="common"></param>
        public void UpdateCommon(CommonMessage common)
        {
            bool scoreChanged = _score != common.Score;
            bool shotsChanged = _shots != common.Shots;

            _score = common.Score;
            _shots = common.Shots;

            if (scoreChanged)
                _animator.AnimateScore();

            if (shotsChanged)
                _animator.AnimateShots();
        }

        /// <summary>
        /// Setup reference to the animator instance that will run animations.
        /// </summary>
        /// <param name="animator"></param>
        public void SetAnimator(GolfAnimator animator)
        {
            _animator = animator;
        }

        public void HandleGolfAction(CommonMessage action)
        {
            switch (action)
            {
                case FeatureActivated featureActivated:
                    switch (featureActivated.Feature)
                    {
                        case Feature.SuperTube:
                            _animator.TriggerAnimation(GolfAnimator.Animations.SuperTubeActivated);
                            break;
                        case Feature.Hazard:
                            _animator.TriggerAnimation(GolfAnimator.Animations.HazardActivated);
                            break;
                    }
                    break;
                case BallLocation ballLocation:
                    switch (ballLocation.Location)
                    {
                        case BallLocations.Tee:
                            _animator.TriggerAnimation(GolfAnimator.Animations.BallOnTee);
                            break;
                        case BallLocations.Hole:
                            _animator.TriggerAnimation(GolfAnimator.Animations.BallInHole);
                            break;
                    }
                    break;
                default:
                    // No action required
                    break;
            }
        }

        /// <summary>
        /// Special method just for hello world :)
        /// </summary>
        public void HandleHelloWorld()
        {
            _animator?.TriggerAnimation(GolfAnimator.Animations.HelloWorld);
        }
    }
}
