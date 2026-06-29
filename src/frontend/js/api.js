// API-клиент для взаимодействия с backend
const API = {
    async get(path) {
        const res = await fetch(`${APP_CONFIG.apiBaseUrl}${path}`);
        if (!res.ok) {
            const text = await res.text();
            throw new Error(`HTTP ${res.status}: ${text || res.statusText}`);
        }
        return res.json();
    },

    async post(path, body) {
        const res = await fetch(`${APP_CONFIG.apiBaseUrl}${path}`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(body)
        });
        if (!res.ok) {
            const text = await res.text();
            throw new Error(`HTTP ${res.status}: ${text || res.statusText}`);
        }
        return res.json();
    }
};
