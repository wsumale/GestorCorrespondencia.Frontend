﻿using GestorCorrespondencia.Frontend.Shared.Interfaces;

namespace GestorCorrespondencia.Frontend.Shared.Model;
public class ApiResponse<T> : IApiResponse<T>
{
    public bool Success { get; set; }
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
}