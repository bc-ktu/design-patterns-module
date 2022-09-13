using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_CardGame_PrototypeV1
{
    internal class Hand
    {
        private GameCard[] _cards;
        private int _currentSize;
        private int _maxSize;

        public GameCard[] Cards { get { return _cards; } }
        public int MaxSize { get { return _maxSize; } }
        public int Size { get { return _currentSize; } }

        public Hand(int size)
        {
            _maxSize = size;
            _currentSize = 0;
            _cards = new GameCard[size];
        }

        public void AddCard(GameCard card)
        {
            _cards[_currentSize] = card;
            _currentSize++;
        }

        public GameCard TakeCard(int index)
        {
            GameCard cardTaken = _cards[index];
            
            for (int i = index; i < _cards.Length - 1; i++)
                _cards[i] = _cards[i + 1];
            _currentSize--;

            return cardTaken;
        }

    }
}
