using GestorCorrespondencia.Frontend.Functionalities.AccessRequest.DTO;
using GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Model;
using Riok.Mapperly.Abstractions;

namespace GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Mapper;

[Mapper(UseDeepCloning = true)]
public static partial class AccessRequestMapper
{
    public static AccessRequestDTO ADAccessRequestFormToAccessRequestDTO(this ADAccessRequestForm form)
    {
        return new AccessRequestDTO
        {
            Username = form.Username,
            Password = EncryptPassword(form.Password!),
            StoreId = 0,
            SystemCode = "SGCRS",
            AuthenticationTypeId = 1
        };
    }

    public static AccessRequestDTO WBAccessRequestExistingUserFormToAccessRequestDTO(this WBAccessRequestExistingUserForm form)
    {
        return new AccessRequestDTO
        {
            Username = form.EmployeeCode.ToString(),
            Password = EncryptPassword(form.Password!),
            StoreId = 0,
            SystemCode = "SGCRS",
            AuthenticationTypeId = 2
        };
    }

    public static AccessRequestDTO WBAccessRequestNewUserFormTOAccessRequestDTO(this WBAccessRequestNewUserForm form)
    {
        return new AccessRequestDTO
        {
            Username = form.EmployeeCode.ToString(),
            Password = EncryptPassword(form.Password!),
            StoreId = 0,
            SystemCode = "SGCRS",
            AuthenticationTypeId = 2
        };
    }

    private static string EncryptPassword(string password) =>
        UniEncryption.AesEncrypt.Encrypt(password);
}
