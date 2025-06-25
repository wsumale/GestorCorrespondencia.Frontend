window.auth = window.auth || {};

// Funciones existentes
window.auth.setTempSession = function (token, userJson) {
    document.cookie = `temp_token=${token}; path=/`;
    document.cookie = `temp_user=${encodeURIComponent(userJson)}; path=/`;
};

window.auth.setTempRefreshToken = function (token, exp) {
    document.cookie = `temp_refresh_token=${token}; path=/`;
    document.cookie = `temp_exp=${exp}; path=/`;
};

// NUEVA función para renovar la cookie de sesión sin recargar la página
window.auth.refreshSession = async function(payloadJson) {
    await fetch('/auth/refresh-session', {
        method: 'POST',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json'
        },
        body: payloadJson
    });
};
