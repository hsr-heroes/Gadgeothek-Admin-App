using ch.hsr.wpf.gadgeothek.domain;
using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace ch.hsr.wpf.gadgeothek.app
{
    /// <summary>
    /// Interaction logic for AddEditGadget.xaml
    /// </summary>
    public partial class AddEditGadget : MetroWindow
    {
        public Gadget OriginalGadget { get; set; }
        public Gadget Gadget { get; set; }
        private bool IsNew { get; set; }
        private int MaxID { get; set; }

        public AddEditGadget(Gadget gadget)
        {
            InitializeComponent();
            OriginalGadget = gadget;
            Gadget = (Gadget) OriginalGadget.Clone();
            IsNew = (Gadget.InventoryNumber == null);
            DataContext = this;
        }

        private void bSaveGadget_Click(object sender, RoutedEventArgs e)
        {
            if (IsNew)
            {
                Gadget.InventoryNumber = GetNewGadgetID();
                if (MainWindow.Service.AddGadget(Gadget))
                {
                    GadgetViewModel.Gadgets.Add(Gadget);
                } else
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Could not add this gadget.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } else
            {
                if(MainWindow.Service.UpdateGadget(Gadget))
                {
                    OriginalGadget.Name = Gadget.Name;
                    OriginalGadget.Manufacturer = Gadget.Manufacturer;
                    OriginalGadget.Price = Gadget.Price;
                    OriginalGadget.Condition = Gadget.Condition;
                } else
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Could not update selected gadget.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            DialogResult = true;
        }

        private string GetNewGadgetID()
        {
            return (GadgetViewModel.Gadgets.Max(x => x.InventoryNumber != null ? Int32.Parse(x.InventoryNumber) : 0) + 1).ToString();
        }

    }
}
