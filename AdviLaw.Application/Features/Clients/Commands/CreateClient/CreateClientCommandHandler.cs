using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using AdviLaw.Application.DTOs.Client;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdviLaw.Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Response<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;
        private readonly UserManager<User> _userManager;

        public CreateClientCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler, UserManager<User> userManager)
        { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
            _userManager = userManager;  
        }
        public async Task<Response<object>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            //check user existence
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return _responseHandler.NotFound<object>("User not found");



            //if client already exists
            var existingClient = await _unitOfWork.Clients.FindFirstAsync(c => c.UserId == request.UserId);
            if (existingClient != null)
                return _responseHandler.BadRequest<object>("Client profile already exists for this user");

            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            Directory.CreateDirectory(uploadsPath);
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.NationalIDImage.FileName);

            var nationalIdImagePath = Path.Combine(uploadsPath, uniqueFileName);

            using (var stream = new FileStream(nationalIdImagePath, FileMode.Create))
                await request.NationalIDImage.CopyToAsync(stream);



            var client = _mapper.Map<Client>(request);
            client.IsApproved = false;
            client.NationalIDImagePath = "/Uploads/" + request.NationalIDImage.FileName;

            var result = await _unitOfWork.Clients.AddAsync(client);

            await _unitOfWork.SaveChangesAsync();
            
            if (result == null)
            {
                return _responseHandler.BadRequest<object>("Client creation failed. Please try again.");

            }

            var clientDto= _mapper.Map<ClientResponseDto>(result);
            return _responseHandler.Created<object>(clientDto);


        }
    }
}
