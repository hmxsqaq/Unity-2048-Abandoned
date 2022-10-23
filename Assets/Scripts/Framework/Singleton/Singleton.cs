using System;

namespace Framework.Singleton
{
    public class Singleton<T> where T : Singleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Lazy<T>().Value;
                }

                return _instance;
            }
        }
    }
}