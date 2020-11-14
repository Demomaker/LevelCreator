using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelCreating
{
    public class Renderer
    {
        private Graphics g;
        private bool needsToRender = false;
        private int startingXOffset = 0;
        private int startingYOffset = 0;
        public int XOffset { get; private set; } = 0;
        public int YOffset { get; private set; } = 0;
        public void SetGraphics(Graphics g) 
        {
            this.g = g;
        }
        public void SetOffset(int xOffset, int yOffset) 
        {
            this.XOffset = xOffset;
            this.YOffset = yOffset;
        }
        public void SetOffsetX(int xOffset) 
        {
            SetOffset(xOffset, this.YOffset);
        }

        public void SetOffsetY(int yOffset) 
        {
            SetOffset(this.XOffset, yOffset);
        }
        public void Repaint() 
        {
            needsToRender = true;
        }

        public bool NeedsToRender() 
        {
            return needsToRender;
        }
        public void RenderImageGrid(LevelGrid levelGrid, Dictionary<int, Image> intToImageDictionary, int blockWidth = 20, int blockHeight = 20)
        {
            needsToRender = false;
            Image[,] images = levelGrid.GetImageLevelGrid(intToImageDictionary);
            for(int i = 0; i < images.GetLength(0); ++i) 
            {
                for(int j = 0; j < images.GetLength(1); ++j)
                {
                    Rectangle r = new Rectangle(i * blockWidth + XOffset, startingYOffset + j * blockHeight + YOffset, blockWidth, blockHeight);
                    Pen p = new Pen(Color.Black);
                    if (levelGrid.GetIntValueAt(i, j) != 0)
                    {
                        g.DrawRectangle(p, r);
                        Image imageToDraw = images[i, j];
                        if (imageToDraw != null)
                        {
                            g.DrawImage(imageToDraw, r);
                        }
                    }
                    else if (levelGrid.GetIntValueAt(i, j) == 0)
                    {
                        g.DrawRectangle(p, r);
                    }
                }
            }
        }

        public void RenderInventory(Dictionary<int, Image> intToImageDictionary, int maxWidth = 20, int blockWidth = 20, int blockHeight = 20)
        {
            needsToRender = false;
            int totalWidth = 0;
            int totalHeight = startingYOffset;
            for (int i = 0; i < intToImageDictionary.Count; ++i) 
            {
                Image imageToSee = intToImageDictionary[i];
                if (totalWidth + blockWidth >= maxWidth)
                {
                    totalHeight += blockHeight;
                    totalWidth = 0;
                }
                if (imageToSee != null)
                {
                    Rectangle r3 = new Rectangle(totalWidth, startingYOffset + totalHeight, blockWidth, blockHeight);
                    g.DrawImage(imageToSee, r3);
                    totalWidth += blockWidth;
                }
            }
        }

        public enum RenderMode
        {
            Inventory,
            Level
        }
    }
}
