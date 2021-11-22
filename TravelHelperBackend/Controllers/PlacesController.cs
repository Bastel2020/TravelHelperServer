using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Interfaces;

namespace TravelHelperBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private IPlacesRepository _placesRepository;
        public PlacesController(IPlacesRepository repository)
        {
            _placesRepository = repository;
        }
    }
}
