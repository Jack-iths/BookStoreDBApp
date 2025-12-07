using System;
using System.Collections.Generic;

namespace BookStoreDbFirst.Models;

public partial class Böcker
{
    public string Isbn13 { get; set; } = null!;

    public string Titel { get; set; } = null!;

    public DateOnly? UtgivningsDatum { get; set; }

    public int? FörfattarId { get; set; }

    public virtual Författare? Författar { get; set; }

    public virtual ICollection<LagerSaldo> LagerSaldos { get; set; } = new List<LagerSaldo>();

    public virtual ICollection<OrderDetaljer> OrderDetaljers { get; set; } = new List<OrderDetaljer>();
}
