using ch.hsr.wpf.gadgeothek.domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ch.hsr.wpf.gadgeothek.app
{
    public class LoanViewModel
    {

        public ObservableCollection<Loan> Loans { get; set; }
        public LoanViewModel()
        {
            List<Loan> list = MainWindow.Service.GetAllLoans();
            Loans = new ObservableCollection<Loan>(list);
        }
        
    }
}