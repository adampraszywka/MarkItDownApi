namespace MarkItDownApiClient.Exceptions;

public class FileTooLarge() : Exception("Provided file exceeds the maximum size set in MarkItDownApi");