using System;
using System.Collections.Generic;

namespace CourseWork.Server.Models;

public partial class AmazonProduct
{
    public string Asin { get; set; } = null!;

    public string? Title { get; set; }

    public string? ImgUrl { get; set; }

    public string? ProductUrl { get; set; }

    public decimal? Stars { get; set; }

    public int? Reviews { get; set; }

    public decimal? Price { get; set; }

    public decimal? ListPrice { get; set; }

    public int? CategoryId { get; set; }

    public bool? IsBestSeller { get; set; }

    public int? BoughtInLastMonth { get; set; }

    public virtual AmazonCategory? Category { get; set; }
}
