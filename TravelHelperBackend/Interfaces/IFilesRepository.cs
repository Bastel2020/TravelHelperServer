using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelHelperBackend.Interfaces
{
    public interface IFilesRepository
    {
        public Task<byte[]> GetPhoto(int photoId);
    }
}
