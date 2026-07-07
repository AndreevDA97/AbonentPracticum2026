// Инициализация приложения
(function () {
    // Регистрация маршрутов
    Router.register('dashboard', () => DashboardPage.render());
    Router.register('utility/:endpoint', (params) => {
        if (params.endpoint == 'cipher-text') {
            CustomUtilityPage.render(params);
            return;
        }
        UtilityPage.render(params);
    });

    // Старт
    Router.init();
})();
