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
        public AddEditViewModel AddEditViewModel { get; set; }

        public AddEditGadget(Gadget gadget)
        {
            InitializeComponent();
            AddEditViewModel = new AddEditViewModel(gadget);
            DataContext = AddEditViewModel;
        }
    }
}
