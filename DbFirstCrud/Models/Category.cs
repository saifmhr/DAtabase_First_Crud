using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbFirstCrud.Models;

public partial class Category
{
    [Key]
    [StringLength(2)]
    [Unicode(false)]
    public string CtCode { get; set; } = null!;

    [StringLength(500)]
    public string CatName { get; set; } = null!;

    [InverseProperty("CategoryNavigation")]
    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
