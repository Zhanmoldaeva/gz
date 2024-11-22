/*1 задание
using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;
using System.Linq;

public enum BookStatus
{
    Available,
    Borrowed
}

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public BookStatus Status { get; set; }

    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        Status = BookStatus.Available;
    }

    public override string ToString()
    {
        return $"{Title} - {Author} (ISBN: {ISBN}) - {Status}";
    }
}

public class Reader
{
    public string Name { get; set; }
    public List<Book> BorrowedBooks { get; private set; }

    private const int MaxBooks = 3;

    public Reader(string name)
    {
        Name = name;
        BorrowedBooks = new List<Book>();
    }

    public bool BorrowBook(Book book)
    {
        if (BorrowedBooks.Count >= MaxBooks)
        {
            Console.WriteLine($"{Name} {MaxBooks}-тен көп кітап ала алмайды.");
            return false;
        }

        if (book.Status == BookStatus.Available)
        {
            BorrowedBooks.Add(book);
            book.Status = BookStatus.Borrowed;
            Console.WriteLine($"{Name} \"{book.Title}\" атты кітапты алды.");
            return true;
        }

        Console.WriteLine($"\"{book.Title}\" қолжетімді емес.");
        return false;
    }

    public void ReturnBook(Book book)
    {
        if (BorrowedBooks.Contains(book))
        {
            BorrowedBooks.Remove(book);
            book.Status = BookStatus.Available;
            Console.WriteLine($"{Name} \"{book.Title}\" атты кітапты қайтарды.");
        }
    }
}

public class Librarian
{
    public string Name { get; set; }

    public Librarian(string name)
    {
        Name = name;
    }

    public void AddBook(Library library, Book book)
    {
        library.Books.Add(book);
        Console.WriteLine($"{Name} кітапханасына \"{book.Title}\" атты кітапты қосты.");
    }
}

public class Library
{
    public List<Book> Books { get; private set; }

    public Library()
    {
        Books = new List<Book>();
    }

    public void DisplayBooks(bool showAll = false)
    {
        foreach (var book in Books)
        {
            if (showAll || book.Status == BookStatus.Available)
            {
                Console.WriteLine(book);
            }
        }
    }

    public List<Book> SearchBooks(string query)
    {
        return Books.Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                 b.Author.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}

class Program
{
    static void Main(string[] args)
    {
       
        var library = new Library();
        var librarian = new Librarian("тасик");

        librarian.AddBook(library, new Book("гул", "ая камысбай", "1531123"));
        librarian.AddBook(library, new Book("осимдиктер", "сая намысова", "846351"));
        librarian.AddBook(library, new Book("жануарлао", "тамага асыл", "445414"));

        var reader = new Reader("John");

        Console.WriteLine("Кітапханада қолжетімді кітаптар:");
        library.DisplayBooks();

        var bookToBorrow = library.Books.First();
        reader.BorrowBook(bookToBorrow);

        Console.WriteLine("\nКітапханада қазір қолжетімді кітаптар:");
        library.DisplayBooks();

        reader.ReturnBook(bookToBorrow);

        Console.WriteLine("\nКітаптар қайтарылғаннан кейінгі кітапханадағы кітаптар:");
        library.DisplayBooks(true);

        Console.WriteLine("\n'Clean' сұранысы бойынша іздеу нәтижелері:");
        var searchResults = library.SearchBooks("Clean");
        foreach (var book in searchResults)
        {
            Console.WriteLine(book);
        }
    }
}*/
/*2 хадаинее
public class Program
{
    public static void Main()
    {
        IHotelService hotelService = new HotelService();
        IBookingService bookingService = new BookingService();
        IPaymentService paymentService = new PaymentService();
        INotificationService notificationService = new NotificationService();
        IUserManagementService userManagementService = new UserManagementService();

        // Пайдаланушыны тіркеу
        int userId = userManagementService.RegisterUser("Раушан", "raushan@gmail.com", "pliuh526");

        // Қонақ үйлерді қосу
        hotelService.AddHotel(new Hotel { Id = 1, Name = "Mask Hotel", Location = "Мекке", RoomClass = "Люкс", PricePerNight = 800 });
        hotelService.AddHotel(new Hotel { Id = 2, Name = "Almaty", Location = "АЛматы", RoomClass = "Стандарт", PricePerNight = 300 });

        // Қонақ үйлерді іздеу
        var hotels = hotelService.SearchHotels("Алматы, "Люкс", 300);
        Console.WriteLine("Қол жетімді қонақ үйлер:");
        foreach (var hotel in hotels)
        {
            Console.WriteLine($"{hotel.Id}: {hotel.Name}, {hotel.Location}, {hotel.PricePerNight}/түн");
        }

        // Бөлмені брондау
        var booking = bookingService.BookRoom(userId, 1, DateTime.Today.AddDays(1), DateTime.Today.AddDays(3));
        notificationService.SendConfirmation(userId, booking);

        // Төлем жасау
        paymentService.ProcessPayment(booking.BookingId, 800, "Несие картасы");
    }
}
public class NotificationService : INotificationService
{
    public void SendConfirmation(int userId, Booking booking)
    {
        Console.WriteLine($"Қолданушы {userId} үшін брондау #{booking.BookingId} расталды.");
    }

    public void SendReminder(int userId, Booking booking)
    {
        Console.WriteLine($"Қолданушы {userId} үшін еске салу: келу күні {booking.CheckInDate.ToShortDateString()}.");
    }
}


Описание компонентов и интерфейсов:
UI-компонент:

Консольный интерфейс для взаимодействия пользователя с системой.
Методы:
SearchHotels(): Поиск отелей.
BookRoom(): Бронирование номеров.
ViewBookings(): Просмотр существующих бронирований.
HotelService:

Управляет данными об отелях.
Методы:
SearchHotels(location, class, price): Поиск отелей по критериям.
AddHotel(hotelData): Добавление новых отелей.
GetHotelDetails(hotelId): Получение информации об отеле.
BookingService:

Обрабатывает бронирования.
Методы:
BookRoom(user, hotel, dates): Бронирование номера.
CheckAvailability(hotel, dates): Проверка доступности номеров.
StoreBooking(bookingDetails): Хранение информации о бронированиях.
PaymentService:

Управляет платежами.
Методы:
ProcessPayment(paymentDetails): Обработка платежа.
VerifyPayment(bookingId): Проверка статуса оплаты.
NotificationService:

Отправляет уведомления пользователям.
Методы:
SendConfirmation(user, bookingDetails): Уведомление о подтверждении бронирования.
SendReminder(user, bookingDetails): Напоминание перед заездом.
UserManagementService:

Управляет регистрацией и авторизацией пользователей.
Методы:
RegisterUser(userData): Регистрация нового пользователя.
AuthenticateUser(credentials): Авторизация пользователя.*/