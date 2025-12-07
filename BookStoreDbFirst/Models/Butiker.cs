using System;
using System.Collections.Generic;

namespace BookStoreDbFirst.Models;

public partial class Butiker
{
    public int ButikId { get; set; }

    public string Namn { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public string Stad { get; set; } = null!;

    public string Postnummer { get; set; } = null!;

    public virtual ICollection<LagerSaldo> LagerSaldos { get; set; } = new List<LagerSaldo>();
}
