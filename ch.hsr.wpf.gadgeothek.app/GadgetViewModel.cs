using ch.hsr.wpf.gadgeothek.domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ch.hsr.wpf.gadgeothek.app
{
    public class GadgetViewModel
    {
        public static ObservableCollection<Gadget> Gadgets { get; set; }
        public ICommand DeleteGadgetCommand { get; internal set; }

        public GadgetViewModel()
        {
            ReloadGadgetList();
        }

        public static void ReloadGadgetList()
        {
            List<Gadget> list = MainWindow.Service.GetAllGadgets();
            Gadgets = new ObservableCollection<Gadget>(list);
        }
    }
}