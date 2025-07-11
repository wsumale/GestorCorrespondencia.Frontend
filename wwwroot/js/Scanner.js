window.startGlobalScanner = function (dotNetHelper) {
    let buffer = '';
    let timer;

    document.addEventListener('keydown', function (e) {
        if (timer) clearTimeout(timer);

        if (e.key === 'Enter') {
            if (buffer.length > 0) {
                dotNetHelper.invokeMethodAsync('OnScannerInputAsync', buffer);
                buffer = '';
            }
        } else {
            buffer += e.key;
        }

        timer = setTimeout(() => {
            buffer = '';
        }, 500);
    });
};