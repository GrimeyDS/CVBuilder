using CVBuilder.Shared.Models;
using MediatR;

namespace CVBuilder.Shared.Queries;

public sealed record GetProfileQuery(int? Id = null) : IRequest<GetProfileQueryResponse>;

public sealed record GetProfileQueryResponse(IEnumerable<ProfileModel> Profiles);