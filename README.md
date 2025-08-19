# Malenkie Admin Bot 1.3.0
## 1. Цель 
**Admin Bot** для сервера Malenkie - это бот-админ для моего сервера в Discord. Я написал его для упрощения работы администрации и улучшения своей гильдии внутри Discord, путем добавления удобного логгирования, сохранения данных в базы данных и автоматизации некоторых процессов, например, авторизация.

## 2. Предисловие
Данный проект стал для меня дверью в прикладное программирование. Многие в начале начинают писать сайты, либо парсеры или тех же ботов. Я не стал исключением и решил совместить приятное с полезным. Целью было изучение языка С# и его концепций. Я хотел углубиться во что-то более интересное, чем просто консольные приложения без взаимодействия с внешним миром, дополнить модель моего понимания программирования и стать хоть чуть, но более компетентым специалистом. С таких мотивов проект и стартовал.

## 3. Техническая часть
Техническая часть представлена следующими технологиями:
- `Discord.NET` - SDK (Software Development Kit) для взаимодействия с Discord API.
- `MediatR` - библиотека для .NET, реализующая паттерн "Посредник". Она позволяет объектам взаимодействовать друг с другом через посредника, уменьшая прямые зависимости и способствуя слабой связанности.
- `DI(Dependency Injection)` - инструмент, который управляет созданием и предоставлением зависимостей (объектов) для других объектов. Он упрощает разработку, делая код более модульным, тестируемым и удобным в поддержке.
- `Entity Framework Core` - это объектно-реляционный преобразователь (O/RM), который позволяет разработчикам .NET работать с базами данных, используя объекты .NET.

## 4. Ключевые примеры 
`Discord.NET` построен на событийной модели - клиент не опрашивает сервер, а реагирует на входящие события. В моем проекте взаимодействие с API Discord представлено следующей схемой. 
<br> [`Discord Event`] → [`DiscordEventsListener`] → [`MediatR Publish Notification`] → [`Handler`].
<br> По данной схеме реализованы все события, необходимые для выполнения поставленных задач ботом на сервере. 
На примере события `UserJoined` рассмотрим пример из проекта:
- В классе `DiscordEventsListener` происходит подписка на событие `UserJoined`:<br>
	
    ```csharp
        public void SubscribeOnEvents(DiscordSocketClient client)
        {
            ...
            client.UserJoined += OnUserJoined;
        }
    ```
- В этом же классе публикуются уведомления о событии:
    ```csharp
        private async Task OnUserJoined(SocketGuildUser socketGuildUser)
        {
            try
            {
                ...
                await mediator.Publish(new UserJoined(socketGuildUser));
            }
        }
    ```
- Уведомления получаются все подписанные обработчики. В моем случае обработчик всегда один, но никто не мешает сделать больше:

    ```csharp
        public async Task Handle(UserJoined notification, CancellationToken cancellationToken)
        {
            try
            {
                if (notification.SocketGuildUser.IsBot) { return; }

                await rolesManager.AddNotRegisteredRoleAsync(notification.SocketGuildUser);
                await textChannelManager.SendWelcomeMessageAsync(notification.SocketGuildUser);
                await moderatorLogsSender.SendLogMessageAsync(new DTOs.LogMessageDto
                {
                    Description = $"> Пользователь {notification.SocketGuildUser.Mention} присоединился к серверу",
                    Title = "Новый пользователь",
                    GuildId = notification.SocketGuildUser.Guild.Id,
                    ChannelId = jsonChannelsMapProvider.LogsChannelId,
                    UserId = notification.SocketGuildUser.Id

                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при работе события UserJoinedHandler");
            }
        }
    ```
Стоит прояснить один момент: `UserJoined ` — это класс уведомления, реализующий `INotification` (для тех, кто с MediatR не знаком).

Это основной механизм обработки событий в моем проекте. 
В каждом хэндлере в зависимости от идеи идет уже реализация механизмом/фич. 
Подробнее можно посмотреть внутри каждого класса-обработчика.

## 5. Точка входа в проект
Точкой входа в проект является класс `Startup.cs`. 
В нем создается и настраивается `Host`, включая конфигурацию сервисов через DI-контейнер, добавление логгирования и запуск приложения. 
Здесь происходит регистрация необходимых сервисов (Domain, Application, Infrastructure, Presentation), настройка логгирования (Debug, Console) и запуск хоста с асинхронным ожиданием завершения.

```csharp
    public static async Task Main()
    {
        IHost host = Host.CreateDefaultBuilder() //Создание хоста
            .ConfigureServices((services) =>
            {
                services.AddDomainServices(); // Подключение сервисов слоя Domain
                services.AddApplicationServices(); // Подключение сервисов слоя Application
                services.AddInfrastructureServices(); // Подключение сервисов слоя Infrastructure
                services.AddPresentationServices(); // Подключение сервисов слоя Presentation
            })
            .ConfigureLogging((logging) =>
            {
                logging.AddDebug(); //Добавление логирование через терминал
                logging.AddConsole(); //Добавление логирования через консоль
            })
            .Build();

        await host.RunAsync(); //Запуск приложения
    }
```
## 6. Как запустить 
Я не задумывал этого бота в виде шаблонного решения и при "поднятии" проекта неизбежно возникнут проблемы
из-за того, что много частей кода зависят от `id'шников` и `string-путей` моего сервера. Например, куда будет отправляться приветственное сообщение: 

```csharp 
    SocketTextChannel? textChannel = socketGuildUser.Guild.TextChannels.FirstOrDefault(x => x.Id == jsonChannelsMapProvider.StartingChannelId);
    ...
    await textChannel.SendMessageAsync($"{socketGuildUser.Mention}", embed: embedMessage, components: MessageComponentsExtension.GetServerHubLinkButton(jsonChannelsMapProvider.HubChannelHttps));
```
Но как скелет можно выделить основные действия:
1. Установите `Discord.NET` через NuGet.
2. Создайте и зарегистрируйте приложение, чтобы получить `API-ключ` на [Discord Developer Portal](https://discord.com/developers/applications). 
3. С данного репозитория вам потребуется `Startup.cs`, `DiscordBotHostService.cs`, `DiscordEventsListener`, `Registration.cs`.
4. В `Startup` нужно немного изменить подключение сервисов, оставив только `services.AddPresentationServices();`
5. В `Registration` оставляем такой вот метод: 

```csharp 
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddHostedService<DiscordBotHostService>();

        services.AddSingleton(new DiscordSocketClient(new() { GatewayIntents = GatewayIntents.All})); //GatewayIntents.All - всевозможные события Discord.NET

        return services;
    }
```
6. В `DiscordBotHostService` меняем строку:<br> `string? MlkAdminBotApiKey = УКАЗЫВАЕМ_ВАШ_API_KEY`. УКАЗЫВАЕМ_ВАШ_API_KEY - ключ, который вы получили на 2 шаге.
7. Если все окей, то в консоли будет предположительный log-месседж:
`Лог-сообщение: Discord.Net v3.17.4 (API v10)` и т.д.

## 7. Особенности 
Причина выбора стека технологий очень проста — её подсказал старший товарищ. 
На момент разработки я был совсем новичком и мало что понимал. 
Даже сейчас, полностью разобравшись с проектом, я не стану давать советы по выбору технологий из-за ограниченного опыта. Этот раздел оставляю для возможного расширения в будущем.