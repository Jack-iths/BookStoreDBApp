using System;
using System.Collections.Generic;

namespace BookStoreDbFirst.Models;

public partial class Författare
{
    public int FörfattarId { get; set; }

    public string Förnamn { get; set; } = null!;

    public string Efternamn { get; set; } = null!;

    public DateOnly? Födelsedatum { get; set; }

    public virtual ICollection<Böcker> Böckers { get; set; } = new List<Böcker>();
}
