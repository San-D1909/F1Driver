using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Datalayer
{
    public class Class1
    {
        private readonly ApplicationDbContext _context;
        public Class1(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<string>> TestBusi()
        {
            var circuits =await _context.circuit.Select(b => b.Name).ToListAsync();
            return circuits;
        }
    }
}
