using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.http.http_request.Type;
using flightdocs_system.staticObject.StaticResultResponse;

namespace flightdocs_system.repositories.Type
{
    public interface ITypeRepository
    {
        Task<ServiceResponse> CreateType(NewTypeRequestTemplate request);
        Task<ServiceResponse> UpdateType(NewTypeRequestTemplate request, int cd);
    }
}