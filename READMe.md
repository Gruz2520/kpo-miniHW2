# Отчет по мини дз 2 по КПО
## Грузинцев Егор Алексеевич БПИ234

## 1. Реализация требуемого функционала

Вот как были реализованы пункты из требуемого функционала:

### a. Добавить / удалить животное
- **Реализовано в классе**: `AnimalRepository` (модуль `Infrastructure\Repositories`) через методы `Add` и `Remove`.
- **Контроллер**: `AnimalsController` (модуль `Presentation\Controllers`), методы:
  - `POST /api/animals` — добавление нового животного.
  - `DELETE /api/animals/{id}` — удаление животного по ID.

### b. Добавить / удалить вольер
- **Реализовано в классе**: `EnclosureRepository` (модуль `Infrastructure\Repositories`) через методы `Add` и `Remove`.
- **Контроллер**: `EnclosuresController` (модуль `Presentation\Controllers`), методы:
  - `POST /api/enclosures` — добавление нового вольера.
  - `DELETE /api/enclosures/{id}` — удаление вольера по ID.

### c. Переместить животное между вольерами
- **Реализовано в сервисе**: `AnimalTransferService` (модуль `Application\Services`), метод `TransferAnimal`.
- **Контроллер**: `AnimalsController`, метод:
  - `POST /api/animals/{animalId}/move-to/{newEnclosureId}` — перемещение животного в новый вольер.

### d. Просмотреть расписание кормления
- **Реализовано в репозитории**: `FeedingScheduleRepository` (модуль `Infrastructure\Repositories`), метод `GetAll`.
- **Контроллер**: `FeedingScheduleController`, метод:
  - `GET /api/feeding-schedule` — получение полного расписания кормления.

### e. Добавить новое кормление в расписание
- **Реализовано в сервисе**: `FeedingOrganizationService` (модуль `Application\Services`), метод `AddFeedingSchedule`.
- **Контроллер**: `FeedingScheduleController`, метод:
  - `POST /api/feeding-schedule` — добавление нового кормления в расписание.

### f. Просмотреть статистику зоопарка
- **Реализовано в сервисе**: `ZooStatisticsService` (модуль `Application\Services`), методы:
  - `GetTotalAnimalsCount` — общее количество животных.
  - `GetFreeEnclosuresCount` — количество свободных вольеров.
  - `GetAnimalsBySpecies` — распределение животных по видам.
  - `GetOvercrowdedEnclosures` — список переполненных вольеров.
- **Контроллер**: `ZooStatisticsController`, методы:
  - `GET /api/zoo-statistics/total-animals` — общее количество животных.
  - `GET /api/zoo-statistics/free-enclosures` — количество свободных вольеров.
  - `GET /api/zoo-statistics/animals-by-species` — распределение животных по видам.
  - `GET /api/zoo-statistics/overcrowded-enclosures` — список переполненных вольеров.

---

## 2. Применение концепций Domain-Driven Design (DDD)

### a. Использование Value Objects для примитивов
- **Value Objects реализованы в модуле**: `Domain\ValueObjects`:
  - `FoodType` — тип пищи.
  - `DietType` — тип диеты (хищник, травоядный, всеядный).
  - `Species` — вид животного.
  - `EnclosureType` — тип вольера.
  - Эти объекты инкапсулируют бизнес-правила и обеспечивают целостность данных.

### b. Инкапсуляция бизнес-правил внутри доменных объектов
- **В классе `Animal`** (модуль `Domain\Entities`) инкапсулированы правила:
  - Метод `Feed` проверяет, здоров ли животное и совпадает ли тип пищи с его предпочтениями.
  - Метод `Treat` позволяет лечить больное животное.
  - Метод `MoveToEnclosure` обеспечивает логику перемещения животного между вольерами.
- **В классе `Enclosure`** (модуль `Domain\Entities`) инкапсулированы правила:
  - Метод `AddAnimal` проверяет вместимость вольера и совместимость типа вольера с видом животного.
  - Метод `RemoveAnimal` удаляет животное из вольера.
  - Метод `Clean` выполняет уборку вольера.

### c. Использование доменных событий
- **Реализованы два доменных события**:
  - `AnimalMovedEvent` (модуль `Domain\Events`) — возникает при перемещении животного.
  - `FeedingTimeEvent` (модуль `Domain\Events`) — возникает при наступлении времени кормления.
- **События публикуются через статический класс**: `DomainEvents` (модуль `Domain\Events`).

---

## 3. Применение принципов Clean Architecture

### a. Слои зависят только внутрь
- Слой `Domain` не зависит от других слоев.
- Слой `Application` зависит только от `Domain`.
- Слой `Infrastructure` зависит от `Application` и `Domain`.
- Слой `Presentation` зависит от `Application`.

### b. Все зависимости между слоями через интерфейсы
- **Интерфейсы определены в модуле**: `Application\Interfaces`:
  - `IAnimalRepository`, `IEnclosureRepository`, `IFeedingScheduleRepository` — для работы с хранилищем.
  - `IDomainEventDispatcher` — для диспетчеризации доменных событий.
  - Сервисы (`AnimalTransferService`, `FeedingOrganizationService`, `ZooStatisticsService`) реализуют бизнес-логику через эти интерфейсы.

### c. Бизнес-логика полностью изолирована в Domain и Application слоях
- Логика работы с животными, вольерами и расписанием кормления реализована в классах `Animal`, `Enclosure`, `FeedingSchedule` (слой `Domain`).
- Логика управления процессами (перемещение, кормление, сбор статистики) реализована в сервисах `AnimalTransferService`, `FeedingOrganizationService`, `ZooStatisticsService` (слой `Application`).

---

## 4. Тестирование приложения через Swagger

Используя Swagger, были протестированы следующие операции:
- Добавление новых сущностей:
  - Животное: `POST /api/animals`.
  - Вольер: `POST /api/enclosures`.
  - Расписание кормления: `POST /api/feeding-schedule`.
- Получение информации:
  - О животных: `GET /api/animals`.
  - О вольерах: `GET /api/enclosures`.
  - О расписании кормления: `GET /api/feeding-schedule`.
- Выполнение операций:
  - Кормление: `PATCH /api/feeding-schedule/{id}/complete`.
  - Перемещение: `POST /api/animals/{animalId}/move-to/{newEnclosureId}`.

---

## 5. Хранение данных в in-memory хранилище

Хранение данных организовано в модуле `Infrastructure\Repositories`:
- Классы `AnimalRepository`, `EnclosureRepository`, `FeedingScheduleRepository` используют коллекции `List<T>` для хранения данных в памяти.

---

## Заключение

Проект соответствует требованиям и архитектурным принципам:
- Реализован весь требуемый функционал.
- Применены концепции DDD (Value Objects, инкапсуляция бизнес-правил, доменные события).
- Соблюдены принципы Clean Architecture (слои зависят только внутрь, зависимости через интерфейсы, изоляция бизнес-логики). 
