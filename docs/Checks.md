# Проверки за етап 1

Изпълнени на 14.11.2026 в Linux с SDK 10.0.100:

- NuGet restore: успех; пакети 10.0.15.
- Release build: успех, 0 errors, 0 warnings.
- EF migration InitialCreate и snapshot: генерирани успешно.
- Idempotent SQL script: генериран успешно.
- Реален HTTP старт: / -> 200; /Home/About -> 200; /missing-page -> 404.
- JSON/XML и ZIP структура: проверени.

Успешна проверка с реален SQL Server 2022 в GitHub Actions:
https://github.com/victordespodski/GoSound/actions/runs/43602873848

Проверен commit: 67748b1a39282cc344e823e6f8a7ee4e82bb659c.
Създадена е временна база GoSound; приложена е InitialCreate; заредени са пет категории. Успешни са шест HTTP проверки: начало (200), категории от базата (200), празен каталог (200), филтриран каталог (200), несъществуващо предаване (404) и липсващ URL (404). Логът е в artifact stage1-smoke-results. Тестовата база е временна и се премахва с runner; това не е production hosting.

Твоят Windows SQL Server и стартът във Visual Studio не са проверени отдалечено. За тях използвай Start-GoSound.ps1 или README. Консултантският достъп изисква точния акаунт.
