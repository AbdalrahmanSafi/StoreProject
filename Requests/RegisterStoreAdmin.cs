namespace StoreProject.Requests;

public record RegisterStoreAdminRequest(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Password,
    bool TermsOfUse
);