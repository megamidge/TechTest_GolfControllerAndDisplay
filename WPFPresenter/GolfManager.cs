using System;
using System.ComponentModel;
using WPFPresenter.Messages;
using WPFPresenter.Messages.Enums;

namespace WPFPresenter
{
    internal class GolfManager : Singleton<GolfManager>
    {
        public enum Animations
        {
            HelloWorld,
            BallOnTee,
            BallInHole,
            SuperTubeActivated,
            HazardActivated
        }
        public class AnimationTriggeredEventArgs
        {
            public Animations Animation;
            public AnimationTriggeredEventArgs(Animations animation)
            {
                Animation = animation;
            }
        }

        private int _score = 0;
        private int _shots = 0;

        public int Score => _score;
        public int Shots => _shots;
        public GolfManager() : base()
        {
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<AnimationTriggeredEventArgs>? AnimationTriggered;

        public void UpdateCommon(CommonMessage common)
        {
            bool scoreChanged = _score != common.Score;
            bool shotsChanged = _shots != common.Shots;

            _score = common.Score;
            _shots = common.Shots;


            if (scoreChanged)
            {
                // TODO: Animate score
                PropertyChanged?.Invoke(this, new(nameof(Score)));
            }
            if (shotsChanged)
            {
                // TODO: Animate shots
                PropertyChanged?.Invoke(this, new(nameof(Shots)));
            }
        }

        public void HandleGolfAction(CommonMessage action)
        {
            switch (action)
            {
                case FeatureActivated featureActivated:
                    switch (featureActivated.Feature)
                    {
                        case Feature.SuperTube:
                            // Trigger supertube animation
                            TriggerAnimation(Animations.SuperTubeActivated);
                            break;
                        case Feature.Hazard:
                            // Trigger hazard animation
                            TriggerAnimation(Animations.HazardActivated);
                            break;
                    }
                    break;
                case BallLocation ballLocation:
                    switch (ballLocation.Location)
                    {
                        case BallLocations.Tee:
                            // Trigger ball on tee animation
                            TriggerAnimation(Animations.BallOnTee);
                            break;
                        case BallLocations.Hole:
                            // Trigger ball in hole animation
                            TriggerAnimation(Animations.BallInHole);
                            break;
                    }
                    break;
            }
        }

        public void HandleHelloWorld()
        {
            TriggerAnimation(Animations.HelloWorld);
        }

        private void TriggerAnimation(Animations animation)
        {
            AnimationTriggered?.Invoke(this, new AnimationTriggeredEventArgs(animation));
        }
    }
}
