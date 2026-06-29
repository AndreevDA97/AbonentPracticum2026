// Дашборд со списком утилит
const DashboardPage = {
    async render() {
        const content = document.getElementById('app-content');
        content.innerHTML = '<div class="loading">Загрузка утилит</div>';

        try {
            const utilities = await API.get('/utilities');

            // Группировка по категориям
            const categories = [...new Set(utilities.map(u => u.category))];

            let html = `
                <h1 class="page-title">📦 Внутренние утилиты</h1>
                <p class="page-subtitle">Выберите инструмент для работы. Реализованные отмечены зелёным, остальные — задания для студентов.</p>

                <div class="filter-bar">
                    <button class="filter-btn active" data-filter="all">Все (${utilities.length})</button>
                    <button class="filter-btn" data-filter="ready">✅ Реализованы (${utilities.filter(u => u.isImplemented).length})</button>
                    ${categories.map(c => `<button class="filter-btn" data-filter="${c}">${c}</button>`).join('')}
                </div>

                <div class="utilities-grid" id="utilities-grid">
                    ${utilities.map(u => DashboardPage.cardHtml(u)).join('')}
                </div>`;

            content.innerHTML = html;

            // Фильтрация
            document.querySelectorAll('.filter-btn').forEach(btn => {
                btn.addEventListener('click', () => {
                    document.querySelectorAll('.filter-btn').forEach(b => b.classList.remove('active'));
                    btn.classList.add('active');
                    DashboardPage.filter(btn.dataset.filter);
                });
            });

            // Клик по карточке
            document.querySelectorAll('.utility-card').forEach(card => {
                card.addEventListener('click', () => {
                    location.hash = `utility/${card.dataset.endpoint}`;
                });
            });

        } catch (err) {
            content.innerHTML = `<div class="error-message">Ошибка загрузки: ${err.message}</div>`;
        }
    },

    cardHtml(u) {
        const stars = '★'.repeat(u.difficulty) + '☆'.repeat(3 - u.difficulty);
        const statusBadge = u.isImplemented
            ? '<span class="badge badge-ready">✅ Готово</span>'
            : '<span class="badge badge-todo">📝 Задание</span>';

        return `
            <div class="utility-card" data-endpoint="${u.endpoint}">
                <div class="utility-card-header">
                    <h3>${u.name}</h3>
                    ${statusBadge}
                </div>
                <p>${u.description}</p>
                <div class="utility-card-footer">
                    <span class="badge badge-category">${u.category}</span>
                    <span class="difficulty-stars" title="Сложность: ${u.difficulty}/3">${stars}</span>
                </div>
            </div>`;
    },

    filter(criterion) {
        const grid = document.getElementById('utilities-grid');
        const cards = grid.querySelectorAll('.utility-card');

        if (criterion === 'all') {
            cards.forEach(c => c.style.display = '');
            return;
        }

        if (criterion === 'ready') {
            cards.forEach(c => {
                const isReady = c.querySelector('.badge-ready') !== null;
                c.style.display = isReady ? '' : 'none';
            });
            return;
        }

        // Фильтр по категории
        cards.forEach(c => {
            const cat = c.querySelector('.badge-category').textContent;
            c.style.display = cat === criterion ? '' : 'none';
        });
    }
};
