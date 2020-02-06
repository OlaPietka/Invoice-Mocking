using System.Collections.Generic;

namespace LabInvoice
{
    public interface IEntity : IEqualityComparer<IEntity>
    {
        int Id { get; set; }
    }
}
