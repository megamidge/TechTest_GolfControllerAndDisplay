namespace Assets._Scripts
{
    public class Singleton<TSingleton> where TSingleton : class, new()
    {
        private static TSingleton _instance;
        public static TSingleton Instance
        {
            get
            {
                if (_instance is null)
                    _instance = new TSingleton();
                return _instance;
            }
        }
    }
}
