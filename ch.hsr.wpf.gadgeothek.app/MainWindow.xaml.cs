using System;
using System.Collections.Generic;
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
using MahApps.Metro.Controls;
using ch.hsr.wpf.gadgeothek.domain;
using System.Collections.ObjectModel;
using ch.hsr.wpf.gadgeothek.service;

namespace ch.hsr.wpf.gadgeothek.app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public GadgetViewModel GadgetViewModel { get; set; }
        public LoanViewModel LoanViewModel { get; set; }

        public static LibraryAdminService Service;

        public MainWindow()
        {
            InitializeComponent();

            Service = new LibraryAdminService("http://mge5.dev.ifs.hsr.ch/");
            GadgetViewModel = new GadgetViewModel();
            LoanViewModel = new LoanViewModel();
            DataContext = this;
        }

        private void bAddGadget_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddEditGadget(new Gadget());
            win.ShowDialog();
        }

        private void bEditGadget_Click(object sender, RoutedEventArgs e)
        {
            Gadget gadget = (Gadget)GadgetGrid.SelectedItem;
            var win = new AddEditGadget((Gadget)GadgetGrid.SelectedItem);
            win.ShowDialog();
        }

        private void bDeleteGadget_Click(object sender, RoutedEventArgs e)
        {
            Gadget gadget = (Gadget)GadgetGrid.SelectedItem;
            MessageBoxResult messageBoxResult = MessageBox.Show(String.Format("Are you sure you want delete this gadget:\n{0} ({1})", gadget.Name, gadget.Manufacturer), "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (Service.DeleteGadget(gadget))
                {
                    GadgetViewModel.Gadgets.Remove(gadget);
                } else
                {
                    messageBoxResult = MessageBox.Show("Could not delete selected gadget.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
