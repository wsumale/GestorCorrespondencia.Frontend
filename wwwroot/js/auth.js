window.auth = {
    setTempSession: function (token, userJson) {
        document.cookie = `temp_token=${token}; path=/`;
        document.cookie = `temp_user=${encodeURIComponent(userJson)}; path=/`;
    },
    setTempRefreshToken: function (token, exp) {
        document.cookie = `temp_refresh_token=${token}; path=/`;
        document.cookie = `temp_exp=${exp}; path=/`;
    }
};
