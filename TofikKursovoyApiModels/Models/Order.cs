using System;
using System.Collections.Generic;

namespace TofikKursovoyApiModels;

public partial class Order
{
    public int Id { get; set; }

    public DateOnly? Dateorder { get; set; }

    public string? IdClient { get; set; }

    public virtual Client? IdClientNavigation { get; set; }

    public virtual ICollection<Productorder> Productorders { get; set; } = new List<Productorder>();
}
