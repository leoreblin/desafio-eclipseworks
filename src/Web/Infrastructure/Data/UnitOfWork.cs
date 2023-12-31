﻿using DesafioEclipseworks.WebAPI.Abstractions.Data;

namespace DesafioEclipseworks.WebAPI.Infrastructure.Data
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
