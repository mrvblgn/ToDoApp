namespace Core.Exceptions;

public sealed class BusinessException(string msg) : Exception(msg);