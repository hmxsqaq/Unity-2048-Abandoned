using System;
using Events;
using Framework.Singleton;

namespace Model
{
    public class GameModel : Singleton<GameModel>
    {
        private int[] _numberList = new int[16];

        public int this[int index]
        {
            get => _numberList[index];
            set
            {
                _numberList[index] = value;
                OnListChangeEvent.Trigger();
            }
        }
        
        private int _score = 0;
        public int Score
        {
            get => _score;
            set => _score = value;
        }
    }
}