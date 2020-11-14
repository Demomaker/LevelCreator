using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LevelCreating
{
    public partial class MainWindow : Form
    {
        Timer timer = new Timer();
        Controller controller;
        Graphics g = null;
        public MainWindow(ref Controller controller)
        {
            this.controller = controller;
            InitializeComponent();
            timer.Tick += Rendering;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Start();
            Width = controller.LevelGrid.LevelWidth * controller.BlockWidth + 17;
            Height = controller.LevelGrid.LevelHeight * controller.BlockHeight + 64;
            controller.SetRenderPanel(ref renderPanel);
            controller.SetWindowWidth(Width);
            controller.SetWindowHeight(Height);
            g = renderPanel.CreateGraphics();
            controller.Renderer.SetGraphics(g);
            controller.SetInteracterController(ref controller);
            controller.Renderer.Repaint();
        }

        private void Rendering(object sender, EventArgs e)
        {
            if(controller.Renderer.NeedsToRender())
            {
                render();
            }
        }

        public void render()
        {
            g.Clear(BackColor);
            switch (controller.RenderMode)
            {
                case Renderer.RenderMode.Inventory:
                    controller.Renderer.RenderInventory(controller.IntToImageDictionary, Width, controller.BlockWidth, controller.BlockHeight);
                    break;
                case Renderer.RenderMode.Level:
                    controller.Renderer.RenderImageGrid(controller.LevelGrid, controller.IntToImageDictionary, controller.BlockWidth, controller.BlockHeight);
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            controller.Interacter.ManageKeyDown(key);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseButtons button = e.Button;
            Point p = this.PointToClient(Cursor.Position);
            controller.Interacter.ManageMouseDown(button, p);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void SizeClick(object sender, EventArgs e)
        {
            LevelSize levelSize = new LevelSize();
            levelSize.Transfer(controller.LevelGrid.LevelWidth, controller.LevelGrid.LevelHeight);
            levelSize.ShowDialog();
            int levelWidth;
            int levelHeight;
            bool saveLevelSize;
            levelSize.TransferBack(out levelWidth, out levelHeight, out saveLevelSize);
            if(saveLevelSize)
            {
                controller.LevelGrid.SetWidth(levelWidth);
                controller.LevelGrid.SetHeight(levelHeight);
                controller.Renderer.Repaint();
            }
        }

        private void BlockSizeClick(object sender, EventArgs e)
        {
            BlockLevelSize blockSize = new BlockLevelSize();
            blockSize.Transfer(controller.BlockWidth, controller.BlockHeight);
            blockSize.ShowDialog();
            int blockWidth;
            int blockHeight;
            bool saveBlockLevelSize;
            blockSize.TransferBack(out blockWidth, out blockHeight, out saveBlockLevelSize);
            if(saveBlockLevelSize) 
            {
                controller.SetBlockWidth(blockWidth);
                controller.SetBlockHeight(blockHeight);
                controller.Renderer.Repaint();
            }
        }

        private void ImportImage(object sender, EventArgs e)
        {
            OpenFileDialog imageFiles = new OpenFileDialog();
            imageFiles.Filter = "Image Files (.PNG, .JPG, .BMP)|*.JPG;*.PNG;*.BMP";
            imageFiles.ShowDialog();
            try 
            {
                if(IsValidPath(imageFiles.FileName, true)) 
                {
                    Image imageAdded = new Bitmap(imageFiles.FileName);
                    if (imageAdded != null)
                    {
                        controller.IntToImageDictionary.Add(controller.IntToImageDictionary.Count, imageAdded);
                    }
                }
            }
            catch (Exception e2) 
            {
                Console.WriteLine(e2.Message);
            }
            controller.Renderer.Repaint();
        }

        private bool IsValidPath(string path, bool allowRelativePaths = false)
        {
            bool isValid = true;

            try
            {
                string fullPath = Path.GetFullPath(path);

                if (allowRelativePaths)
                {
                    isValid = Path.IsPathRooted(path);
                }
                else
                {
                    string root = Path.GetPathRoot(path);
                    isValid = string.IsNullOrEmpty(root.Trim(new char[] { '\\', '/' })) == false;
                }
            }
            catch (Exception ex)
            {
                isValid = false;
            }

            return isValid;
        }

        private void Save(object sender, EventArgs e)
        {
            SaveForm save = new SaveForm();
            save.ShowDialog();
            bool saveState = save.GetSaveState();
            if (!saveState) return;
            string seperator = save.GetSeperator();
            string lineseperator = save.GetLineSeperator();
            string alphabet = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            alphabet.ToUpper();
            string[] characters = alphabet.Split(',');
            SaveFileDialog txtfolder = new SaveFileDialog();
            txtfolder.Filter = "Text file (*.txt)|*.txt|JSON file (*.json)|*.json| CSharp file (.cs)|*.cs";
            txtfolder.ShowDialog();
            if (txtfolder.FileName.Contains(".txt"))
            {

                using (StreamWriter sw = File.CreateText(
                txtfolder.FileName))
                {
                    SaveTxt(sw, lineseperator, seperator, characters);
                }

            }
            if(txtfolder.FileName.Contains(".json")) 
            {
                using (StreamWriter sw = File.CreateText(
                    txtfolder.FileName))
                {
                    SaveJson(sw, lineseperator, characters);
                }
            }

            if (txtfolder.FileName.Contains(".cs")) 
            {
                using (StreamWriter sw = File.CreateText(
                    txtfolder.FileName)) 
                {
                    SaveCs(sw, lineseperator, characters);
                }
            }
        }

        private void SaveJson(StreamWriter sw, string lineseperator, string[] characters) 
        {
            sw.Write("{\n\"levelInCharacters\":[");
            for (int y = 0; y < controller.LevelGrid.IntLevelGrid.GetLength(0); y++)
            {
                for (int x = 0; x < controller.LevelGrid.IntLevelGrid.GetLength(1); x++)
                {
                    sw.Write("\"");
                    if (controller.LevelGrid.IntLevelGrid[y, x] < 10)
                    {
                        sw.Write(controller.LevelGrid.IntLevelGrid[y, x]);
                    }
                    else
                    {
                        int number = controller.LevelGrid.IntLevelGrid[y, x];
                        sw.Write(characters[number - 10]);
                    }
                    sw.Write("\"");

                    if (x + y < controller.LevelGrid.IntLevelGrid.Length - 1)
                    {
                        sw.Write(", ");
                    }
                }
                sw.Write("\"" + lineseperator + "\"");
                if (y < controller.LevelGrid.IntLevelGrid.GetLength(0) - 1)
                {
                    sw.Write(", ");
                }
            }
            sw.Write("]\n}");
        }
        private void SaveTxt(StreamWriter sw, string lineseperator, string seperator, string[] characters) 
        {
            for (int y = 0; y < controller.LevelGrid.IntLevelGrid.GetLength(0); y++)
            {
                for (int x = 0; x < controller.LevelGrid.IntLevelGrid.GetLength(1); x++)
                {
                    if (controller.LevelGrid.IntLevelGrid[y, x] < 10)
                    {
                        sw.Write(controller.LevelGrid.IntLevelGrid[y, x]);
                    }
                    else
                    {
                        int number = controller.LevelGrid.IntLevelGrid[y, x];
                        sw.Write(characters[number - 10]);
                    }

                    if (x < controller.LevelGrid.IntLevelGrid.GetLength(1) - 1)
                    {
                        sw.Write(seperator);
                    }
                }
                if (lineseperator == "\\n")
                {
                    sw.WriteLine();
                }
                else
                {
                    sw.Write(lineseperator);
                }
            }
        }

        private void SaveCs(StreamWriter sw, string lineseperator, string[] characters) 
        {
            sw.Write("public class Level\n{\n    private char[] level = new char[]{");
            for (int y = 0; y < controller.LevelGrid.IntLevelGrid.GetLength(0); y++)
            {
                for (int x = 0; x < controller.LevelGrid.IntLevelGrid.GetLength(1); x++)
                {
                    sw.Write("'");
                    if (controller.LevelGrid.IntLevelGrid[y, x] < 10)
                    {
                        sw.Write(controller.LevelGrid.IntLevelGrid[y, x]);
                    }
                    else
                    {
                        int number = controller.LevelGrid.IntLevelGrid[y, x];
                        sw.Write(characters[number - 10]);
                    }
                    sw.Write("'"); 
                    if (x + y < controller.LevelGrid.IntLevelGrid.Length - 1)
                    {
                        sw.Write(", ");
                    }
                }
                sw.Write("\'" + lineseperator + "\'");
                if (y < controller.LevelGrid.IntLevelGrid.GetLength(0) - 1)
                {
                    sw.Write(", ");
                }
            }
            sw.Write("};\n}");
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            controller.Renderer.Repaint();
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            controller.Renderer.Repaint();
        }

        private void Form1_ResizeEnd_1(object sender, EventArgs e)
        {
            controller.Renderer.Repaint();
        }

        private void inventoryMenuItem_Click(object sender, EventArgs e)
        {
            inventoryMenuItem.Checked = !inventoryMenuItem.Checked;
            if (inventoryMenuItem.Checked) 
            {
                controller.SetRenderMode(Renderer.RenderMode.Inventory);
            }
            else 
            {
                controller.SetRenderMode(Renderer.RenderMode.Level);
            }
            controller.Renderer.Repaint();
        }

        private void renderPanel_MouseClick(object sender, MouseEventArgs e)
        {
            MouseButtons button = e.Button;
            Point p = this.PointToClient(Cursor.Position);
            controller.Interacter.ManageMouseDown(button, p);
        }
    }
}
