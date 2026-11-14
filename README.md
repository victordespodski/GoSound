# GoSound — етап 1

Платформа за подкасти и аудио предавания. Авторът и класът се попълват в отделната училищна документация.

## Проверки и статус

Проектът е компилиран в Linux с .NET SDK 10.0.100: 0 грешки и 0 предупреждения. Пакетите са обновени до 10.0.15. Генерирани са InitialCreate миграцията, snapshot и idempotent SQL схема. Реалната SQL Server проверка се изпълнява от GitHub Actions; актуалният резултат е в Actions и docs/Checks.md. Локалната ти Windows база не се създава автоматично от публикуването в GitHub.

Repository: https://github.com/victordespodski/GoSound
Публична техническа документация: docs/Stage1.md. Word/PDF документацията с лични полета се предоставя отделно и не се публикува тук.

## Ако Visual Studio не поддържа .NET 10

Това е ограничение на IDE, не доказателство за грешка в кода. Можеш да използваш актуализиран Visual Studio или .NET 10 SDK през PowerShell. От папката на solution изпълни `./Start-GoSound.ps1`. Скриптът проверява SDK, пита за LocalDB или custom connection string, прилага миграцията и стартира сайта. Не променя версията на Visual Studio. При блокирани PowerShell scripts изпълни командите по-долу ръчно; не е необходимо да изключваш защитите.

## Отваряне във Visual Studio

1. Направи резервно копие на старата папка. Разархивирай пакета в нова папка.
2. Отвори GoSound.slnx във Visual Studio с ASP.NET workload и .NET 10 SDK.
3. Ако искаш старата папка: затвори VS, преименувай старата папка с дата и копирай новото съдържание на нейното място. Не смесвай двата проекта и не изтривай стария без backup.
4. Структурата е GoSound.slnx и подпапка GoSound/GoSound.csproj — съвпада с качения solution.

## SQL Server и стартиране

Отвори Terminal в папката на solution. За Windows SQL Server LocalDB, ако е инсталиран:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(localdb)\MSSQLLocalDB;Database=GoSound;Trusted_Connection=True;TrustServerCertificate=True;" --project GoSound/GoSound.csproj
```

За съществуващ Docker SQL Server използвай твоята действителна парола локално, без да я качваш. Примерът е за host port 14330; потвърди port mapping:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=127.0.0.1,14330;Database=GoSound;User Id=sa;Password=<LOCAL_PASSWORD>;Encrypt=True;TrustServerCertificate=True;" --project GoSound/GoSound.csproj
```

TrustServerCertificate е настройка за локалната учебна среда; при публично внедряване се използва валиден сертификат. User Secrets е само за разработка.

След това:

```powershell
dotnet restore GoSound.slnx
dotnet tool restore
dotnet ef database update --project GoSound/GoSound.csproj
dotnet build GoSound.slnx
dotnet run --project GoSound/GoSound.csproj
```

Отвори http://localhost:5180. Алтернативно Setup-Database.ps1 извършва restore, генерира първата миграция само ако липсва и прилага миграциите. Началната миграция и snapshot вече са включени. При следващ старт не генерирай InitialCreate повторно.

## Налично в кода

- Начална страница и адаптивна тъмносиня/тюркоазена визия.
- Категории и каталог от SQL Server; заглавно търсене и филтър по категория.
- Детайли на публикувано предаване; безопасно празно състояние без фиктивно аудио.
- Identity модел за бъдещ вход, шест домейн entities, DbContext и начални категории.
- Custom error изгледи и build workflow за GitHub Actions.

## Планирано

Вход и роли, CRUD, качване и плеър, прогрес, следвания, коментари, Admin area и тестове. Identity таблиците не означават, че входът вече е реализиран. Крайният критерий за 5 controllers/models/views и 65% функционално unit покритие още не е изпълнен.

## База

Category 1:N Podcast; ApplicationUser 1:N Podcast; Podcast 1:N Episode; Follow свързва User/Podcast; ListeningProgress свързва User/Episode. Външните ключове използват Restrict за контролиран жизнен цикъл. Seed съдържа само категории, без потребители или пароли. Каталогът е празен до създаване на съдържание в следващ етап.

## Етап 1 предаване

Виж docs/Stage1.md за концепция, увод и проверките, docs/Research.md за предварително проучване и docs/Database.md за модела. Пренеси текста в собствено копие на официалния Word шаблон, запазвайки стиловете. Добави реални repo и documentation линкове след създаването им и осигури достъп на консултанта.
