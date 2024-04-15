using System;
using System.Collections.Generic;

namespace TofikKursovoyApiModels;

public partial class Client
{
    public string Id { get; set; } = null!;

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Patronymic { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
