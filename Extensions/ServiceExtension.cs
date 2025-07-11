﻿using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Services.Security;
using GestorCorrespondencia.Frontend.Services.UI;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Shared.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using GestorCorrespondencia.Frontend.Services.SGL;
using GestorCorrespondencia.Frontend.Services.SGU;
using GestorCorrespondencia.Frontend.Functionalities.MyPackages.Http;
using GestorCorrespondencia.Frontend.Functionalities.SenderConsolidation.Http;
using GestorCorrespondencia.Frontend.Functionalities.CorrespondenceConsolidation.Http;
using GestorCorrespondencia.Frontend.Functionalities.CorrespondencePendingConsolidations.Http;
using GestorCorrespondencia.Frontend.Functionalities.ReceptionPendingConsolidations.Http;
using GestorCorrespondencia.Frontend.Shared.Components.ConsolidationReceiveList.Http;
using GestorCorrespondencia.Frontend.Functionalities.PendingPackages.Http;
using GestorCorrespondencia.Frontend.Functionalities.Tracking.Http;
using GestorCorrespondencia.Frontend.Functionalities.MyConsolidations.Http;
using GestorCorrespondencia.Frontend.Functionalities.AllConsolidations.Http;
using GestorCorrespondencia.Frontend.Functionalities.ConsolidationsMailbox.Http;
using GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Http;
using GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Http;
using GestorCorrespondencia.Frontend.Shared.Components.ViewConsolidationDetail.Http;
using GestorCorrespondencia.Frontend.Functionalities.AllPackages.Http;
using GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Http;
using GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.Http;

namespace GestorCorrespondencia.Frontend.Extensions;
public static class ServiceExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<MainLayoutService>();
        services.AddScoped<ApiService>();
        services.AddScoped<ApiGetService>();
        services.AddScoped<ApiPostService>();
        services.AddScoped<ApiHeadAccessService>();
        services.AddScoped<ApiPatchService>();
        services.AddScoped<SGLService>();
        services.AddScoped<SGUService>();
        services.AddScoped<MyPackagesHttp>();
        services.AddScoped<SenderConsolidationHttp>();
        services.AddScoped<CorrespondenceConsolidationHttp>();
        services.AddScoped<CorrespondencePendingConsolidationHttp>();
        services.AddScoped<ReceptionPendingConsolidationsHttp>();
        services.AddScoped<ConsolidationReceiveListHttp>();
        services.AddScoped<PendingPackagesHttp>();
        services.AddScoped<TrackingHttp>();
        services.AddScoped<MyConsolidationsHttp>();
        services.AddScoped<AllConsolidationsHttp>();
        services.AddScoped<ConsolidationsMailboxHttp>();
        services.AddScoped<ConsolidationTrackingHttp>();
        services.AddScoped<PackagesWithIncidentHttp>();
        services.AddScoped<ViewConsolidationDetailHttp>();
        services.AddScoped<AllPackagesHttp>();
        services.AddScoped<PackageShippingHttp>();
        services.AddScoped<CreateConsolidatedHttp>();

        services.AddScoped<IAuthCookieService, AuthCookieService>();
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        services.AddScoped<CustomAuthenticationStateProvider>();
        services.AddScoped<AccessControlService>();
        services.AddScoped<ISessionReasonService, SessionReasonService>();
        services.AddScoped<EncryptionService>();
        services.AddScoped<GetCurrentUser>();
        services.AddScoped<CustomDialogService>();

        services.AddHttpContextAccessor();
        services.AddScoped<AuthSessionService>();

        // Autenticación por cookies
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/";
                options.Cookie.Name = "GC.Auth"; // nombre de la cookie
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
            });
        services.AddAuthorization();
    }

}