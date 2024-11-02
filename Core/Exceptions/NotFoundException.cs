namespace Core.Exceptions;

public sealed class NotFoundException(string msg) : Exception(msg);