using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.repositories.Type;
using Microsoft.AspNetCore.Mvc;

namespace flightdocs_system.Controllers.Type
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeController : ControllerBase
    {
        private readonly ITypeRepository _typeRepository;
        public TypeController(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }
    }
}