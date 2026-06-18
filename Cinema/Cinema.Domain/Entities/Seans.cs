using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Domain.Common;

namespace Cinema.Domain.Entities;

public class Seans : EntityBase
{
    public Guid FilmId { get; set; }
    public Guid SalonId { get; set; }
    public DateTime StartTime { get; set; }
    public decimal Price { get; set; }

    public Film? Film { get; set; }
    public Salon? Salon { get; set; }
    public ICollection<Bilet> Biletler { get; set; } = [];
}