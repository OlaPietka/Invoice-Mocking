using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabInvoice
{
    class ServiceRepository : IService<Client>
    {
        private List<Client> dataSource = new List<Client>();
        public IEnumerable<Client> List => dataSource;

        public void AddClient(Client c)
        {
            if (FindById(c.id) == null)
                dataSource.Add(c);
        }

        public Client FindById(int id)
        {
            return dataSource.Find(x => x.id == id);
        }

        public void DeleteClient(Client c)
        {
            if (FindById(c.id) != null)
            {
                dataSource.Remove(c);
            }
        }

        public int GetNextID()
        {
            return dataSource[dataSource.Count - 1].id + 1;
        }
    }
}
