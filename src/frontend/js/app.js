// Инициализация приложения
(function () {
    // Регистрация маршрутов
    Router.register('dashboard', () => DashboardPage.render());
    Router.register('utility/:endpoint', (params) => UtilityPage.render(params));

    // Старт
    Router.init();
})();
