using System;
using System.Collections.Generic;
using System.Text;
using TodoList.BusinessLayer.Contracts;
using TodoList.DataLayer.Context;

namespace TodoList.BusinessLayer.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly TodoListContext _context;
        private TodoItemRepository _todoItem;
        private TodoItemListRepository _todoItemList;
        private AccountRepository _account;

        public RepositoryWrapper(TodoListContext context)
        {
            _context = context;
        }

        public ITodoItemRepository TodoItem
        {
            get
            {
                if (_todoItem == null)
                {
                    _todoItem = new TodoItemRepository(_context);
                }

                return _todoItem;
            }
        }

        public ITodoItemListRepository TodoItemList
        {
            get
            {
                if (_todoItemList == null)
                {
                    _todoItemList = new TodoItemListRepository(_context);
                }

                return _todoItemList;
            }
        }

        public IAccountRepository Account
        {
            get
            {
                if (_account == null)
                {
                    _account = new AccountRepository(_context);
                }

                return _account;
            }
        }

    }
}
