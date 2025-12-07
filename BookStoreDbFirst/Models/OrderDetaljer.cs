using System;
using System.Collections.Generic;

namespace BookStoreDbFirst.Models;

public partial class OrderDetaljer
{
    public int OrderDetaljId { get; set; }

    public int? OrderId { get; set; }

    public string? Isbn13 { get; set; }

    public int Antal { get; set; }

    public decimal Pris { get; set; }

    public virtual Böcker? Bok { get; set; }

    public virtual Ordrar? Order { get; set; }
}
