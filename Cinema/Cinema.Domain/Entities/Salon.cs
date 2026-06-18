using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Domain.Common;

namespace Cinema.Domain.Entities;

public class Salon : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string City { get; set; } = string.Empty;

    public ICollection<Seans> Seanslar { get; set; } = [];
}