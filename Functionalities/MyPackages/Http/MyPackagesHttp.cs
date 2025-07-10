﻿using System.Text.Json;
using GestorCorrespondencia.Frontend.Functionalities.MyPackages.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.MyPackages.Http;
public class MyPackagesHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;

    public MyPackagesHttp(ApiGetService apiGetService,
                          CustomDialogService customDialogService,
                          DialogService dialogService)
    {
        _apiGetService = apiGetService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
    }

    public async Task<IList<MyPackagesView>> GetMyPackagesAsync()
    {
        try
        {
            var response = await _apiGetService.GetAsync("usuarios/paquetes", "", 1, false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IList<MyPackagesView>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<MyPackagesView>();

            }
            else
            {
                await _customDialogService.OpenViewErrorsAsync(response);
            }
        }
        catch (Exception e)
        {

            await _customDialogService.OpenInternalErrorAsync(e);
        }

        return new List<MyPackagesView>();
    }
}