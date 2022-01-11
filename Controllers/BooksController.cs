using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBooks.Hubs;
using TestBooks.Models;

namespace TestBooks.Controllers
{
    public class BooksController : Controller
    {
            private readonly BooksDbContext _dbContext;
            private readonly IHubContext<NotificationHub> _hubContext;

        public BooksController(BooksDbContext dbContext, IHubContext<NotificationHub> hubContext)
        {
            _dbContext = dbContext;
            _hubContext = hubContext;
        }
        public async Task<IActionResult> Index() => View(await SetStudentViewModelList());

        [Route("Books/Index/{id:int}/{isDelete:bool}")]
        public async Task<IActionResult> Index(int id, bool isDelete)
        {
            try
            {
                var books = _dbContext.Books.Find(id);
                BooksViewModel booksViewModel = null;

                if (isDelete)
                {
                    _dbContext.Books.Remove(books);
                    var notification = new Notification()
                    {
                        BookName = books.BookName,
                        BookCode = books.BookId.ToString(),
                        OperationType = "Eliminar"
                    };
                    await _dbContext.Notifications.AddAsync(notification);
                    await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
                    await _dbContext.SaveChangesAsync();
                    booksViewModel = new BooksViewModel()
                    {
                        Book = null,
                        Books = await _dbContext.Books.ToListAsync()
                    };
                }
                else
                {
                    booksViewModel = new BooksViewModel()
                    {
                        Book = books,
                        Books = await _dbContext.Books.ToListAsync()
                    };
                }
                return View(booksViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.InnerException.Message;
                return View(await SetStudentViewModelList());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(BooksViewModel booksViewModel)
        {
            try
            {
                var booksEntity = new Books()
                {
                    BookId = booksViewModel.Book.BookId,
                    BookName = booksViewModel.Book.BookName,
                    Description = booksViewModel.Book.Description,
                    Author = booksViewModel.Book.Author,
                    PublishDate = booksViewModel.Book.PublishDate
                };

                if (booksEntity.BookId > 0)
                {
                    _dbContext.Books.Update(booksEntity);

                    var notification = new Notification()
                    {
                        BookName = booksEntity.BookName,
                        BookCode = booksEntity.BookId.ToString(),
                        OperationType = "Modificar"
                    };

                    await _dbContext.Notifications.AddAsync(notification);

                    await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
                }
                else
                {
                    await _dbContext.Books.AddAsync(booksEntity);

                    var notification = new Notification()
                    {
                        BookName = booksEntity.BookName,
                        BookCode = booksEntity.BookId.ToString(),
                        OperationType = "Guardar"
                    };

                    await _dbContext.Notifications.AddAsync(notification);

                    await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
                }

                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.InnerException.Message;
                return View(await SetStudentViewModelList());
            }
        }

        private async Task<BooksViewModel> SetStudentViewModelList()
        {
            var booksViewModel = new BooksViewModel()
            {
                Book = null,
                Books = await _dbContext.Books.ToListAsync()
            };
            return booksViewModel;
        }
    }
    
}
