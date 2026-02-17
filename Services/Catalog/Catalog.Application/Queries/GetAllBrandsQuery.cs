using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries;

public record GetAllBrandsQuery : IRequest<IEnumerable<BrandResponse>>
{
}

public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IEnumerable<BrandResponse>>
{
    private readonly IBrandRepository _brandRepository;

    public GetAllBrandsQueryHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<IEnumerable<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brands = await _brandRepository.GetAllBrands();

        return brands.ToResponse();
    }
}