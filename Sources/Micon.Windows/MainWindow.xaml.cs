using Autofac;
using Micon.Portable.Generation;
using Micon.Windows.Bitmaps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Micon.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = App.Container.Resolve<Portable.HomeViewModel>();

            vm.ExportCommand.Subscribe((folder) =>
            {
                Process.Start(folder);
            });

            this.DataContext = vm;
            
            this.SizeChanged += MainWindow_SizeChanged;
            
            this.UpdatePanel(this.RenderSize);
        }
        const int min = 1375;

        private Portable.HomeViewModel ViewModel { get { return this.DataContext as Portable.HomeViewModel; } }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(e.PreviousSize.Width <=  min && e.NewSize.Width > min || e.PreviousSize.Width >= min && e.NewSize.Width < min)
            {
                UpdatePanel(e.NewSize);
            }
        }

        private void UpdatePanel(Size size)
        {
            if (size.Width > min)
            {
                this.widePanel.Visibility = Visibility.Visible;
                this.smallPanel.Visibility = Visibility.Hidden;
            }
            else
            {
                this.widePanel.Visibility = Visibility.Hidden;
                this.smallPanel.Visibility = Visibility.Visible;
            }
        }

        private void OnExportClick(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if(result == System.Windows.Forms.DialogResult.OK)
            {
                var vm = this.DataContext as Portable.HomeViewModel;
                if(vm.ExportCommand.CanExecute(dialog.SelectedPath))
                {
                    vm.ExportCommand.Execute(dialog.SelectedPath);
                }
            }
        }

        private void menuOpen_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".micon";
            dlg.Filter = "Micon|*.micon";

            var result = dlg.ShowDialog();

            var cmd = this.ViewModel.OpenCommand;
            if (result == true && cmd.CanExecute(dlg.FileName))
            {
                cmd.Execute(dlg.FileName);
            }
        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".micon";
            dlg.Filter = "Micon|*.micon";

            var result = dlg.ShowDialog();

            var cmd = this.ViewModel.SaveCommand;
            if (result == true && cmd.CanExecute(dlg.FileName))
            {
                cmd.Execute(dlg.FileName);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
    }
}
