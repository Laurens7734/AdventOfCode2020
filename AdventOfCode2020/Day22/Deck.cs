using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Deck
    {
        List<int> cards;
        public Deck(List<int> c)
        {
            cards = c;
        }

        public bool IsEmpty()
        {
            if (cards.Count > 0)
                return false;

            return true;
        }

        public int CardCount()
        {
            return cards.Count;
        }

        public Deck SubGameDeck(int amount)
        {
            List<int> newDeck = new List<int>();
            for(int i = 0; i < amount; i++)
            {
                newDeck.Add(cards[i]);
            }
            return new Deck(newDeck);
        }

        public int Draw()
        {
            int i = cards[0];
            cards.RemoveAt(0);
            return i;
        }

        public void AddCard(int i)
        {
            cards.Add(i);
        }

        public long CountScore()
        {
            long answer = 0;
            cards.Reverse();
            for(int i = 0; i < cards.Count; i++)
            {
                answer += cards[i] * (i + 1);
            }
            cards.Reverse();
            return answer;
        }

        public string DeckAsString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(int c in cards)
            {
                sb.Append(c);
                sb.Append(',');
            }
            return sb.ToString();
        }
    }
}
