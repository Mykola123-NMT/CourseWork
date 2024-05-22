using System;
using System.Collections.Generic;

namespace CourseWork.Server.Models;

public partial class AmazonCategory
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<AmazonProduct> AmazonProducts { get; set; } = new List<AmazonProduct>();
}
