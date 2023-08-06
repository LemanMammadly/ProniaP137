using System;
using Microsoft.EntityFrameworkCore;
using P137Pronia.DataAccess;

namespace P137Pronia.Services
{
	public class LayoutService
	{
		readonly ProniaDBContext _context;

        public LayoutService(ProniaDBContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string,string>> GetSettings()
           => await _context.Settings.ToDictionaryAsync(s => s.Key, s => s.Value);
    }
}

