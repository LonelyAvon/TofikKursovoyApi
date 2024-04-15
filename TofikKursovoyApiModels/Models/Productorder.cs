using System;
using System.Collections.Generic;

namespace TofikKursovoyApiModels;

public partial class Productorder
{
    public int Id { get; set; }

    public int? IdProduct { get; set; }

    public int? IdOrder { get; set; }

    public string? Description { get; set; }

    public virtual Order? IdOrderNavigation { get; set; }

    public virtual Product? IdProductNavigation { get; set; }
}
