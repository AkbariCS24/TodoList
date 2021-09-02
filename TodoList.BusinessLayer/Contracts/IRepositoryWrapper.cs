using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.BusinessLayer.Contracts
{
    public interface IRepositoryWrapper
    {
        ITodoItemRepository TodoItem { get; }
        ITodoItemListRepository TodoItemList { get; }
        IAccountRepository Account { get; }
    }
}
