using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Database;
using TravelHelperBackend.Interfaces;
using System.IO;

namespace TravelHelperBackend.Repositories
{
    public class DefaultFilesRepository : IFilesRepository
    {
        private DefaultDbContext _db;
        public DefaultFilesRepository(DefaultDbContext context)
        {
            _db = context;
        }
        public async Task<byte[]> GetPhoto(int photoId)
        {
            var fileModel = await _db.Files.FirstOrDefaultAsync(f => f.Id == photoId);
            if (fileModel == null)
                return null;
            try
            {
                var data = File.ReadAllBytes(Environment.CurrentDirectory + fileModel.Path);
                return data;
            }
            catch
            {
                return new byte[0];
            }
        }
    }
}
