using System;
using System.IO;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Clients.DTOs;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;

namespace AdviLaw.Application.Features.Clients.Commands
{
    public class UpdateClientProfileImageCommandHandler : IRequestHandler<UpdateClientProfileImageCommand, Response<ClientProfileDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;

        public UpdateClientProfileImageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }

        public async Task<Response<ClientProfileDTO>> Handle(UpdateClientProfileImageCommand request, CancellationToken cancellationToken)
        {
            if (request.Image == null || request.Image.Length == 0)
                return _responseHandler.BadRequest<ClientProfileDTO>("No image uploaded.");

            var client = await _unitOfWork.Clients.GetByIdIncludesAsync(request.ClientId, includes: new System.Collections.Generic.List<Expression<Func<Client, object>>>
            {
                j => j.User,
            });
            if (client == null || client.User == null)
                return _responseHandler.NotFound<ClientProfileDTO>("Client not found.");

            // Save image to Uploads folder
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            Directory.CreateDirectory(uploadsPath);
            var uniqueFileName = $"client_{request.ClientId}_profile_{Guid.NewGuid()}{Path.GetExtension(request.Image.FileName)}";
            var filePath = Path.Combine(uploadsPath, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
                await request.Image.CopyToAsync(stream, cancellationToken);

            // Update user profile image URL
            var imageUrl = $"/Uploads/{uniqueFileName}";
            client.User.ImageUrl = imageUrl;
            client.User.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Update(client.User);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var clientDto = _mapper.Map<ClientProfileDTO>(client);
            return _responseHandler.Success(clientDto);
        }
    }
} 