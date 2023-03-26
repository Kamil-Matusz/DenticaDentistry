using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Infrastructure.DAL.Decorators;

internal sealed class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;
    private readonly IUnitOfWork _unitOfWork;
    
    public UnitOfWorkCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, IUnitOfWork unitOfWork)
    {
        _commandHandler = commandHandler;
        _unitOfWork = unitOfWork;
    }
    public async Task HandlerAsync(TCommand command)
    {
        await _unitOfWork.ExecuteAsync(() => _commandHandler.HandlerAsync(command));
    }
}