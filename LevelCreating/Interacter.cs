using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelCreating
{
    public class Interacter
    {
        private Controller controller;
        private int blockNumberChoice = -1;
        private bool mouseDown = false; 
        private Size dragSize = SystemInformation.DragSize;
        private Rectangle dragBounds = Rectangle.Empty;
        private bool canDrag = false;
        private bool needsToDrag = false;
        private int startX = 0;
        private int startY = 0;
        public Interacter(ref Controller controller) 
        {
            this.controller = controller;
            controller.RenderPanel.MouseDown += RenderPanel_MouseDown;
            controller.RenderPanel.MouseUp += RenderPanel_MouseUp;
            controller.RenderPanel.MouseMove += RenderPanel_DragDrop;
        }

        private void RenderPanel_MouseUp(object sender, MouseEventArgs e)
        {
            canDrag = false;
        }

        private void RenderPanel_DragDrop(object sender, MouseEventArgs e)
        {
            if (canDrag)
            {
                controller.Renderer.SetOffset(controller.Renderer.XOffset + (int)((e.X - startX) * 0.1), controller.Renderer.YOffset + (int)((e.Y - startY) * 0.1));
                controller.Renderer.Regenerate();
                controller.Renderer.Repaint();
            }
        }

        private void RenderPanel_MouseDown(object sender, MouseEventArgs e)
        {
            startX = e.X;
            startY = e.Y;
            canDrag = true;
        }

        public void ManageKeyDown(Keys key) 
        {
            if (key == Keys.E)
            {
                if (controller.RenderMode == Renderer.RenderMode.Level)
                {
                    controller.SetRenderMode(Renderer.RenderMode.Inventory);
                }
                else
                {
                    controller.SetRenderMode(Renderer.RenderMode.Level);
                }
            }
            if (key == Keys.Left)
            {
                ManageArrowKeyLeft();
            }
            if (key == Keys.Right)
            {
                ManageArrowKeyRight();
            }
            if (key == Keys.Up)
            {
                ManageArrowKeyUp();
            }
            if (key == Keys.Down)
            {
                ManageArrowKeyDown();
            }
            controller.Renderer.Regenerate();
        }

        public void ManageMouseDown(MouseButtons button, Point p) 
        {
            int mousePositionX = p.X;
            int mousePositionY = p.Y;

            if (mousePositionX >= 0 && mousePositionY >= 0)
            {
                if (button == MouseButtons.Left)
                {
                    if (controller.RenderMode == Renderer.RenderMode.Level)
                    {
                        PlaceBlocks(mousePositionX, mousePositionY);
                    }
                    else
                    {
                        ChooseBlock(mousePositionX, mousePositionY);
                    }
                }

                if (button == MouseButtons.Right)
                {
                    RemoveBlocks(mousePositionX, mousePositionY);
                }
            }
            controller.Renderer.Regenerate();
        }

        public void ManageArrowKeyLeft()
        {
            int xOffset = controller.Renderer.XOffset;
            if (xOffset < 0)
            {
                xOffset += controller.BlockWidth;
                controller.Renderer.SetOffsetX(xOffset);
            }
        }
        public void ManageArrowKeyUp()
        {
            int yOffset = controller.Renderer.YOffset;
            if (yOffset < 0)
            {
                yOffset += controller.BlockHeight;
                controller.Renderer.SetOffsetY(yOffset);
            }
        }
        public void ManageArrowKeyRight()
        {
            int xOffset = controller.Renderer.XOffset;
            if (controller.LevelGrid.LevelWidth + (xOffset / controller.BlockWidth) > 20)
            {
                xOffset -= controller.BlockWidth;
                controller.Renderer.SetOffsetX(xOffset);
            }
        }
        public void ManageArrowKeyDown()
        {
            int yOffset = controller.Renderer.YOffset;
            if (controller.LevelGrid.LevelHeight + (yOffset / controller.BlockHeight) > 20)
            {
                yOffset -= controller.BlockHeight;
                controller.Renderer.SetOffsetY(yOffset);
            }
        }

        private void ChooseBlock(int mousePositionX, int mousePositionY)
        {
            int allWidth = 0;
            int allHeight = 0;
            blockNumberChoice = 0;
            for (int compteur = 0; compteur < controller.IntToImageDictionary.Count; compteur++)
            {
                if (allWidth >= controller.WindowWidth)
                {
                    allHeight += controller.BlockHeight;
                    allWidth = 0;
                }

                if (mousePositionX - controller.RenderPanel.Left < allWidth + controller.BlockWidth 
                    && mousePositionX - controller.RenderPanel.Left >= allWidth 
                    && mousePositionY - controller.RenderPanel.Top < allHeight + controller.BlockHeight 
                    && mousePositionY - controller.RenderPanel.Top >= allHeight)
                {
                    blockNumberChoice = compteur;
                }
                allWidth += controller.BlockWidth;
            }
        }

        private void PlaceBlocks(int mousePositionX, int mousePositionY)
        {
            if (controller.RenderMode == Renderer.RenderMode.Level)
            {
                ChangeBlockNumberChoice(mousePositionX, mousePositionY, blockNumberChoice + 1);
            }
            controller.Renderer.Regenerate();
            controller.Renderer.Repaint();
        }
        private void RemoveBlocks(int mousePositionX, int mousePositionY)
        {
            if (controller.RenderMode == Renderer.RenderMode.Level)
            {
                ChangeBlockNumberChoice(mousePositionX, mousePositionY, 0);
                controller.Renderer.Regenerate();
                controller.Renderer.Repaint();
            }

        }

        private void ChangeBlockNumberChoice(int mousePositionX, int mousePositionY, int value) 
        {
            for (int x = 0; x < controller.LevelGrid.IntLevelGrid.GetLength(0); x++)
            {
                for (int y = 0; y < controller.LevelGrid.IntLevelGrid.GetLength(1); y++)
                {
                    if (y * controller.BlockHeight + controller.Renderer.YOffset <= mousePositionY - controller.RenderPanel.Top
                        && y * controller.BlockHeight + controller.Renderer.YOffset + controller.BlockHeight > mousePositionY - controller.RenderPanel.Top
                        && x * controller.BlockWidth + controller.Renderer.XOffset <= mousePositionX - controller.RenderPanel.Left
                        && x * controller.BlockWidth + controller.Renderer.XOffset + controller.BlockWidth > mousePositionX - controller.RenderPanel.Left)
                    {
                        controller.LevelGrid.SetIntValueAt(x, y, value);
                    }
                }
            }
        }
    }
}
