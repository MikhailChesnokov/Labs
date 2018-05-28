namespace MainApp
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    using Interface;



    public partial class MainForm : Form, IMainApp
    {
        private readonly Dictionary<string, IPlugin> _plugins = new Dictionary<string, IPlugin>(); 



        public MainForm()
        {
            InitializeComponent();

            FindPlugins();
            CreatePluginsMenu();
        }



        public Bitmap Image
        {
            get => mainPictureBox.Image as Bitmap;
            set => mainPictureBox.Image = value;
        }



        void FindPlugins()
        {
            IEnumerable<Type> types = Directory
                .GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .SelectMany(x => Assembly
                             .LoadFile(x)
                             .GetTypes()
                             .Where(y => typeof(IPlugin).IsAssignableFrom(y))
                             .Where(y => y.IsClass)).ToList();
                types
                    .Where(x => x != null)
                    .ToList()
                    .ForEach(t => _plugins.Add(t.Name, Activator.CreateInstance(t) as IPlugin));
        }

        private void CreatePluginsMenu()
        {
            _plugins.Keys.ToList().ForEach(x => PluginsToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(x, null, OnPluginClick)));
        }

        private void OnPluginClick(object sender, EventArgs args)
        {
            _plugins[(sender as ToolStripMenuItem)?.Text ?? throw new InvalidOperationException("Plugin not found")].Transform(this);
        }
    }
}