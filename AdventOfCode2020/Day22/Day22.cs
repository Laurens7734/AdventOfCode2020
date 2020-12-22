using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{ 
    class Day22 : Day
    {
        Deck player1;
        Deck player2;
        RecursiveGame game2;
        public Day22(List<string> d)
        {
            List<int> temp = new List<int>();
            foreach(string s in d)
            {
                if (s.StartsWith("Player"))
                    temp = new List<int>();
                else if (s == "")
                    player1 = new Deck(temp);
                else
                    temp.Add(int.Parse(s));
            }
            player2 = new Deck(temp);
            game2 = new RecursiveGame(player1.SubGameDeck(player1.CardCount()), player2.SubGameDeck(player2.CardCount()));
        }

        public string Answer1()
        {
            while(!(player1.IsEmpty() || player2.IsEmpty()))
            {
                int card1 = player1.Draw();
                int card2 = player2.Draw();

                if(card1 > card2)
                {
                    player1.AddCard(card1);
                    player1.AddCard(card2);
                }
                else if (card1 < card2)
                {
                    player2.AddCard(card2);
                    player2.AddCard(card1);
                }
            }
            if (player1.IsEmpty())
                return player2.CountScore().ToString();
            else if(player2.IsEmpty())
                return player1.CountScore().ToString();
            return "error";
        }

        public string Answer2()
        {
            if (game2.ResolveGame() > 0)
                return game2.CountScore().ToString();
            else
                return "Error";
        }
    }
}
