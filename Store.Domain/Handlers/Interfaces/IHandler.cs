using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Handlers
{
    // * Handlers: Gerenciam os fluxos da Aplicação

    public interface IHandler<T> where T : ICommand
    {
         ICommandResult Handle(T command);
    }
}