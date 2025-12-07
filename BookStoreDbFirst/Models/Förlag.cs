using System;
using System.Collections.Generic;

namespace BookStoreDbFirst.Models;

public partial class Förlag
{
    public int FörlagId { get; set; }

    public string Namn { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public string Stad { get; set; } = null!;

    public string Postnummer { get; set; } = null!;
}
