using System.Windows;
using NotifyPropertyChanged.MVVM;

namespace NotifyPropertyChanged
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new NameViewModel();
        }
    }
}
