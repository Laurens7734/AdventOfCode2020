using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class RecursiveGame
    {
        Deck player1;
        Deck player2;
        List<string> previousDecks1 = new List<string>();
        List<string> previousDecks2 = new List<string>();
        public RecursiveGame(Deck a, Deck b)
        {
            player1 = a;
            player2 = b;
        }

        //returns 1 for player 1 win and 2 for player 2 win.
        //returns -1 in case of an error
        public int ResolveGame()
        {
            while (!(player1.IsEmpty() || player2.IsEmpty()))
            {
                if (previousDecks1.Contains(player1.DeckAsString()) && previousDecks2.Contains(player2.DeckAsString()))
                {
                    return 1;
                }
                else
                {
                    previousDecks1.Add(player1.DeckAsString());
                    previousDecks2.Add(player2.DeckAsString());
                }

                int card1 = player1.Draw();
                int card2 = player2.Draw();

                if (player1.CardCount() >= card1 && player2.CardCount() >= card2)
                {
                    RecursiveGame subgame = new RecursiveGame(player1.SubGameDeck(card1), player2.SubGameDeck(card2));
                    if (subgame.ResolveGame() == 1)
                    {
                        player1.AddCard(card1);
                        player1.AddCard(card2);
                    }
                    else if (subgame.ResolveGame() == 2)
                    {
                        player2.AddCard(card2);
                        player2.AddCard(card1);
                    }
                }
                else
                {
                    if (card1 > card2)
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
            }
            if (player1.IsEmpty())
                return 2;
            else if (player2.IsEmpty())
                return 1;
            else
                return -1;
        }

        public long CountScore()
        {
            if (player1.IsEmpty())
                return player2.CountScore();
            else if (player2.IsEmpty())
                return player1.CountScore();
            else
                return -1;
        }
    }
}
