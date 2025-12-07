using System;
using System.Collections.Generic;

namespace BookStoreDbFirst.Models;

public partial class Kunder
{
    public int KundId { get; set; }

    public string Förnamn { get; set; } = null!;

    public string Efternamn { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Ordrar> Ordrars { get; set; } = new List<Ordrar>();
}
