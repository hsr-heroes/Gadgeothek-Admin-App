using ch.hsr.wpf.gadgeothek.domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ch.hsr.wpf.gadgeothek.service;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;

namespace ch.hsr.wpf.gadgeothek.app
{
    public class MainViewModel
    {
        public static ObservableCollection<Gadget> Gadgets { get; set; }
        public static ObservableCollection<Loan> Loans { get; set; }
        public static ObservableCollection<Reservation> Reservations { get; set; }
        public ICommand DeleteCommand { get; set; } = new RelayCommand<Gadget>((x) => Delete(x), (x) => true);
        public ICommand AddCommand { get; set; } = new RelayCommand<Gadget>((x) => AddEdit(new Gadget()), (x) => true);
        public ICommand EditCommand { get; set; } = new RelayCommand<Gadget>((x) => AddEdit(x), (x) => true);
        public static LibraryAdminService Service;

        private static string _serverAddress;
        public static string ServerAddress
        {
            get { return _serverAddress; }
            set
            {
                Service = new LibraryAdminService(value);
                _serverAddress = value;
                Task.Run(() =>
                {
                    Console.WriteLine("------------------Update Gadgets");
                    List<Gadget> list = Service.GetAllGadgets();
                    Application.Current.Dispatcher.BeginInvoke(
                        DispatcherPriority.Background,
                        new Action(() => {
                            Gadgets.Clear();
                            list.ForEach(Gadgets.Add);
                        }));
                });
            }
        }

        public MainViewModel(String server)
        {
            ServerAddress = server;
            Gadgets = new ObservableCollection<Gadget>(Service.GetAllGadgets());
            Loans = new ObservableCollection<Loan>(Service.GetAllLoans());
            Reservations = new ObservableCollection<Reservation>(Service.GetAllReservations());
            Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine("------------------Update Loans");
                    Thread.Sleep(3000);
                    List<Loan> list = Service.GetAllLoans();
                    Application.Current.Dispatcher.BeginInvoke(
                        DispatcherPriority.Background,
                        new Action(() => {
                            Loans.Clear();
                            list.ForEach(Loans.Add);
                        }));
                }
            });
            Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine("------------------Update Reservations");
                    Thread.Sleep(3000);
                    List<Reservation> list = Service.GetAllReservations();
                    Application.Current.Dispatcher.BeginInvoke(
                        DispatcherPriority.Background,
                        new Action(() => {
                            Reservations.Clear();
                            list.ForEach(Reservations.Add);
                        }));
                }
            });
        }

        public static void Delete(Gadget gadget)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(String.Format("Are you sure you want delete this gadget:\n{0} ({1})", gadget.Name, gadget.Manufacturer), "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (Service.DeleteGadget(gadget))
                {
                    Gadgets.Remove(gadget);
                }
                else
                {
                    messageBoxResult = MessageBox.Show("Could not delete selected gadget.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public static void AddEdit(Gadget gadget)
        {
            var win = new AddEditGadget(gadget);
            win.ShowDialog();
        }
    }
}