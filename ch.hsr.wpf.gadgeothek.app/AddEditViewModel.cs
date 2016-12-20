using ch.hsr.wpf.gadgeothek.domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ch.hsr.wpf.gadgeothek.service;
using System.Linq;

namespace ch.hsr.wpf.gadgeothek.app
{
    public class AddEditViewModel
    {
        public ICommand SaveCommand { get; set; } = new RelayCommand<Window>((x) => Save(x), (x) => CanSave(Gadget));
        public static Gadget OriginalGadget { get; set; }
        public static Gadget Gadget { get; set; }
        private static bool IsNew { get; set; }
        private static int MaxID { get; set; }

        public AddEditViewModel(Gadget gadget)
        {
            OriginalGadget = gadget;
            Gadget = (Gadget)OriginalGadget.Clone();
            IsNew = (Gadget.InventoryNumber == null);
        }


        public static bool CanSave(Gadget gadget) => 
            gadget.Manufacturer != null &&
            gadget.Name != null &&
            gadget.Price >= 0;
        public static void Save(Window win)
        {
            if (IsNew)
            {
                Gadget.InventoryNumber = GetNewGadgetID();
                if (MainViewModel.Service.AddGadget(Gadget))
                {
                    MainViewModel.Gadgets.Add(Gadget);
                }
                else
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Could not add this gadget.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if (MainViewModel.Service.UpdateGadget(Gadget))
                {
                    OriginalGadget.Name = Gadget.Name;
                    OriginalGadget.Manufacturer = Gadget.Manufacturer;
                    OriginalGadget.Price = Gadget.Price;
                    OriginalGadget.Condition = Gadget.Condition;
                }
                else
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Could not update selected gadget.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            win.Close();
        }

        private static string GetNewGadgetID()
        {
            return (MainViewModel.Service.GetAllGadgets().Max(x => x.InventoryNumber != null ? Int32.Parse(x.InventoryNumber) : 0) + 1).ToString();
        }
    }
}