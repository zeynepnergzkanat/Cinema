using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Domain.Identity;
using Cinema.Domain.Common;
using Cinema.Domain.Enums;

namespace Cinema.Domain.Entities;

public class Bilet : EntityBase
{
    public Guid SeansId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public BiletTipi BiletTipi { get; set; }
    public string KoltukNumaralari { get; set; } = string.Empty;

    public Seans? Seans { get; set; }
    public AppUser? User { get; set; }
}