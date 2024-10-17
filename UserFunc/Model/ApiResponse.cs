namespace UserFunc.Model;

public record ApiResponse(
    bool IsSuccess,
    string Code,
    object? Item,
    ErrorResponseData? Error
);

public record ErrorResponseData(
    string Code,
    string Description,
    IEnumerable<ErrorResponseElement>? Details
    );

public record ErrorResponseElement (
    string Code,
    string Description,
    IEnumerable<string>?  Validations
    );
