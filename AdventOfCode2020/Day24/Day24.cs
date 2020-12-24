using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day24 : Day
    {
        
        public Day24(List<string> d)
        {
            foreach (string s in d)
            {
                FloorTile ft = FloorTile.GenerateFloorTile(s);
                ft.Flip();
            }
        }

        public string Answer1()
        {
            List<FloorTile> usedTiles = FloorTile.GetAllTiles();
            int count = 0;
            foreach (FloorTile f in usedTiles)
            {
                if (f.IsBlack)
                    count++;
            }
            return count.ToString();
        }

        public string Answer2()
        {
            foreach (FloorTile f in FloorTile.GetAllTiles())
            {
                f.GenerateNeighbours();
            }
            for (int i = 0; i < 100; i++)
            {
                foreach(FloorTile f in FloorTile.GetAllTiles())
                {
                    f.GenerateNeighbours();
                }
                FloorTile.UpdateAllTiles();
            }
            List<FloorTile> usedTiles = FloorTile.GetAllTiles();
            int count = 0;
            foreach (FloorTile f in usedTiles)
            {
                if (f.IsBlack)
                    count++;
            }
            return count.ToString();
        }
    }
}
