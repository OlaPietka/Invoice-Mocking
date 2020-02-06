using System.Collections.Generic;

namespace LabInvoice
{
    public class Invoice : IEntity
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public List<InvoiceItem> Items { get; set; } 

        public List<InvoiceItem> getItems()
        {
            return this.Items;
        }

        public bool Equals(IEntity x, IEntity y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(IEntity obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
