using System;

namespace LabInvoice
{
    public class InvoiceItem : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public double Price { get; set; } 
        public double Tax { get; set; }

        public bool Equals(IEntity x, IEntity y)
        { 
            if (x.Id == y.Id) 
                return true;
            else
                throw new NotImplementedException();
        }

        public int GetHashCode(IEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
