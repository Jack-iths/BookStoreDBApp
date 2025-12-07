using System;
using System.Collections.Generic;

namespace BookStoreDbFirst.Models;

public partial class LagerSaldo
{
    public int LagerSaldoId { get; set; }

    public int? ButikId { get; set; }

    public string? Isbn13 { get; set; }

    public int Antal { get; set; }

    public virtual Butiker? Butik { get; set; }

    public virtual Böcker? Bok { get; set; }
}
