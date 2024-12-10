WarehouseApp
WarehouseApp е приложение за управление на складове, предоставящо ефективни инструменти за управление на продукти, заявки, поръчки, продажби и потребители. Проектът е изграден с ASP.NET Core и следва модулна структура, разделена на слоеве за по-голяма скалируемост и поддръжка.

Съдържание
Функционалности
Структура на проекта
Технологии
Инсталация
Как да използвате
Тестове
Принос
Лиценз
Функционалности
Управление на продукти, включително добавяне, редактиране и категоризация.
Проследяване на заявки от клиенти и дистрибутори.
Управление на поръчки, включително преглед, създаване и редактиране на статус.
Управление на продажби и генериране на отчети.
Активиране, деактивиране и управление на потребителски акаунти.
Поддръжка на роли за различни типове потребители: клиенти, дистрибутори, складови работници и доставчици.
Структура на проекта
Проектът е разделен на няколко слоя:

WarehouseApp.Common
Съдържа константи и помощни съобщения.

WarehouseApp.Data.Models
Модели на базата данни, включително:

Потребители (наследяват ApplicationUser).
Продукти, категории, заявки, поръчки и продажби.
WarehouseApp.Data
Конфигурация на базата данни с помощта на Fluent API.
Скрити и миграции за управление на базата данни.
WarehouseApp.Services.Data
Бизнес логика за управление на поръчки, продукти, заявки, продажби и потребители.

WarehouseApp.Services.Mapping
Мапинг между модели и вю модели с помощта на AutoMapper.

WarehouseApp.Services.Tests
Тестове за услуги, използващи xUnit.

WarehouseApp.Web
ASP.NET Core MVC проект, който предоставя интерфейс за потребителите.

WarehouseApp.Web.ViewModels
Съдържа модели за изгледи, използвани в потребителския интерфейс.

Технологии
ASP.NET Core 6
Entity Framework Core
AutoMapper
nUnit
Microsoft Identity
