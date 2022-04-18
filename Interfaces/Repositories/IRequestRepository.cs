using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;

namespace FirstProject.Interfaces.Repositories
{
    public interface IRequestRepository
    {
        Task<RequestDto> CreateAsync (Request request);
        Task<RequestDto> UpdateAsync (Request request);
        Task<RequestDto> DetailsAsync (int id);
        Task<Request> GetByIdAsync (int id);
       
        Task<IEnumerable<RequestDto>> GetAllFulfilledRequestAsync ();
        Task<IEnumerable<RequestDto>> GetAllPendingRequestAsync ();
        Task<IList<Request>> GetAllPendingRequestReturningObjectAsync ();
        Task<IEnumerable<RequestDto>> GetAllMergedRequestAsync ();
        Task<IEnumerable<RequestDto>> GetAllMergedRequestByCompanyAsync (int id);
        Task<IEnumerable<RequestDto>> GetAllPendingRequestByCompanyAsync (int id);
        Task<IEnumerable<RequestDto>> GetAllFulfilledRequestByCompanyAsync (int id);
        
    }
}