using GestorCorrespondencia.Frontend.Functionalities.Authentication.DTO;
using GestorCorrespondencia.Frontend.Functionalities.Authentication.Model.Form;
using Riok.Mapperly.Abstractions;

namespace GestorCorrespondencia.Frontend.Functionalities.Authentication.Mapper;

[Mapper(UseDeepCloning = true)]
public static partial class LoginMapper
{
    [MapperIgnoreTarget(nameof(LoginRequestDto.StoreId))]
    [MapperIgnoreTarget(nameof(LoginRequestDto.SystemCode))]
    [MapProperty(nameof(LoginForm.AuthenticatorCode), nameof(LoginRequestDto.AuthenticatorCode))]
    [MapProperty(nameof(LoginForm.Username), nameof(LoginRequestDto.Username))]
    [MapPropertyFromSource(nameof(LoginForm.Password), Use = nameof(EncryptPassword))]
    public static partial LoginRequestDto LoginFormToLoginRequestDto(this LoginForm form);

    private static string EncryptPassword(LoginForm form) =>
        UniEncryption.AesEncrypt.Encrypt(form.Password!);

}
