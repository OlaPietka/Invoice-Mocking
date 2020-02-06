using System;
using System.Collections.Generic;

namespace LabInvoice
{
    class InvoiceRepository : IRepository<Invoice>
    {
        private List<Invoice> dataSource = new List<Invoice>();
        public IEnumerable<Invoice> List => dataSource;
        public double totalPrice {get; set; }
        public string name { get; set; }


        public List<Invoice> getAll()
        {
            return dataSource;
        }

        public double InvoicePrice(Invoice entity)
        {
            double price = 0;

            if (FindById(entity.Id) != null)
            {
                foreach (InvoiceItem items in entity.getItems())
                {
                    price += items.Price;
                }
            }

            return price;
        }

        public void TotalPrice()
        {
            double price = 0;

            foreach (Invoice iv in dataSource)
            {
                price += InvoicePrice(iv);
            }

            this.totalPrice = price;
        }

        public void ChangeName(int Id)
        {
            if(FindById(Id) != null)
                FindById(Id).Customer = name;
        }

        public Invoice FindById(int Id)
        {
            return dataSource.Find(x => x.Id == Id);
        }

        public void Add(Invoice entity)
        {
            if (FindById(entity.Id) == null)
                dataSource.Add(entity);
        }

        public void Delete(Invoice entity)
        {
            if(FindById(entity.Id) != null)
                dataSource.Remove(entity);
        }

        public void Update(Invoice entity)
        {
            if (FindById(entity.Id) != null)
                Delete(entity);
            else
                throw new ArgumentException();
            Add(entity);
        }

        public int GetNextID()
        {
            return dataSource[dataSource.Count - 1].Id + 1;
        }
    }
}
