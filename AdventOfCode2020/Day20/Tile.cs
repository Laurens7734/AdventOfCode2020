using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Tile
    {
        public int Id { get; }
        public List<string> image;
        string[] borders;
        public bool IsLocked { get; private set; }

        public Tile(int id)
        {
            Id = id;
            image = new List<string>();
        }

        public void AddLine(string line)
        {
            image.Add(line);
        }

        public void GenerateBorders()
        {
            if (IsLocked)
                return;
            borders = new string[8];
            borders[0] = image[0];
            borders[1] = Reverse(image[0]);
            borders[2] = image[image.Count - 1];
            borders[3] = Reverse(image[image.Count - 1]);
            StringBuilder left = new StringBuilder();
            StringBuilder right = new StringBuilder();
            foreach(string s in image)
            {
                left.Append(s[0]);
                right.Append(s[s.Length - 1]);
            }
            borders[4] = left.ToString();
            borders[5] = Reverse(left.ToString());
            borders[6] = right.ToString();
            borders[7] = Reverse(right.ToString());
        }

        public string[] GetBorders()
        {
            return borders;
        }

        string Reverse(string s)
        {
            char[] c = s.ToCharArray();
            Array.Reverse(c);
            return new string(c);
        }

        public List<string> GetBorderlessImage()
        {
            List<string> answer = new List<string>();
            for(int i = 1; i < image.Count-1; i++)
            {
                answer.Add(image[i].Substring(1, image[i].Length - 2));
            }
            return answer;
        }

        public bool IsNextPiece(string border, int pos)
        {
            int mypos = -1;
            for(int i = 0; i < 8; i++)
            {
                if (border == borders[i])
                    mypos = i;
            }

            if(mypos != -1)
            {
                if (mypos == 0)
                {
                    RotateClockwise(270);
                    FlipVertical();
                }
                else if (mypos == 1)
                {
                    RotateClockwise(270);
                }
                else if (mypos == 2)
                {
                    RotateClockwise(90);
                }
                else if (mypos == 3)
                {
                    RotateClockwise(90);
                    FlipVertical();
                }
                else if (mypos == 5)
                {
                    FlipVertical();
                }
                else if (mypos == 6)
                {
                    FlipVertical();
                    RotateClockwise(180);
                }
                else if (mypos == 7)
                    RotateClockwise(180);

                if (pos == 1)
                {
                    RotateClockwise(90);
                    FlipHorizontal();
                }
                Lock();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Lock()
        {
            IsLocked = true;
            string[] temp = new string[4];
            temp[0] = borders[0];
            temp[1] = borders[2];
            temp[2] = borders[4];
            temp[3] = borders[6];
            borders = temp;
        }

        void FlipHorizontal()
        {
            if (IsLocked)
                return;
            for(int i = 0; i < image.Count; i++) 
            {
                image[i] = Reverse(image[i]);
            }
            string temp = borders[4];
            borders[4] = borders[6];
            borders[6] = temp;

            temp = borders[5];
            borders[5] = borders[7];
            borders[7] = temp;

            temp = borders[0];
            borders[0] = borders[1];
            borders[1] = temp;

            temp = borders[2];
            borders[2] = borders[3];
            borders[3] = temp;
        }

        void FlipVertical()
        {
            if (IsLocked)
                return;
            image.Reverse();
            string temp = borders[0];
            borders[0] = borders[2];
            borders[2] = temp;

            temp = borders[1];
            borders[1] = borders[3];
            borders[3] = temp;

            temp = borders[4];
            borders[4] = borders[5];
            borders[5] = temp;

            temp = borders[6];
            borders[6] = borders[7];
            borders[7] = temp;
        }

        
        public void RotateClockwise(int deg)
        {
            if (IsLocked)
                return;
            if (deg == 180)
            {
                FlipHorizontal();
                FlipVertical();
            }
            else if(deg == 90)
            {
                Transpose();
                FlipHorizontal();
            }
            else if(deg == 270)
            {
                Transpose();
                FlipVertical();
            }
        }

        void Transpose()
        {
            string temp = borders[0];
            borders[0] = borders[4];
            borders[4] = temp;

            temp = borders[1];
            borders[1] = borders[5];
            borders[5] = temp;

            temp = borders[2];
            borders[2] = borders[6];
            borders[6] = temp;

            temp = borders[3];
            borders[3] = borders[7];
            borders[7] = temp;

            List<string> transposedImage = new List<string>();
            List<StringBuilder> builder = new List<StringBuilder>();
            for(int a = 0; a < image[0].Length; a++)
            {
                builder.Add(new StringBuilder());
            }
            foreach(string s in image) 
            { 
                for(int j = 0; j < image[0].Length; j++)
                {
                    builder[j].Append(s[j]);
                }
            }
            foreach(StringBuilder sb in builder)
            {
                transposedImage.Add(sb.ToString());
            }
            image = transposedImage;
        }

        public string[] Image()
        {
            return image.ToArray();
        }
    }
}
