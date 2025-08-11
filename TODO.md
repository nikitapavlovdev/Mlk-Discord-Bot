# Refactoring/Improvement TODO

---

## Общие цели
- [x] Улучшить читаемость и поддерживаемость кода

- [x] Разделить ответственности по слоям (DI, логика, инфраструктура)

- [x] Упростить добавление новых фич (масштабируемость)

- [ ] Перейти от "песочницы" к продакшен-архитектуре

---

## Startup.cs
- [x] Разбить регистрацию сервисов на модули (Core, Infrastructure, Presentation)

- [x] Вынести конфигурации JSON файлов в `IConfiguration` (appsettings.json)

- [x] Перевести запуск бота в `IHostedService` (чистый lifecycle)

- [x] Зарегистрировать бота и контроллеры через расширения

---

## Json Providers
- [x] Унифицировать все JSON-провайдеры через общий интерфейс `IJsonProvider<T>`

- [x] Добавить поддержку "Reload" для динамической перезагрузки файлов

- [ ] Вынести чтение путей в конфигурацию, не хардкодить `Path.Combine(...)`

- [ ] Перевести на `System.Text.Json` (по возможности)

---

## StaticDataServices
- [ ] Сделать поддержку нескольких пользователей 

- [ ] Словарь `UniqieLobbyNames` сделать `readonly`

- [ ] Перевести `LoadStaticData()` на `IHostedService`/инициализатор

- [ ] Добавить `IStaticDataService` интерфейс для замены и тестов

- [ ] Сделать метод `GetUniqieLobbyName(...)` безопасным — возвращать `null` или использовать `TryGetValue`

---

## Общий кодстайл
- [ ] Навести порядок в нейминге (`UniqieLobbyNames` → `UniqueLobbyNames`, `RootDiscordUsersLobby.User.GuzMan` → универсальнее)

- [ ] Разбить длинные методы на подметоды

- [ ] Убрать дублирование логики (пример: создание провайдеров)

- [ ] Добавить XML-документацию к публичным классам

---

## Возможные улучшения
- [ ] Внедрить `ILogger<>` через логгинг-фабрику везде

- [ ] Внедрить `Guard Clauses` или `FluentValidation` для входных данных

- [ ] Перевести часть логики на события/доменные уведомления (если пойдешь в DDD)

- [ ] Добавить простые unit-тесты на сервисы и провайдеры

- [ ] Использовать `.editorconfig` или `StyleCop.Analyzers`

---

## Структура проекта (на подумать)
- [ ] Ввести Feature-based структуру вместо слоями (по фичам, если проект вырастет)

- [ ] Добавить `README.md` с описанием архитектуры

---

## Тестирование
- [ ] Подключить XUnit/NUnit + Moq

- [ ] Написать тесты для `StaticDataServices` и `JsonProviders`

- [ ] Проверить работоспособность при недоступных/битых JSON-файлах

---

## DiscordEventsController

- [x] Разделить подписку и обработку ивентов

- [ ] Вынести подписку в интерфейс `IDiscordEventSubscriber`

- [x] Упростить `async`-методы, где это возможно (lambda-style)

- [ ] Покрыть тестами логику публикации событий в MediatR

---

## ChannelsCache
- [ ] Исправить опечатку: `GenereatingChannels` → `GeneratingChannels`

- [ ] Разделить на: `ChannelsCache` (хранилище) и `ChannelClassifier` (логика)

- [ ] Упростить удаление канала: заменить `foreach + Remove` на `Remove(id)`

- [x] Везде использовать `.Id` вместо сравнения объектов (`==`)

- [ ] Добавить методы GetAll* для дебага и прозрачности

- [ ] Рассмотреть добавление потокобезопасности (lock или Concurrent коллекции)

---

## TextMessageManager
- [x] Уменьшить количество зависимостей конструктора (сейчас: 12) — внедрить фасады или выделить сервисы.

- [x] Разделить класс на 2–3 более узкоспециализированных (например: `DynamicMessagesSender`, `UserNotificationSender`, `TextChannelsInitializer`).

- [x] Дублируется логика в методах Send*Message:
  - Поиск канала
  - Получение и модификация / отправка сообщения
  - Повторяющиеся блоки `if (await channel.GetMessageAsync(...)`
  - Вынести в отдельный метод-утилиту (например `TryModifyOrSendMessageAsync(...)`)

- [x] Метод `SendUserInputToDeveloper(...)` перегружен — объединить перегрузки с параметром по умолчанию `input_text2 = null`.

- [ ] Проверки на `null` каналов/гильдий не всегда есть — потенциальный `NullReferenceException` (например: `channel.SendMessageAsync(...)` без `null`-чека).

- [ ] Нет unit-тестов, а архитектура не способствует их легкой реализации — классы нужно упростить и выделить интерфейсы.

- [ ] Избыточный `await Task.CompletedTask` в `LoadTextChannelsFromGuild()` — удалить

---

## VoiceChannelsManager
- [ ] Уменьшить количество обязанностей класса — разделить на:
	- `VoiceChannelInitializer` (инициализация и загрузка каналов)
	- `TemporaryVoiceChannelsCleaner` (очистка временных каналов)
	- `VoiceChannelFactory` (создание голосовых каналов)

- [ ] В методе `GetLobbyName`:
	- [ ] Вынести логику с вероятностями в отдельный сервис/метод, чтобы избавиться от "магических чисел"
	- [ ] Отрефакторить `if-else` блок, заменить на коллекцию с шансами и итерацию

- [ ] Метод `CreateVoiceChannelAsync(...)` перегружен логикой — разделить на:
	-  Получение прав доступа `(GetPermissionOverwrites(...))`
	- Получение имени канала `(GetLobbyName(...))`
	- Конфигурацию `VoiceChannelProperties`

- [ ] Потенциальный `NullReferenceException ` в `socketVoiceChannel.Category.Id` — заменить на `socketVoiceChannel.Category?.Id`

- [ ]  Исправить магические значения:
	- `"rotterdam"` → `DefaultRtcRegion`
	- `64000 ` → `DefaultBitrate`
	- `"🔉 |` " → `DefaultLobbyPrefix` 

- [ ] В методе `ClearTemporaryVoiceChannels` дублируется логика: 
	- [ ] Проверка условий удаления и логгирования
	- [ ] Добавление в `TemporaryChannelsCache` вынести в приватные вспомогательные методы

- [ ] `LoadVoiceChannelsFromGuild` содержит `await Task.CompletedTask` без нужды — сделать метод синхронным

- [ ] Нет логгирования успешного создания канала — добавить в `CreateVoiceChannelAsync(...)`

- [ ] Не реализованы unit-тесты, архитектура не способствует — нужен рефакторинг под интерфейсы и разделение логики