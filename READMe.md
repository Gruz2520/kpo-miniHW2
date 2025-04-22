# ����� �� ���� �� 2 �� ���
## ��������� ���� ���������� ���234

## 1. ���������� ���������� �����������

��� ��� ���� ����������� ������ �� ���������� �����������:

### a. �������� / ������� ��������
- **����������� � ������**: `AnimalRepository` (������ `Infrastructure\Repositories`) ����� ������ `Add` � `Remove`.
- **����������**: `AnimalsController` (������ `Presentation\Controllers`), ������:
  - `POST /api/animals` � ���������� ������ ���������.
  - `DELETE /api/animals/{id}` � �������� ��������� �� ID.

### b. �������� / ������� ������
- **����������� � ������**: `EnclosureRepository` (������ `Infrastructure\Repositories`) ����� ������ `Add` � `Remove`.
- **����������**: `EnclosuresController` (������ `Presentation\Controllers`), ������:
  - `POST /api/enclosures` � ���������� ������ �������.
  - `DELETE /api/enclosures/{id}` � �������� ������� �� ID.

### c. ����������� �������� ����� ���������
- **����������� � �������**: `AnimalTransferService` (������ `Application\Services`), ����� `TransferAnimal`.
- **����������**: `AnimalsController`, �����:
  - `POST /api/animals/{animalId}/move-to/{newEnclosureId}` � ����������� ��������� � ����� ������.

### d. ����������� ���������� ���������
- **����������� � �����������**: `FeedingScheduleRepository` (������ `Infrastructure\Repositories`), ����� `GetAll`.
- **����������**: `FeedingScheduleController`, �����:
  - `GET /api/feeding-schedule` � ��������� ������� ���������� ���������.

### e. �������� ����� ��������� � ����������
- **����������� � �������**: `FeedingOrganizationService` (������ `Application\Services`), ����� `AddFeedingSchedule`.
- **����������**: `FeedingScheduleController`, �����:
  - `POST /api/feeding-schedule` � ���������� ������ ��������� � ����������.

### f. ����������� ���������� ��������
- **����������� � �������**: `ZooStatisticsService` (������ `Application\Services`), ������:
  - `GetTotalAnimalsCount` � ����� ���������� ��������.
  - `GetFreeEnclosuresCount` � ���������� ��������� ��������.
  - `GetAnimalsBySpecies` � ������������� �������� �� �����.
  - `GetOvercrowdedEnclosures` � ������ ������������� ��������.
- **����������**: `ZooStatisticsController`, ������:
  - `GET /api/zoo-statistics/total-animals` � ����� ���������� ��������.
  - `GET /api/zoo-statistics/free-enclosures` � ���������� ��������� ��������.
  - `GET /api/zoo-statistics/animals-by-species` � ������������� �������� �� �����.
  - `GET /api/zoo-statistics/overcrowded-enclosures` � ������ ������������� ��������.

---

## 2. ���������� ��������� Domain-Driven Design (DDD)

### a. ������������� Value Objects ��� ����������
- **Value Objects ����������� � ������**: `Domain\ValueObjects`:
  - `FoodType` � ��� ����.
  - `DietType` � ��� ����� (������, ����������, ��������).
  - `Species` � ��� ���������.
  - `EnclosureType` � ��� �������.
  - ��� ������� ������������� ������-������� � ������������ ����������� ������.

### b. ������������ ������-������ ������ �������� ��������
- **� ������ `Animal`** (������ `Domain\Entities`) ��������������� �������:
  - ����� `Feed` ���������, ������ �� �������� � ��������� �� ��� ���� � ��� ��������������.
  - ����� `Treat` ��������� ������ ������� ��������.
  - ����� `MoveToEnclosure` ������������ ������ ����������� ��������� ����� ���������.
- **� ������ `Enclosure`** (������ `Domain\Entities`) ��������������� �������:
  - ����� `AddAnimal` ��������� ����������� ������� � ������������� ���� ������� � ����� ���������.
  - ����� `RemoveAnimal` ������� �������� �� �������.
  - ����� `Clean` ��������� ������ �������.

### c. ������������� �������� �������
- **����������� ��� �������� �������**:
  - `AnimalMovedEvent` (������ `Domain\Events`) � ��������� ��� ����������� ���������.
  - `FeedingTimeEvent` (������ `Domain\Events`) � ��������� ��� ����������� ������� ���������.
- **������� ����������� ����� ����������� �����**: `DomainEvents` (������ `Domain\Events`).

---

## 3. ���������� ��������� Clean Architecture

### a. ���� ������� ������ ������
- ���� `Domain` �� ������� �� ������ �����.
- ���� `Application` ������� ������ �� `Domain`.
- ���� `Infrastructure` ������� �� `Application` � `Domain`.
- ���� `Presentation` ������� �� `Application`.

### b. ��� ����������� ����� ������ ����� ����������
- **���������� ���������� � ������**: `Application\Interfaces`:
  - `IAnimalRepository`, `IEnclosureRepository`, `IFeedingScheduleRepository` � ��� ������ � ����������.
  - `IDomainEventDispatcher` � ��� ��������������� �������� �������.
  - ������� (`AnimalTransferService`, `FeedingOrganizationService`, `ZooStatisticsService`) ��������� ������-������ ����� ��� ����������.

### c. ������-������ ��������� ����������� � Domain � Application �����
- ������ ������ � ���������, ��������� � ����������� ��������� ����������� � ������� `Animal`, `Enclosure`, `FeedingSchedule` (���� `Domain`).
- ������ ���������� ���������� (�����������, ���������, ���� ����������) ����������� � �������� `AnimalTransferService`, `FeedingOrganizationService`, `ZooStatisticsService` (���� `Application`).

---

## 4. ������������ ���������� ����� Swagger

��������� Swagger, ���� �������������� ��������� ��������:
- ���������� ����� ���������:
  - ��������: `POST /api/animals`.
  - ������: `POST /api/enclosures`.
  - ���������� ���������: `POST /api/feeding-schedule`.
- ��������� ����������:
  - � ��������: `GET /api/animals`.
  - � ��������: `GET /api/enclosures`.
  - � ���������� ���������: `GET /api/feeding-schedule`.
- ���������� ��������:
  - ���������: `PATCH /api/feeding-schedule/{id}/complete`.
  - �����������: `POST /api/animals/{animalId}/move-to/{newEnclosureId}`.

---

## 5. �������� ������ � in-memory ���������

�������� ������ ������������ � ������ `Infrastructure\Repositories`:
- ������ `AnimalRepository`, `EnclosureRepository`, `FeedingScheduleRepository` ���������� ��������� `List<T>` ��� �������� ������ � ������.

---

## ����������

������ ������������� ����������� � ������������� ���������:
- ���������� ���� ��������� ����������.
- ��������� ��������� DDD (Value Objects, ������������ ������-������, �������� �������).
- ��������� �������� Clean Architecture (���� ������� ������ ������, ����������� ����� ����������, �������� ������-������). 
