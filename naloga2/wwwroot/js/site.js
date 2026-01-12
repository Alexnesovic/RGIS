(function () {
    const STORAGE_KEY = "theme";
    const btn = () => document.getElementById("themeToggle");

    function applyTheme(theme) {
        document.documentElement.setAttribute("data-theme", theme);
        const b = btn();
        if (b) b.textContent = theme === "dark" ? "☀️ Light" : "🌙 Dark";
    }

    function getPreferredTheme() {
        const saved = localStorage.getItem(STORAGE_KEY);
        if (saved === "dark" || saved === "light") return saved;

        // če ni shranjeno, vzemi sistemsko nastavitev
        const prefersDark = window.matchMedia &&
            window.matchMedia("(prefers-color-scheme: dark)").matches;
        return prefersDark ? "dark" : "light";
    }

    // init
    applyTheme(getPreferredTheme());

    // bind toggle
    window.addEventListener("DOMContentLoaded", () => {
        const b = btn();
        if (!b) return;

        b.addEventListener("click", () => {
            const current = document.documentElement.getAttribute("data-theme") || "light";
            const next = current === "dark" ? "light" : "dark";
            localStorage.setItem(STORAGE_KEY, next);
            applyTheme(next);
        });
    });
})();
