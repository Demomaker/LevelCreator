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
        private Controller controller;
        private Dictionary<Rectangle, Image> inventoryImages = new Dictionary<Rectangle, Image>();
        private List<Rectangle> inventoryRectangles = new List<Rectangle>();
        private Dictionary<Rectangle, Image> levelImages = new Dictionary<Rectangle, Image>();
        private List<Rectangle> levelRectangles = new List<Rectangle>();
        public int XOffset { get; private set; } = 0;
        public int YOffset { get; private set; } = 0;
        public Renderer(ref Controller controller) 
        {
            this.controller = controller;
            controller.RenderPanel.Paint += RenderPanelPaint;
            controller.RenderPanel.Resize += RenderPanelResize;
        }

        private void RenderPanelResize(object sender, EventArgs e)
        {
            controller.RenderPanel.Invalidate();
        }

        private void RenderPanelPaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Render(ref g);
        }

        public void Render(ref Graphics g)
        {
            g.Clear(controller.RenderPanel.BackColor);
            switch (controller.RenderMode)
            {
                case RenderMode.Inventory:
                    controller.Renderer.RenderInventory(ref g);
                    break;
                case RenderMode.Level:
                    controller.Renderer.RenderImageGrid(ref g);
                    break;
            }

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
        public void Regenerate() 
        {
            if(controller.RenderMode == RenderMode.Level) 
            {
                GenerateImageGrid(controller.LevelGrid, controller.IntToImageDictionary, controller.BlockWidth, controller.BlockHeight);
            }

            else 
            {
                GenerateInventory(controller.IntToImageDictionary, controller.RenderPanel.Width, controller.BlockWidth, controller.BlockHeight);
            }

        }

        public void Repaint() 
        {
            controller.RenderPanel.Invalidate();
        }

        public void GenerateImageGrid(LevelGrid levelGrid, Dictionary<int, Image> intToImageDictionary, int blockWidth = 20, int blockHeight = 20) 
        {
            Image[,] images = levelGrid.GetImageLevelGrid(intToImageDictionary);
            levelImages.Clear();
            levelRectangles.Clear();
            for (int i = 0; i < images.GetLength(0); ++i)
            {
                for (int j = 0; j < images.GetLength(1); ++j)
                {
                    Rectangle r = new Rectangle(i * blockWidth + XOffset, j * blockHeight + YOffset, blockWidth, blockHeight);
                    Pen p = new Pen(Color.Black);
                    levelRectangles.Add(r);
                    if (levelGrid.GetIntValueAt(i, j) != 0)
                    {
                        Image imageToDraw = images[i, j];
                        if (imageToDraw != null)
                        {
                            levelImages.Add(r, imageToDraw);
                        }
                    }
                }
            }
        }
        public void RenderImageGrid(ref Graphics g)
        {
            Pen rectanglePen = Pens.Black;
            foreach(Rectangle rectangle in levelRectangles) 
            {
                if (!levelImages.ContainsKey(rectangle)) 
                {
                    g.DrawRectangle(rectanglePen, rectangle);
                }
            }
            foreach(KeyValuePair<Rectangle, Image> image in levelImages) 
            {
                g.DrawImage(image.Value, image.Key);
            }
        }

        public void GenerateInventory(Dictionary<int, Image> intToImageDictionary, int maxWidth = 20, int blockWidth = 20, int blockHeight = 20) 
        {
            int totalWidth = 0;
            int totalHeight = 0;
            inventoryImages.Clear();
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
                    Rectangle r3 = new Rectangle(totalWidth, totalHeight, blockWidth, blockHeight);
                    inventoryImages.Add(r3, imageToSee);
                    totalWidth += blockWidth;
                }
            }
        }

        public void RenderInventory(ref Graphics g)
        {
            Pen rectanglePen = Pens.Black;
            foreach(Rectangle rectangle in inventoryRectangles) 
            {
                g.DrawRectangle(rectanglePen, rectangle);
            }
            foreach(KeyValuePair<Rectangle, Image> image in inventoryImages) 
            {
                g.DrawImage(image.Value, image.Key);
            }
        }

        public enum RenderMode
        {
            Inventory,
            Level
        }
    }
}
