using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabInvoice
{
    public interface IService<T>
    {
        IEnumerable<T> List { get; }
        void AddClient(T c);
        Client FindById(int id);
        void DeleteClient(T c);
        int GetNextID();
    }
}
