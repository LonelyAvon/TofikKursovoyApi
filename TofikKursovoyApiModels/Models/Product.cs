using System;
using System.Collections.Generic;

namespace TofikKursovoyApiModels;

public partial class Product
{
    public int Id { get; set; }

    public int? Brandname { get; set; }

    public int? Typeoftechnic { get; set; }

    public string? Name { get; set; }

    public int? Cost { get; set; }

    public string? Description { get; set; }

    public string? Photoname { get; set; }

    public bool? Existance { get; set; }

    public string? ShortDescription { get; set; }

    public virtual Brand? BrandnameNavigation { get; set; }

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<Productorder> Productorders { get; set; } = new List<Productorder>();

    public virtual Typeoftechnic? TypeoftechnicNavigation { get; set; }
}
