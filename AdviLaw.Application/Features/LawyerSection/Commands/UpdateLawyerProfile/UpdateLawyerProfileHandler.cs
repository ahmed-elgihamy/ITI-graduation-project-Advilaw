using AdviLaw.Application.Basics;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace AdviLaw.Application.Features.LawyerSection.Commands.UpdateLawyerProfile
{
    public class UpdateLawyerProfileHandler(
        IUnitOfWork unitOfWork,
        ResponseHandler responseHandler,
        IMapper mapper
        ) : IRequestHandler<UpdateLawyerProfileCommand, Response<LawyerProfileDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly ResponseHandler _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<LawyerProfileDTO>> Handle(UpdateLawyerProfileCommand request, CancellationToken cancellationToken)
        {
                var lawyer = await _unitOfWork.Lawyers.GetByIdIncludesAsync(
                request.LawyerId,
                includes: new List<Expression<Func<Lawyer, object>>>
                {
                    x => x.User!,
                }
                );
            if (lawyer == null)
            {
                return _responseHandler.NotFound<LawyerProfileDTO>("Lawyer not found.");
            }
            // Map the request to the lawyer entity
            _mapper.Map(request, lawyer);

            // Handle profile image upload (base64 to file)
            if (!string.IsNullOrEmpty(request.ImageUrl))
            {
                if (request.ImageUrl.StartsWith("data:image"))
                {
                    var base64Data = Regex.Replace(request.ImageUrl, @"^data:image\/[^;]+;base64,", string.Empty);
                    var fileName = $"{Guid.NewGuid()}.png";
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    byte[] imageBytes = Convert.FromBase64String(base64Data);
                    File.WriteAllBytes(filePath, imageBytes);
                    // Build the full URL
                    var baseUrl = "https://localhost:44302"; // You may want to make this configurable
                    var fullImageUrl = $"{baseUrl}/Uploads/{fileName}";
                    lawyer.User.ImageUrl = fullImageUrl;
                }
                else
                {
                    lawyer.User.ImageUrl = request.ImageUrl;
                }
            }

            // Update the lawyer profile in the database
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            // Map the updated lawyer entity to DTO
            var lawyerProfileDto = _mapper.Map<LawyerProfileDTO>(lawyer);

            return _responseHandler.Success(lawyerProfileDto, "Lawyer profile updated successfully.");
        }
    }
}
