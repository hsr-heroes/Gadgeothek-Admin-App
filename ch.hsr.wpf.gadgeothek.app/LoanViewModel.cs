using ch.hsr.wpf.gadgeothek.domain;
using System;

namespace ch.hsr.wpf.gadgeothek.app
{
    public class LoanViewModel
    {
        public LoanViewModel()
        {
            _loan = new Loan("1", new Gadget("iPhone 7"), new Customer("Markus Akermann", "123456", "mail@hsr.ch", "10001"), DateTime.Now, DateTime.Now);
        }
        Loan _loan;


        public string Id
        {
            get { return _loan.Id; }
        }

        public string Customer {
            get { return _loan.Customer.Name; }
        }

        public string Gadget
        {
            get { return _loan.Gadget.Name; }
        }

        public DateTime? PickupDate {
            get { return _loan.PickupDate; }
        }
        public DateTime? ReturnDate
        {
            get { return _loan.ReturnDate; }
        }
    }
}