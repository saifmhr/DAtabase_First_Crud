using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbFirstCrud.Models;

public partial class Product
{
    [Key]
    public int Code { get; set; }

    [StringLength(75)]
    public string? Name { get; set; }

    [StringLength(75)]
    public string? Description { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? Category { get; set; }

    [Column(TypeName = "money")]
    public decimal Cost { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [StringLength(500)]
    public string? ImageUrl { get; set; }

    [ForeignKey("Category")]
    [InverseProperty("Products")]
    public virtual Category? CategoryNavigation { get; set; }
}
