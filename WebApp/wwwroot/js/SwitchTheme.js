<script>
    document.getElementById('themeToggleBtn').addEventListener('click', function () {
        const currentTheme = localStorage.getItem('theme') || 'dark';

    if (currentTheme === 'dark') {
        document.getElementById('themeStylesheet').setAttribute('href', '/css/light-theme.css');
    localStorage.setItem('theme', 'light');
        } else {
        document.getElementById('themeStylesheet').setAttribute('href', '/css/dark-theme.css');
    localStorage.setItem('theme', 'dark');
        }
    });

    // Charger le thème correct au chargement de la page
    window.onload = function() {
        const currentTheme = localStorage.getItem('theme') || 'dark';
    document.getElementById('themeStylesheet').setAttribute('href', `/css/${currentTheme}-theme.css`);
    };
</script>
