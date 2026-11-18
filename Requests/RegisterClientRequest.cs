namespace StoreProject.Requests;

public record RegisterClientRequest(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Password,
    string Gender,
    bool TermsOfUse
);