﻿using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Application.Features;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Commands.DeleteSampleModel;

public class DeleteSampleModelCommandHandler(ISampleProjectUnitOfWork unitOfWork) : ICommandQueryHandler<DeleteSampleModelCommand>
{
    public async Task<Result> Handle(DeleteSampleModelCommand request, CancellationToken cancellationToken)
    {
        var existEntity = await unitOfWork.SampleModelRepository.GetByIdAsync(request.Id, cancellationToken) 
            ?? throw new NotFoundException(BuildingBlocks.Resources.Messages.NotFound);

        await unitOfWork.SampleModelRepository.DeleteAsync(existEntity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var result = new Result();
        result.OK();
        return result;
    }
}