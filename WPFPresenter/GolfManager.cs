using WPFPresenter.Messages;
using WPFPresenter.Messages.Enums;

namespace WPFPresenter
{
    internal class GolfManager : Singleton<GolfManager>
    {
        private int _score = 0;
        private int _shots = 0;

        public int Score => _score;
        public int Shots => _shots;

        public GolfManager() : base()
        {

        }

        public void UpdateCommon(CommonMessage common)
        {
            bool scoreChanged = _score != common.Score;
            bool shotsChanged = _shots != common.Shots;

            _score = common.Score;
            _shots = common.Shots;

            if (scoreChanged)
            {
                // TODO: Animate score
            }
            if (shotsChanged)
            {
                // TODO: Animate shots
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
                            break;
                        case Feature.Hazard:
                            // Trigger hazard animation
                            break;
                    }
                    break;
                case BallLocation ballLocation:
                    switch (ballLocation.Location)
                    {
                        case BallLocations.Tee:
                            // Trigger ball on tee animation
                            break;
                        case BallLocations.Hole:
                            // Trigger ball in hole animation
                            break;
                    }
                    break;
            }
        }
    }
}
