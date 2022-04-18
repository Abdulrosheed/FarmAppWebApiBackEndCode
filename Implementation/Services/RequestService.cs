using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Interfaces.Repositories;

namespace FirstProject.Implementation.Services
{
    public class RequestService : IRequestService
    {
      private readonly ICompanyRepository _companyRepository;
      private readonly IFarmProduceRepository _farmProduceRepository;
      private readonly IRequestRepository _requestRepository;
      private readonly IOrderRepository _orderRepository;
      private readonly IFarmProductRepository _farmProductRepository;

        public RequestService(ICompanyRepository companyRepository, IFarmProduceRepository farmProduceRepository, IRequestRepository requestRepository, IOrderRepository orderRepository, IFarmProductRepository farmProductRepository)
        {
            _companyRepository = companyRepository;
            _farmProduceRepository = farmProduceRepository;
            _requestRepository = requestRepository;
            _orderRepository = orderRepository;
            _farmProductRepository = farmProductRepository;
        }

        public Task<BaseResponse<RequestDto>> ApprovedFarmProductByCompany(int companyId, int RequestId, List<FarmProduct> farmProducts)
        {
            throw new System.NotImplementedException();
        }

        // public async Task<BaseResponse<RequestDto>> ApprovedFarmProductByCompany(int companyId, int requestId, List<FarmProduct> farmProducts)
        // {
        //     var company = await _companyRepository.GetCompanyReturningCompanyObjectAsync(companyId);
        //     var request = await _requestRepository.GetByIdAsync(requestId);
        //     foreach(var )
        // }

        public async Task<BaseResponse<RequestDto>> CreateAsync(CreateRequestModel model , int id)
        {
            var company = await _companyRepository.GetCompanyReturningCompanyObjectAsync(id);
            var farmProduce = await _farmProduceRepository.GetFarmProduceReturningFarmProduceObjectAsync(model.FarmProduceId);
            if(company == null || farmProduce == null)
            {
                return new BaseResponse<RequestDto>
                {
                    Message = "This action is not sucessfull",
                    IsSucess = false
                };
            }
            var request = new Request
            {
                Company = company,
                CompanyId = company.Id,
                FarmProduce = farmProduce,
                FarmProduceId = farmProduce.Id,
                Status = RequestStatus.Pending,
                Quantity = model.Quantity,
                Grade = model.Grade,
                YearNeeded = model.YearNeeded,
                MonthNeeded = model.MonthNeeded,

                
            };
            var requestInfo = await _requestRepository.CreateAsync(request);
            return new BaseResponse<RequestDto>
            {
                Message = "Sucessfully created a request",
                IsSucess = true,
                Data = requestInfo
            };
        }

        public async Task<BaseResponse<RequestDto>> DetailsAsync(int id)
        {
            var request = await _requestRepository.DetailsAsync(id);
            if(request == null)
            {
                return new BaseResponse<RequestDto>
                {
                    Message = "No request found",
                    IsSucess = false
                };
            }
            return new BaseResponse<RequestDto>
            {
                Message = "Request sucessfully retrieved",
                IsSucess = true,
                Data= request
            };
        }

        public async Task<BaseResponse<IEnumerable<RequestDto>>> GetAllFulfilledRequestAsync()
        {
            var request = await _requestRepository.GetAllFulfilledRequestAsync();
            if(request == null)
            {
                return new BaseResponse<IEnumerable<RequestDto>>
                {
                    Message = "No request found",
                    IsSucess = false
                };
            }
            return new BaseResponse<IEnumerable<RequestDto>>
            {
                Message = "Requests sucessfully retrieved",
                IsSucess = true,
                Data= request
            };
        }

        public async Task<BaseResponse<IEnumerable<RequestDto>>> GetAllFulfilledRequestByCompanyAsync(int id)
        {
            var request = await _requestRepository.GetAllFulfilledRequestByCompanyAsync(id);
            if(request == null)
            {
                return new BaseResponse<IEnumerable<RequestDto>>
                {
                    Message = "No request found",
                    IsSucess = false
                };
            }
            return new BaseResponse<IEnumerable<RequestDto>>
            {
                Message = "Requests sucessfully retrieved",
                IsSucess = true,
                Data= request
            };
        }

        public async Task<BaseResponse<IEnumerable<RequestDto>>> GetAllMergedRequestAsync()
        {
             var request = await _requestRepository.GetAllMergedRequestAsync();
            if(request == null)
            {
                return new BaseResponse<IEnumerable<RequestDto>>
                {
                    Message = "No request found",
                    IsSucess = false
                };
            }
            return new BaseResponse<IEnumerable<RequestDto>>
            {
                Message = "Requests sucessfully retrieved",
                IsSucess = true,
                Data= request
            };
        }

        public async Task<BaseResponse<IEnumerable<RequestDto>>> GetAllMergedRequestByCompanyAsync(int id)
        {
             var request = await _requestRepository.GetAllMergedRequestByCompanyAsync(id);
            if(request == null)
            {
                return new BaseResponse<IEnumerable<RequestDto>>
                {
                    Message = "No request found",
                    IsSucess = false
                };
            }
            return new BaseResponse<IEnumerable<RequestDto>>
            {
                Message = "Requests sucessfully retrieved",
                IsSucess = true,
                Data= request
            };
        }

        public async Task<BaseResponse<IEnumerable<RequestDto>>> GetAllPendingRequestAsync()
        {
            var request = await _requestRepository.GetAllPendingRequestAsync();
            if(request == null)
            {
                return new BaseResponse<IEnumerable<RequestDto>>
                {
                    Message = "No request found",
                    IsSucess = false
                };
            }
            return new BaseResponse<IEnumerable<RequestDto>>
            {
                Message = "Requests sucessfully retrieved",
                IsSucess = true,
                Data= request
            };
        }

        public async Task<BaseResponse<IEnumerable<RequestDto>>> GetAllPendingRequestByCompanyAsync(int id)
        {
             var request = await _requestRepository.GetAllPendingRequestByCompanyAsync(id);
            if(request == null)
            {
                return new BaseResponse<IEnumerable<RequestDto>>
                {
                    Message = "No request found",
                    IsSucess = false
                };
            }
            return new BaseResponse<IEnumerable<RequestDto>>
            {
                Message = "Requests sucessfully retrieved",
                IsSucess = true,
                Data= request
            };
        }

        public async Task<BaseResponse<RequestDto>> UpdateAsync(UpdateRequestModel model , int id)
        {
            var request = await _requestRepository.GetByIdAsync(id);
            if(request == null)
            {
                return new BaseResponse<RequestDto>
                {
                    Message = "No request Found",
                    IsSucess = false
                };
            }
            request.MonthNeeded = model.MonthNeeded;
            request.YearNeeded = model.YearNeeded;
            request.Quantity = model.Quantity;
            request.Grade = model.Grade;
            var requestInfo = await _requestRepository.UpdateAsync(request);
            return new BaseResponse<RequestDto>
            {
                Message = "Request sucessfully updated",
                IsSucess = true,
                Data = requestInfo
            };

        }
    }
}