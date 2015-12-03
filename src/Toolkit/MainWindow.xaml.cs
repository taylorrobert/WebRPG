using System.Windows;

namespace Toolkit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //output.Text = JsonConvert.SerializeObject(SchemaService.GetMasterSchemaDevSample(), Formatting.Indented);
        }
    }
}
