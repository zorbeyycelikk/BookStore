using AutoMapper;
using MediatR;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class BookCommandHandler :
    IRequestHandler<CreateBookCommand, ApiResponse>,
    IRequestHandler<CreateBookRangeCommand, ApiResponse<BookResponse>>,
    IRequestHandler<DeleteBookCommand, ApiResponse>,
    IRequestHandler<DeleteBookAllCommand, ApiResponse>,
    IRequestHandler<HardDeleteBookCommand, ApiResponse>,
    IRequestHandler<HardBookAllCommand, ApiResponse>,
    IRequestHandler<UpdateBookCommand, ApiResponse>
{
    // private readonly VkDbContext dbContext;
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public BookCommandHandler(IMapper mapper, IUnitOfWork unitOfWork,VkDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }
    public async Task<ApiResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        Book entity = mapper.Map<Book>(request.Model);
        unitOfWork.BookRepository.Insert(entity,cancellationToken);
        unitOfWork.CompleteTransaction();
        return new ApiResponse("Create Command Succes !");
    }

    public Task<ApiResponse<BookResponse>> Handle(CreateBookRangeCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        unitOfWork.BookRepository.Delete(request.Id);
        unitOfWork.Complete();
        return new ApiResponse("DeleteById Basarili");
    }

    public async Task<ApiResponse> Handle(DeleteBookAllCommand request, CancellationToken cancellationToken)
    {
        unitOfWork.BookRepository.DeleteAll();
        unitOfWork.Complete();
        return new ApiResponse("Delete All Basarili");
    }

    public Task<ApiResponse> Handle(HardDeleteBookCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> Handle(HardBookAllCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.BookRepository.GetById(request.Id,cancellationToken,"Author","Category");
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }
        entity.BookNumber = request.Model.BookNumber;
        entity.PageCount = request.Model.PageCount;
        entity.Publisher = request.Model.Publisher;
        unitOfWork.BookRepository.Update(request.Id, request.Model, cancellationToken);
        unitOfWork.CompleteTransaction();
        return new ApiResponse();
    }
}