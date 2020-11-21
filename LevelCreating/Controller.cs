using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelCreating
{
    public class Controller
    {
        private const int STARTING_LEVEL_WIDTH = 20;
        private const int STARTING_LEVEL_HEIGHT = 20;
        private static Controller controller = new Controller();

        public Dictionary<int, Image> IntToImageDictionary { get; private set; } = new Dictionary<int, Image>();
        public LevelGrid LevelGrid { get; private set; } = new LevelGrid();
        public Interacter Interacter { get; private set; } = null;
        public Renderer Renderer { get; private set; }
        public Renderer.RenderMode RenderMode { get; private set; } = Renderer.RenderMode.Level;
        public int WindowWidth { get; private set; } = STARTING_LEVEL_WIDTH;
        public int WindowHeight { get; private set; } = STARTING_LEVEL_HEIGHT;
        public Panel RenderPanel { get; private set; }

        public int BlockWidth { get; private set; } = 20;
        public int BlockHeight { get; private set; } = 20;
        public Controller()
        {
            LevelGrid.SetWidth(STARTING_LEVEL_WIDTH);
            LevelGrid.SetHeight(STARTING_LEVEL_HEIGHT);
        }

        public void SetInteracterController(ref Controller controller) 
        {
            Interacter = new Interacter(ref controller);
            Renderer = new Renderer(ref controller);
        }
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static public void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow(ref controller));
        }

        public void SetRenderMode(Renderer.RenderMode renderMode) 
        {
            RenderMode = renderMode;
            controller.Renderer.Repaint();
        }

        public void SetWindowWidth(int windowWidth) 
        {
            WindowWidth = windowWidth;
        }

        public void SetWindowHeight(int windowHeight) 
        {
            WindowHeight = windowHeight;
        }

        public void SetBlockWidth(int blockWidth) 
        {
            BlockWidth = blockWidth;
        }

        public void SetBlockHeight(int blockHeight) 
        {
            BlockHeight = blockHeight;
        }

        public void SetRenderPanel(ref Panel renderPanel) 
        {
            this.RenderPanel = renderPanel;
        }
        
    }
}
