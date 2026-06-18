using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Domain.Entities;
using Cinema.Infrastructure.Common;
using Cinema.Infrastructure.Data;

namespace Cinema.Infrastructure.Repositories;

public class FilmRepository(AppDbContext context)
    : EFRepositoryBase<Film, AppDbContext>(context)
{
}
