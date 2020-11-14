using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelCreating
{
    public class LevelGrid
    {
        public int LevelWidth { get; private set; } = 20;
        public int LevelHeight { get; private set; } = 20;
        public int[,] IntLevelGrid { get; private set; } = new int[20, 20];

        public Image[,] GetImageLevelGrid(Dictionary<int, Image> intToImageDictionary) 
        {
            Image[,] imageLevelGrid = new Image[LevelWidth, LevelHeight];
            for(int i = 0; i < LevelWidth; ++i) 
            {
                for(int j = 0; j < LevelHeight; ++j) 
                {
                    if(IntLevelGrid[i,j] - 1 > -1) 
                    {
                        imageLevelGrid[i, j] = intToImageDictionary[IntLevelGrid[i, j] - 1];
                    }
                }
            }
            return imageLevelGrid;
        }

        public void SetIntLevelGrid(int[,] intLevelGrid) 
        {
            this.IntLevelGrid = intLevelGrid;
        }

        public void SetIntValueAt(int x, int y, int value) 
        {
            IntLevelGrid[x, y] = value;
        }

        public int GetIntValueAt(int x, int y) 
        {
            return IntLevelGrid[x, y];
        }

        public void SetWidth(int width) 
        {
            if (LevelWidth == width) return;
            if(width < LevelWidth) 
            {
                int[,] temp = new int[width, LevelHeight];
                CopyGridToOtherGrid(IntLevelGrid, temp);
                IntLevelGrid = temp;
            }
            else 
            {
                int[,] temp = new int[width, LevelHeight];
                temp.Populate(0);
                CopyGridToOtherGrid(IntLevelGrid, temp, LevelWidth);
            }

            LevelWidth = width;
        }

        public void SetHeight(int height) 
        {
            if (LevelHeight == height) return;
            if(height < LevelHeight) 
            {
                int[,] temp = new int[LevelWidth, height];
                CopyGridToOtherGrid(IntLevelGrid, temp);
                IntLevelGrid = temp;
            }
            else
            {
                int[,] temp = new int[LevelWidth, height];
                temp.Populate(0);
                CopyGridToOtherGrid(IntLevelGrid, temp, LevelHeight);
            }
            LevelHeight = height;
        }

        public void CopyGridToOtherGrid(int[,] firstGrid, int[,] secondGrid, int desiredWidth = 0, int desiredHeight = 0) 
        {
            int width = (desiredWidth != 0) ? desiredWidth : secondGrid.GetLength(0);
            int height = (desiredHeight != 0) ? desiredHeight : secondGrid.GetLength(1);
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    secondGrid[i, j] = firstGrid[i, j];
                }
            }
        }
    }
}
