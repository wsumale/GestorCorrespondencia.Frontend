public class LayoutService
{
    // Eventos para el cambio de texto y el estado del sidebar
    public event Action<string> OnHeaderTextChanged = null!;
    public event Action<bool> OnSidebarExpandedChanged = null!;

    // Estado del sidebar
    private bool sidebarExpanded = true;

    // Actualizar el texto del header
    public void ActualizarHeader(string nuevoTexto)
    {
        OnHeaderTextChanged?.Invoke(nuevoTexto);
    }

    // Actualizar el estado del sidebar
    public void SetSidebarExpanded(bool expanded)
    {
        if (sidebarExpanded != expanded)
        {
            sidebarExpanded = expanded;
            OnSidebarExpandedChanged?.Invoke(expanded);
        }
    }

    // Obtener el estado actual del sidebar
    public bool GetSidebarExpanded()
    {
        return sidebarExpanded;
    }
}
