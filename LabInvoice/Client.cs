using System;
using System.Collections.Generic;

namespace LabInvoice
{
    public class Client
    {
        public int id;
        public string name;
        public IRepository<Invoice> myInvoices;

        public Client(IRepository<Invoice> repo)
        {
            myInvoices = repo;
        }

        public Invoice Buy(string customerName, List<InvoiceItem> list)
        {
            if (customerName.Length < 3 || list.Count == 0)
                throw new ArgumentException();
            else
            {
                Invoice iv = new Invoice()
                {
                    Id = myInvoices.GetNextID(),
                    Customer = customerName,
                    Items = list
                };
                myInvoices.Add(iv);
                return iv;
            }
        }

        public Boolean Return(Invoice iv)
        { 
            if (myInvoices.FindById(iv.Id) == null)
                return false;
            else
            {
                myInvoices.Delete(iv);
                return true;
            }
        }

        public Boolean Update(Invoice iv)
        {
            if (myInvoices.FindById(iv.Id) == null)
                return false;
            else
            {
                myInvoices.Update(iv);
                return true;
            }
        }

        public double InvoicePrice(Invoice iv)
        {
            if (myInvoices.FindById(iv.Id) != null)
                return myInvoices.InvoicePrice(iv);
            else
                throw new ArgumentNullException();
        }

        public void showTotalPrice()
        {
                myInvoices.TotalPrice();
                System.Console.Write(myInvoices.totalPrice);
        }

        public void ChangeName(int id, string customerName)
        {
            if (myInvoices.FindById(id) != null)
            {
                myInvoices.name = customerName;

                myInvoices.ChangeName(id);
            }
        }
    }
}
