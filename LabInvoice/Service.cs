using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabInvoice
{
    public class Service
    {
        private IService<Client> clients;

        public Service(IService<Client> repo)
        {
            clients = repo;
        }

        public Client CreateClient(string customerName)
        {
            var client = new Client(null)
            {
                id = clients.GetNextID(),
                name = customerName
            };

            clients.AddClient(client);
            return client;
        }

        public void CreateInvoiceForClient(Client c, Invoice iv)
        {
            if (clients.FindById(c.id) != null)
                c.Buy(c.name, iv.Items);
        }

        public void DeleteClient(Client c)
        {
            if (clients.FindById(c.id) != null)
            {
                if (c.myInvoices.getAll() != null)
                {
                    foreach(Invoice iv in c.myInvoices.getAll())
                        c.Return(iv);
                }

                clients.DeleteClient(c);
            }
        }
    }
}
