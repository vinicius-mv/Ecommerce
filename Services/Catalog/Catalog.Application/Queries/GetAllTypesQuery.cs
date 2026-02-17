using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries;

public record class GetAllTypesQuery : IRequest<IEnumerable<TypeResponse>>
{
}

public record class GetAllTypesQueryHandler : IRequestHandler<GetAllTypesQuery, IEnumerable<TypeResponse>>
{
    private readonly ITypeRepository _typeRepository;

    public GetAllTypesQueryHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }

    public async Task<IEnumerable<TypeResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var types = await _typeRepository.GetAllTypes();
        return types.ToResponse();
    }
}
