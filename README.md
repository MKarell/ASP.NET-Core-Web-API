# Training Center API 🏫

Prosty backend aplikacji do zarządzania salami dydaktycznymi oraz ich rezerwacjami. Projekt został stworzony w celu praktycznego przećwiczenia tworzenia aplikacji typu **ASP.NET Core Web API**, w tym obsługi routingu, metod HTTP, model bindingu oraz zwracania poprawnych kodów statusu.

W tej wersji projekt opiera się na **strukturach danych w pamięci (In-Memory)** – nie wymaga konfiguracji zewnętrznej bazy danych. Dane są inicjalizowane przy starcie aplikacji i resetują się po jej wyłączeniu.

## 🚀 Technologie
* C#
* .NET (ASP.NET Core Web API)
* Swashbuckle (Swagger) do dokumentacji API

## ✨ Główne funkcjonalności
* **Zarządzanie salami:** Przeglądanie, dodawanie, edycja i usuwanie sal dydaktycznych.
* **Zarządzanie rezerwacjami:** Tworzenie, edycja, usuwanie oraz podgląd rezerwacji.
* **Filtrowanie:** Możliwość wyszukiwania sal i rezerwacji przy użyciu parametrów *Query String* oraz zmiennych w ścieżce (*Route params*).
* **Reguły biznesowe:** Zabezpieczenie przed nakładaniem się rezerwacji czasowych, uniemożliwienie rezerwacji w nieaktywnych lub nieistniejących salach.
* **Walidacja:** Użycie *Data Annotations* do sprawdzania poprawności danych wejściowych (zwracanie statusu `400 Bad Request`).

## ⚙️ Jak uruchomić projekt lokalnie?

1. Sklonuj to repozytorium na swój dysk:
   ```bash
   git clone [https://github.com/TwojaNazwaUzytkownika/NazwaTwojegoRepozytorium.git](https://github.com/TwojaNazwaUzytkownika/NazwaTwojegoRepozytorium.git)
