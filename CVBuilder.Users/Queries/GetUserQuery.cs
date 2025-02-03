using CVBuilder.Users.Models;
using MediatR;

namespace CVBuilder.Users.Queries;

internal sealed record GetUserQuery(int? Id = 0) : IRequest<GetUserQueryResponse>;

internal sealed record GetUserQueryResponse(IEnumerable<UserModel> Users);