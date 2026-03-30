# System wypożyczalni sprzętu akademickiego

## Uruchomienie

#### Wymagania: zainstalowane min. .NET 9 SDK

1. Sklonuj repo
2. Otwórz terminal
3. Przejdź w terminalu do folderu do którego sklonowane zostało repo
4. Uruchom komende: `dotnet run`

## Demo

Aplikacja posiada interfejsu użytkownika. Zostało zaimplemntowane za to demo aplikacji przedstawiające konkretny scenariusz:
1. Dodanie 6 urządzeń (2 laptopy, 2 projektory, 2 aparaty)
2. Wyświetlenie listy całego sprzętu z aktualnym statusem
3. Dodanie 2 studentów i 1 pracownika
4. Prawidłowe rezerwacje (student – 2 szt., pracownik – 1 szt.)
5. Błędne operacje: przekroczenie limitu studenta, sprzęt już wypożyczony
6. Oznaczenie aparatu jako niedostępny i próba jego wypożyczenia
7. Wyświetlenie listy sprzętu dostępnego do wypożyczenia
8. Zwrot na czas (kara = 0 zł) i zwrot spóźniony o 5 dni (kara = 50 zł)
9. Wyświetlenie aktywnych rezerwacji pracownika i przeterminowanych wypożyczeń
10. Raport końcowy


## Decyzje projektowe

### Spójność (cohesion)
Każda klasa ma jedną odpowiedzialność: modele przechowują dane, serwisy zawierają logikę biznesową, wyjątki opisują błędy domenowe, a `Program.cs` pełni wyłącznie rolę scenariusza demonstracyjnego.

### Luźne powiązania (coupling)
Serwisy komunikują się przez interfejsy (`IEquipmentService`, `IUserService`, `IReservationService`). Zależności są wstrzykiwane przez konstruktor, co ułatwia testowanie i podmienianie implementacji.

### Odpowiedzialności klas
- **`Equipment` (abstrakcyjna)** – przechowuje wspólny stan sprzętu (id, nazwa, status); wymusza implementację `GetDetails()` w podklasach.
- **`Laptop`, `Projector`, `Camera`** – rozszerzają `Equipment` o własne pola i implementują `GetDetails()`.
- **`User`** – przechowuje dane użytkownika i jego typ (Student/Employee).
- **`Reservation`** – reprezentuje pojedyncze wypożyczenie; oblicza czy jest aktywne lub przeterminowane; rejestruje zwrot i karę.
- **`EquipmentService`** – zarządza kolekcją sprzętu (dodawanie, wyszukiwanie, oznaczanie jako niedostępny).
- **`UserService`** – zarządza kolekcją użytkowników.
- **`ReservationService`** – zawiera całą logikę biznesową: walidację limitów, dostępności sprzętu, obliczanie kar za opóźnienie.
- **`ReportService`** – agreguje dane ze wszystkich serwisów i zwraca gotowy raport jako `string`.
- **Klasy wyjątków** – każda opisuje dokładnie jeden błąd domenowy, co ułatwia obsługę konkretnych sytuacji w `Program.cs`.