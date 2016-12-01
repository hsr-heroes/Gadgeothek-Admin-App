using ch.hsr.wpf.gadgeothek.domain;

namespace ch.hsr.wpf.gadgeothek.app
{
    public class GadgetViewModel
    {
        public GadgetViewModel()
        {
            _gadget = new Gadget("iPhone 7");
            _gadget.Manufacturer = "Apple";
            _gadget.Price = 999.99;
        }
        Gadget _gadget;

        public string InventoryNumber
        {
            get { return _gadget.InventoryNumber; }
        }

        public Condition Condition
        {
            get { return _gadget.Condition; }
        }

        public double Price
        {
            get { return _gadget.Price; }
        }

        public string Manufacturer
        {
            get { return _gadget.Manufacturer; }
        }

        public string Name
        {
            get { return _gadget.Name; }
        }
    }
}