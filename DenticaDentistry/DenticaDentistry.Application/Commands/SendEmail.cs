using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Application.Commands;

public record SendEmail(string Recipient, string Subject, string Body) : ICommand;