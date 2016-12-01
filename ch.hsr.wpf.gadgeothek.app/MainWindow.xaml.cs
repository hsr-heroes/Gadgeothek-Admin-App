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

namespace ch.hsr.wpf.gadgeothek.app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public ObservableCollection<GadgetViewModel> GadgetList { get; set; }
        public ObservableCollection<LoanViewModel> LoanList { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            List<GadgetViewModel> gadgets = new List<GadgetViewModel>();
            gadgets.Add(new GadgetViewModel());
            GadgetList = new ObservableCollection<GadgetViewModel>(gadgets);
            List<LoanViewModel> loans = new List<LoanViewModel>();
            loans.Add(new LoanViewModel());
            LoanList = new ObservableCollection<LoanViewModel>(loans);

            DataContext = this;
        }
    }
}
