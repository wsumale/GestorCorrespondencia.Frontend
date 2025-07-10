using System.Text.Json;
using GestorCorrespondencia.Frontend.Functionalities.Authentication.DTO;
using GestorCorrespondencia.Frontend.Functionalities.Authentication.Model.Form;
using GestorCorrespondencia.Frontend.Functionalities.Authentication.Pages;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Services.Security;
using Microsoft.AspNetCore.Components;
using QRCoder;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.Authentication.Components;
public partial class Form2FA
{
    [Inject] NotificationService NotificationService { get; set; } = default!;
    [Inject] ApiGetService ApiGetService { get; set; } = default!;
    [Inject] ApiPostService ApiPostService { get; set; } = default!;
    [Inject] DialogService DialogService { get; set; } = default!;
    [Inject] EncryptionService EncryptionService { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;

    [Parameter] public LoginRequestDto Login { get; set; } = default!;
    private readonly TwoFactorAuthFormModel Form = new();
    private readonly TwoFactorSettingsRequestDto Request = new();
    private string qr = "";

    protected override void OnInitialized()
    {
        Request.username = Login.Username;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await GetSettingsAsync();
            StateHasChanged();
        }
    }

    private async Task OnSubmitAsync()
    {
        try
        {
            Request.authenticatorCode = Form.AuthenticatorCode;

            var response = await ApiPostService.PostAsync($"auth/2fa/validar-codigo?username={Request.username}&authenticatorCode={Request.authenticatorCode}&secretKey={Request.secretKey}", Request, 2, false);

            if (response.IsSuccessStatusCode)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Configuración 2FA Completada", Detail = "Inicia sesión nuevamente ", Duration = 3000 });
                DialogService.Close();
            }
            else
            {
                await CustomDialogService.OpenViewErrorsAsync(response);
            }
        }
        catch (Exception e)
        {
            await DialogService.Alert(e.Message, "Error de Conexión", new AlertOptions() { OkButtonText = "Aceptar" });
        }
    }

    private async Task GetSettingsAsync()
    {
        try
        {
            var response = await ApiGetService.GetAsync($"auth/2fa/configurar?username={Login.Username}", "", 2, true);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var Settings = JsonSerializer.Deserialize<TwoFactorSettingsResponseDto>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                Request.secretKey = EncryptionService.Decrypt(Settings!.secretKey!);
                qr = GenerateQRCode(EncryptionService.Decrypt(Settings.qrCodeUri!));
            } else
            {
                await CustomDialogService.OpenViewErrorsAsync(response);
            }
        }
        catch (Exception e)
        {
            await DialogService.Alert(e.Message, "Error de Conexión", new AlertOptions() { OkButtonText = "Aceptar" });
        }
    }

    public string GenerateQRCode(string text)
    {
        using QRCodeGenerator qrGenerator = new QRCodeGenerator();
        using QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
        using SvgQRCode qrCode = new SvgQRCode(qrCodeData);
        return qrCode.GetGraphic(5);
    }
}