using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelCreating
{
    public partial class Form1 : Form
    {
        #region Variables
        static int[,] levelGrid;
        static Image[,] levelGridShow;
        static Image[] images;
        static int[] imageNumbers;
        string fileName = "save";
        int levelWidth = 20;
        int levelHeight = 20;
        int blockWidth = 20;
        int blockHeight = 20;
        int blockNumberChoice = 0;
        int xMovement = 0;
        int yMovement = 0;
        bool clearGraphics = false;
        bool running = false;
        bool windowDragged = false;
        bool showRectangle = false;
        bool programStart = false;
        bool sizeChange = false;
        bool removeImage = false;
        bool needsToRender = true;
        Timer timer = new Timer();
        int removeBlockX = 0;
        int removeBlockY = 0;
        #endregion Variables
        #region Loading
        public Form1()
        {
            InitializeComponent();
            timer.Tick += rendering;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            programStart = true;
            timer.Start();
            Width = 20 * 20 + 17;
            Height = 20 * 20 + 64;
            clearGraphics = true;
            levelGrid = new int[levelHeight, levelWidth];
        }
        #endregion Loading
        #region Rendering

        private void rendering(object sender, EventArgs e)
        {
            if (windowDragged == false)
            {
                if(needsToRender)
                {
                    needsToRender = false;
                    render();
                }
            }
        }

        Graphics g;
        Rectangle r;
        Pen p;
        Brush b;

        public void render()
        {

            g = CreateGraphics();
            g.Clear(BackColor);
            if (clearGraphics == true || programStart == true)
            {
                clearGraphics = false;
                programStart = false;
            }
            p = new Pen(Brushes.Black);
            if(showRectangle == false)
            {
                if(sizeChange || levelGridShow == null)
                {
                    levelGridShow = new Image[levelHeight, levelWidth];
                    sizeChange = false;
                }
                for (int y = 0; y < levelHeight; y++)
                {
                    for (int x = 0; x < levelWidth; x++)
                    {
                        r = new Rectangle(x * blockWidth + xMovement, 24 + y * blockHeight + yMovement, blockWidth, blockHeight);
                        g.DrawRectangle(p, r);
                        if(levelGrid[y, x] != 0)
                        {
                            Image imageToDraw = levelGridShow[y, x];
                            if(imageToDraw != null)
                            {
                                g.DrawImage(imageToDraw, r);
                            }
                        } else if(levelGrid[y, x] == 0 && removeImage)
                        {
                            r = new Rectangle(removeBlockX * blockWidth + xMovement, 24 + removeBlockY * blockHeight + yMovement, blockWidth, blockHeight);
                            b = Brushes.BlanchedAlmond;
                            g.FillRectangle(b, r);
                            removeImage = false;
                        }
                    }
                }
            }
            if (showRectangle)
            {
                b = Brushes.White;
                Rectangle r2 = new Rectangle(0, 24, Width, Height - 24);
                Rectangle r3;
                if (images != null && imageNumbers != null)
                {
                    Image imageToSee;

                    int totalWidth = 0;
                    int totalHeight = 0;
                    for (int compteur = 0; compteur < images.GetLength(0); compteur++)
                    {
                        imageToSee = images[compteur];
                        if(totalWidth + blockWidth >= Width)
                        {
                            totalHeight += blockHeight;
                            totalWidth = 0;
                        }
                        if(imageToSee != null)
                        {
                            b = Brushes.Black;
                            r3 = new Rectangle(totalWidth, 24 + totalHeight, blockWidth, blockHeight);
                            g.DrawImage(imageToSee, r3);
                            totalWidth += blockWidth;
                        }
                    }
                }
            }
        }
        #endregion Rendering
        #region Controls
        /* Controls */
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            if(key == Keys.E)
            {
                OpenBlockInventory();
                needsToRender = true;
            }
            if (key == Keys.Left)
            {
                ManageArrowKeyLeft();
                needsToRender = true;
            }
            if (key == Keys.Right)
            {
                ManageArrowKeyRight();
                needsToRender = true;
            }
            if (key == Keys.Up)
            {
                ManageArrowKeyUp();
                needsToRender = true;
            }
            if (key == Keys.Down)
            {
                ManageArrowKeyDown();
                needsToRender = true;
            }
        }

        public void ManageArrowKeyLeft()
        {
            if(xMovement < 0)
            {
                xMovement += blockWidth;
                clearGraphics = true;
            }
        }
        public void ManageArrowKeyUp()
        {
            if(yMovement < 0)
            {
                yMovement += blockHeight;
                clearGraphics = true;
            }
        }
        public void ManageArrowKeyRight()
        {
            if(levelWidth + (xMovement / blockWidth) > 20)
            {
                xMovement -= blockWidth;
                clearGraphics = true;
            }
        }
        public void ManageArrowKeyDown()
        {
            if(levelHeight + (yMovement / blockHeight) > 20)
            {
                yMovement -= blockHeight;
                clearGraphics = true;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseButtons button = e.Button;
            Point p = this.PointToClient(Cursor.Position);
            int mousePositionX = p.X;
            int mousePositionY = p.Y;
            if ( mousePositionX < 0 && mousePositionY < 0)
            {
                windowDragged = true;
            }

            if (mousePositionX >= 0 && mousePositionY >= 0)
            {
                if (button == MouseButtons.Left)
                {
                    if(showRectangle == false)
                    {
                        PlaceBlocks(mousePositionX, mousePositionY);
                    } else
                    {
                        ChooseBlock(mousePositionX, mousePositionY);
                    }
                }

                if (button == MouseButtons.Right)
                {
                    RemoveBlocks(mousePositionX, mousePositionY);
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            windowDragged = false;
        }
        /* Controls End */
        #endregion Controls
        #region Interactions
        private void SizeClick(object sender, EventArgs e)
        {
            LevelSize levelSize = new LevelSize();
            levelSize.Transfer(levelWidth, levelHeight);
            levelSize.ShowDialog();
            levelSize.TransferBack(out levelWidth, out levelHeight);

            levelGrid = new int[levelHeight, levelWidth];
            sizeChange = true;
            clearGraphics = true;
            needsToRender = true;
        }

        private void BlockSizeClick(object sender, EventArgs e)
        {

            BlockLevelSize blockSize = new BlockLevelSize();
            blockSize.Transfer(blockWidth, blockHeight);
            blockSize.ShowDialog();
            blockSize.TransferBack(out blockWidth, out blockHeight);

           // Debug.WriteLine(blockWidth + "\n" + blockHeight);
            sizeChange = true;
            clearGraphics = true;
            needsToRender = true;
        }

        private void ChooseBlock(int mousePositionX, int mousePositionY)
        {
            int allWidth = -blockWidth;
            int allHeight = 0;
            blockNumberChoice = 0;
            for(int compteur = 0; compteur < images.Length; compteur++)
            {
                if(allWidth + blockWidth >= Width)
                {
                    allHeight += blockHeight;
                    allWidth = 0;
                }

                if(mousePositionX < allWidth + blockWidth && mousePositionX >= allWidth && mousePositionY - 24 < allHeight + blockHeight && mousePositionY - 24>= allHeight)
                {
                    blockNumberChoice = imageNumbers[compteur];
                }
                allWidth += blockWidth;
            }
          //  Debug.WriteLine("Block chosen : " + blockNumberChoice);
        }

        private void PlaceBlocks(int mousePositionX, int mousePositionY)
        {
            if(showRectangle == false)
            {
               // Debug.WriteLine("Block Placed :");
                for (int y = 0; y < levelGrid.GetLength(0); y++)
                {
                    for (int x = 0; x < levelGrid.GetLength(1); x++)
                    {
                        if (y * blockHeight + yMovement <= mousePositionY - 24 && y * blockHeight + yMovement + blockHeight > mousePositionY - 24 && x * blockWidth + xMovement <= mousePositionX && x * blockWidth + xMovement + blockWidth > mousePositionX)
                        {
                            levelGrid[y, x] = blockNumberChoice;
                        }
                      //  Debug.Write(levelGrid[y, x]);
                    }
                    //Debug.Write("\n");
                }

                TransferImagesToGrid();
            }
            needsToRender = true;

        }

        private void RemoveBlocks(int mousePositionX, int mousePositionY)
        {
            if(showRectangle == false)
            {
                Debug.WriteLine("Block Removed :");
                for (int y = 0; y < levelGrid.GetLength(0); y++)
                {
                    for (int x = 0; x < levelGrid.GetLength(1); x++)
                    {
                        if (y * blockHeight + yMovement <= mousePositionY - 24 && y * blockHeight + yMovement + blockHeight > mousePositionY - 24 && x * blockWidth + xMovement <= mousePositionX && x * blockWidth + xMovement + blockWidth > mousePositionX)
                        {
                            levelGrid[y, x] = 0;
                            removeBlockX = x;
                            removeBlockY = y;
                        }
                        Debug.Write(levelGrid[y, x]);
                    }
                    Debug.Write("\n");
                }


                TransferImagesToGrid();
                removeImage = true;
            }
            
        }
        
        #endregion Interactions
        #region InventoryMenu
        public void OpenBlockInventory()
        {
            showRectangle = !showRectangle;
            if(showRectangle == false)
            {
                clearGraphics = true;
            }
        }
        #endregion InventoryMenu

        private void ImportImage(object sender, EventArgs e)
        {
            OpenFileDialog imageFiles = new OpenFileDialog();
            imageFiles.Filter = "Image Files (.PNG, .JPG, .BMP)|*.JPG;*.PNG;*.BMP";
            imageFiles.ShowDialog();
            Image imageAdded;
            imageAdded = new Bitmap(imageFiles.FileName);
            AddImageToTables(imageAdded);
        }

        public void AddImageToTables(Image image)
        {
            int lastImageElementNumber = 0;
            Image[] temporaryImageTable;
            int[] temporaryIntTable;
            int previousLastNumber;
            int previousLastImage;
            if (imageNumbers != null)
            {
               lastImageElementNumber = imageNumbers[imageNumbers.Length - 1];
               temporaryIntTable = new int[imageNumbers.Length + 1];
               for (int compteur = 0; compteur < imageNumbers.Length; compteur++)
               {
                   temporaryIntTable[compteur] = imageNumbers[compteur];
               }
                previousLastNumber = imageNumbers.Length - 1;
            }
            else
            {
                temporaryIntTable = new int[2];
                previousLastNumber = 0;
            }
            if (images != null)
            {
                Image lastImageElement = images[images.Length - 1];
                temporaryImageTable = new Image[images.Length + 1];
                for (int compteur = 0; compteur < images.Length; compteur++)
                {
                    temporaryImageTable[compteur] = images[compteur];
                }
                previousLastImage = images.Length - 1;
            }
            else
            {
                temporaryImageTable = new Image[2];
                previousLastImage = 0;
            }
            for (int compteur = previousLastNumber; compteur < temporaryIntTable.Length - 1; compteur++)
            {
                temporaryIntTable[1 + compteur] = (lastImageElementNumber + 1);
            }
            for (int compteur = previousLastImage; compteur < temporaryImageTable.Length - 1; compteur++)
            {
                temporaryImageTable[1 + compteur] = image;
            }
            images = temporaryImageTable;
            imageNumbers = temporaryIntTable;
            needsToRender = true;
        }

        public void TransferImagesToGrid()
        {
            for(int y = 0; y < levelGridShow.GetLength(0); y++)
            {
                for(int x = 0; x < levelGridShow.GetLength(1); x++)
                {
                    if(levelGrid[y, x] != 0)
                    {
                        int imageNumber = levelGrid[y, x];
                        levelGridShow[y, x] = images[imageNumber];
                    } 
                }
            }
        }

        private void Save(object sender, EventArgs e)
        {
            SaveForm save = new SaveForm();
            save.ShowDialog();
            string seperator = save.GetSeperator();
            string lineseperator = save.GetLineSeperator();
            string alphabet = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            alphabet.ToUpper();
            string[] caracters = alphabet.Split(',');
            SaveFileDialog txtfolder = new SaveFileDialog();
            txtfolder.Filter = "Text file (*.txt)|*.txt|JSON file (*.json)|*.json"; ;
            txtfolder.ShowDialog();
            // Create a file to write to.

            if (txtfolder.FileName.Contains(".txt"))
            {

                using (StreamWriter sw = File.CreateText(
                txtfolder.FileName))
                {
                    for (int y = 0; y < levelGrid.GetLength(0); y++)
                    {
                        for (int x = 0; x < levelGrid.GetLength(1); x++)
                        {
                            if (levelGrid[y, x] < 10)
                            {
                                sw.Write(levelGrid[y, x]);
                            }
                            else
                            {
                                int number = levelGrid[y, x];
                                sw.Write(caracters[number - 10]);
                            }

                            if(x < levelGrid.GetLength(1) - 1)
                            {
                                sw.Write(seperator);
                            }
                        }
                        sw.Write(lineseperator);
                        sw.WriteLine();
                    }
                }

            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            needsToRender = true;
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            needsToRender = true;
        }

        private void Form1_ResizeEnd_1(object sender, EventArgs e)
        {
            needsToRender = true;
        }
    }
}
