using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;

namespace FirstProject
{
    public interface IRequestService
    {
         Task<BaseResponse<RequestDto>> CreateAsync (CreateRequestModel model , int id);
        Task<BaseResponse<RequestDto>> UpdateAsync (UpdateRequestModel model , int id);
        Task<BaseResponse<RequestDto>> DetailsAsync (int id);
       Task<BaseResponse<RequestDto>> ApprovedFarmProductByCompany(int companyId , int RequestId , List<FarmProduct> farmProducts);
        Task<BaseResponse<IEnumerable<RequestDto>>> GetAllFulfilledRequestAsync ();
        Task<BaseResponse<IEnumerable<RequestDto>>> GetAllPendingRequestAsync ();
       Task<BaseResponse<IEnumerable<RequestDto>>> GetAllMergedRequestAsync ();
       Task<BaseResponse<IEnumerable<RequestDto>>> GetAllMergedRequestByCompanyAsync (int id);
        Task<BaseResponse<IEnumerable<RequestDto>>> GetAllPendingRequestByCompanyAsync (int id);
        Task<BaseResponse<IEnumerable<RequestDto>>> GetAllFulfilledRequestByCompanyAsync (int id);
       
    }
}