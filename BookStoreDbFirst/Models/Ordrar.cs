using System;
using System.Collections.Generic;

namespace BookStoreDbFirst.Models;

public partial class Ordrar
{
    public int OrderId { get; set; }

    public int? KundId { get; set; }

    public DateTime OrderDatum { get; set; }

    public virtual Kunder? Kund { get; set; }

    public virtual ICollection<OrderDetaljer> OrderDetaljers { get; set; } = new List<OrderDetaljer>();
}
