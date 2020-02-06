using System.Collections.Generic;

namespace LabInvoice
{
    public interface IRepository<T> where T : IEntity {

        IEnumerable<T> List { get; }
        double totalPrice { get; set; }
        string name { get; set; }
        List<Invoice> getAll();
        double InvoicePrice(T entity);
        void TotalPrice();
        void ChangeName(int Id);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T FindById(int Id);
        int GetNextID();
    }

}
